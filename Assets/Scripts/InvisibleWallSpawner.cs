using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InvisibleWallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameObject spider;
    [SerializeField] private Transform targetParent;
    [SerializeField] private int numberOfTargets = 25;
    private Vector3 _position;
    [SerializeField] bool isSpawn;

    private void Start()
    {
        isSpawn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 6 && isSpawn == false)
        {
            for(int i=0; i<numberOfTargets; i++)
            {
                _position = new Vector3(
                    Random.Range(-48, 48),
                    Random.Range(4, 40),
                    Random.Range(-42, 40));
                Instantiate(objectToSpawn, _position, Quaternion.identity,targetParent);
            }
            for(int i=0; i<3; i++)
            {
                _position = new Vector3(
                    Random.Range(-48, 48),
                    1f,
                    Random.Range(-42, 40));
                Instantiate(spider, _position, Quaternion.identity,targetParent);
            }
            isSpawn = true;

        }
    }

    
}
