using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private GameObject manager;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float maxhealth;
    public float health;
    public Animator animatorAttack;
    private bool isDead;
    [SerializeField] private float damage;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        isDead = false;
        manager = GameObject.Find("Manager");
        health = maxhealth;
        WhatIsThePlayer();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        animatorAttack.SetBool("Walk Forward",true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint est atteint
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calcul un point random dans la range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y+4, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 10f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        animatorAttack.SetBool("Walk Forward",true);
        agent.SetDestination(player.position);
       
    }

    private void AttackPlayer()
    {
        animatorAttack.SetBool("Walk Forward",false);
       
        agent.SetDestination(transform.position);
        
        // Quand l'ennemi attaque, il regarde dans la direction du joueur
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4);

        if (!alreadyAttacked)
        {
            animatorAttack.SetTrigger("Attack");
            player.GetComponent<PlayerController>().GetDamage(damage);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        if (health>0)
        {
            animatorAttack.SetTrigger("Take Damage");
            health -= damage;
        }
        if (health <= 0 && isDead==false)
        {
            isDead = true;
            animatorAttack.SetTrigger("Dead");
            DestroyEnemy(1.2f);
        }
    }

    private void DestroyEnemy(float delay)
    {
        manager.GetComponent<Spawner>().currentNumberOfEnemies -= 1;
        Destroy(gameObject, delay);
    }

    private void OnDrawGizmosSelected()
    {
        // Affiche les distances d'attaque et de vue
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void WhatIsThePlayer()
    {
        if (StartGame.weaponData.weaponName == "Sniper")
        {
            player = GameObject.Find("Russian_sniper_prefab Variant(Clone)").transform;
        }
        else if (StartGame.weaponData.weaponName == "Mp5")
        {
            player = GameObject.Find("Submachine5_prefab Variant(Clone)").transform;
        }
        else if (StartGame.weaponData.weaponName == "Shotgun")
        {
            player = GameObject.Find("Shotgun3_prefab Variant(Clone)").transform;
        }
    }

}