using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void Start()
    {
        Destroy(this.gameObject,6);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(gameObject);
        Instantiate(explosion, 
            transform.position, 
            Quaternion.identity);
        
    }
    
}
