using UnityEngine;

public class DoorCollision : MonoBehaviour
{
    private DoorController child;
    void Start()
    {
        child = transform.GetChild(0).GetComponent<DoorController>();
    }

    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && child.isOpen)
        {   
            child.Close();
        }
    }
}