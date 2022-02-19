using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform impactBullet;
    private GameObject bulletParent;
    private void Start()
    {
        bulletParent = GameObject.Find("BulletParent");
        Destroy(this.gameObject,4);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point+contact.normal*0.01f;
            Instantiate(impactBullet, position, rotation,bulletParent.transform);
        }
        
        
        GameObject.Destroy(gameObject);
        Instantiate(explosion, 
            transform.position, 
            Quaternion.identity,
            bulletParent.transform);
        
    }
    
}
