using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    // CANVAS
    [SerializeField] private GameObject wave1;
    [SerializeField] private GameObject wave2;
    [SerializeField] private GameObject wave3;
    [SerializeField] private GameObject victory;
    // POSITION
    [SerializeField] private List<Transform> posEnemy1;
    [SerializeField] private List<Transform> posEnemy2;
 
    // ENEMIES
    [SerializeField] private GameObject enemyToBeSpawn1;
    [SerializeField] private GameObject enemyToBeSpawn2;
    [SerializeField] private Transform enemyParent;
    
    // PARAMETERS
        // WAVE 1
    [SerializeField] private int numberOfEnemies1_Wave1;
    [SerializeField] private int numberOfEnemies2_Wave1;
        // WAVE 2
    [SerializeField] private int numberOfEnemies1_Wave2;
    [SerializeField] private int numberOfEnemies2_Wave2;
        // WAVE 3
    [SerializeField] private int numberOfEnemies1_Wave3;
    [SerializeField] private int numberOfEnemies2_Wave3;

    [SerializeField] private float timeBetweenWaves;
    
    // COUNTER
    public int currentNumberOfEnemies;
    private int currentWave;

    private void Start()
    {
        currentWave = 1;
        currentNumberOfEnemies = numberOfEnemies1_Wave1 + numberOfEnemies2_Wave1;
        
        if (numberOfEnemies1_Wave1 > 20) { numberOfEnemies1_Wave1 = 20; }
        if (numberOfEnemies2_Wave1 > 10) { numberOfEnemies2_Wave1 = 10; }
        
        if (numberOfEnemies1_Wave2 > 20) { numberOfEnemies1_Wave2 = 20; }
        if (numberOfEnemies2_Wave2 > 10) { numberOfEnemies2_Wave2 = 10; }
        
        if (numberOfEnemies1_Wave3 > 20) { numberOfEnemies1_Wave3 = 20; }
        if (numberOfEnemies2_Wave3 > 10) { numberOfEnemies2_Wave3 = 10; }
    }

    private void Update()
    {
        if (currentNumberOfEnemies <= 0 && currentWave ==1)
        {
            wave2.SetActive(true);
            currentNumberOfEnemies = numberOfEnemies1_Wave2 + numberOfEnemies2_Wave2;
            currentWave = 2;
            StartCoroutine(SpawnWithDelay(numberOfEnemies1_Wave2, numberOfEnemies2_Wave2,wave2));
        }
        else if (currentNumberOfEnemies <= 0 && currentWave == 2)
        {
            wave3.SetActive(true);
            currentNumberOfEnemies = numberOfEnemies1_Wave3 + numberOfEnemies2_Wave3;
            currentWave = 3;
            StartCoroutine(SpawnWithDelay(numberOfEnemies1_Wave3, numberOfEnemies2_Wave3,wave3));
        }
        else if (currentNumberOfEnemies <= 0 && currentWave == 3)
        {
            victory.SetActive(true);
            currentWave = 0;
            StartCoroutine(RestartWithDelay(victory));
        }
    }

    public void StartSpawningWave1()
    {
        //WAVE1
        wave1.SetActive(true);
        currentWave = 1;
        currentNumberOfEnemies = numberOfEnemies1_Wave1 + numberOfEnemies2_Wave1;
        StartCoroutine(SpawnWithDelay(numberOfEnemies1_Wave1,numberOfEnemies2_Wave1,wave1));
    }
    
    private void SpawnWave(int numberOfEnemies1, int numberOfEnemies2)
    {
        currentNumberOfEnemies = numberOfEnemies1 + numberOfEnemies2;
        for (int i=0;i<numberOfEnemies1;i++)
        {
            Instantiate(enemyToBeSpawn1, posEnemy1[i].position,Quaternion.Euler(0,90,0) ,enemyParent);
        }

        for (int i = 0; i < numberOfEnemies2; i++)
        {
            Instantiate(enemyToBeSpawn2, posEnemy2[i].position, Quaternion.Euler(0,90,0),enemyParent);
        }
    }

    IEnumerator SpawnWithDelay(int numberOfEnemies1, int numberOfEnemies2, GameObject canvas)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        canvas.SetActive(false);
        SpawnWave(numberOfEnemies1,numberOfEnemies2); 
    }

    IEnumerator RestartWithDelay(GameObject canvas)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        canvas.SetActive(false);
        manager.GetComponent<Restart>().RestartGame();
    }
    
}
