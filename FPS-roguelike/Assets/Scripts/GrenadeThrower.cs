using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml. Serialization;
using TMPro;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 20f;
    public float grenadeLifetime = 3f;
    public TextMeshProUGUI count;
    public int grenadeCount = 2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && grenadeCount > 0)
        {
            grenadeCount--;
            count.text = grenadeCount.ToString();
            ThrowGrenade();
        }
    }

    private void Start()
    {
        count = GameObject.Find("Grenade_counter").GetComponent<TextMeshProUGUI>();
        count.text = grenadeCount.ToString();
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
        // Destroy(grenade,0.5f);
    }
}