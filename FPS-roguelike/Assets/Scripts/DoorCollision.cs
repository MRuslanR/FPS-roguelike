using UnityEngine;

public class DoorCollision : MonoBehaviour
{
    private DoorController child;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        child = transform.GetChild(0).GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && child.isOpen)
        {   
            child.Close();
        }
    }
}