using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemies : MonoBehaviour
{
    public float wanderRadius; // Radio para rondar
    public float chaseRadius; // Radio para que el enemigo empiece a perseguir
    public float attackRadius; // Radio para empezar a atacar
    public float wanderTimer; // Tiempo antes de cambiar de posicion

    private Transform player; 
    private NavMeshAgent agent; 
    private float timer; 

    private Animator animator; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRadius)
        {
            Attack();
        }
        else if (distanceToPlayer <= chaseRadius)
        {
            Chase();
        }
        else
        {
            // Rondar aleatoreamente
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        //animator.SetTrigger("Attack");
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit; //El punto dentro del navmesh mas cercano al generado de forma random

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
