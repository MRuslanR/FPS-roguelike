using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml. Serialization;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 20f;
    public float grenadeLifetime = 3f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrenade();
        }
    }
     
    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.VelocityChange);
        StartCoroutine(ExplodeAfterDelay(grenade));
    }
    IEnumerator ExplodeAfterDelay(GameObject grenade)
    {
        yield return new WaitForSeconds(grenadeLifetime);
        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        if (grenadeScript != null)
        {
            grenadeScript.Explode();
        }
        Destroy(grenade,0.5f);
    }
}