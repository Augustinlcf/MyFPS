using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InvisibleWallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform targetParent;
    [SerializeField] private int numberOfTargets = 25;
    private Vector3 _position;
    [SerializeField] bool isSpawn = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "weapon" && isSpawn == false)
        {
            for(int i=0; i<numberOfTargets; i++)
            {
                _position = new Vector3(
                    Random.Range(-48, 48),
                    Random.Range(4, 40),
                    Random.Range(-42, 40));
                Instantiate(objectToSpawn, _position, Quaternion.identity,targetParent);
            }
            isSpawn = true;

        }
    }

    
}
