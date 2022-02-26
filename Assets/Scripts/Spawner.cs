using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;
    [SerializeField] private Transform pos4;

    [SerializeField] private GameObject ennemyToBeSpawn1;
    [SerializeField] private GameObject ennemyToBeSpawn2;
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
        Instantiate(ennemyToBeSpawn1, pos1.position, Quaternion.identity,targetParent);
        Instantiate(ennemyToBeSpawn1, pos2.position, Quaternion.identity,targetParent);
        Instantiate(ennemyToBeSpawn1, pos3.position, Quaternion.identity,targetParent);
        Instantiate(ennemyToBeSpawn2, pos4.position, Quaternion.identity,targetParent);
    }
}
