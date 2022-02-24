using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;

    [SerializeField] private GameObject objectToBeSpawn;
    [SerializeField] private Transform targetParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Invoke(nameof(SpawnWithDelay),3f);
    }
    
    private void SpawnWithDelay()
    {
        Instantiate(objectToBeSpawn, pos1.position, Quaternion.identity,targetParent);
        Instantiate(objectToBeSpawn, pos2.position, Quaternion.identity,targetParent);
        Instantiate(objectToBeSpawn, pos3.position, Quaternion.identity,targetParent);
    }
}
