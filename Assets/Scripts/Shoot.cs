using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent;
    // Update is called once per frame
    void Update()
    {
        Tirer();
    }
    
    private void Tirer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bulletBody = Instantiate(bullet,
                transform.position + transform.forward*1.7f,
                transform.rotation,
                bulletParent);
            bulletBody.GetComponent<Rigidbody>().AddForce(transform.forward*3000);
        }
    }

  
}
