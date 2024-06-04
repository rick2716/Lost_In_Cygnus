using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemies : MonoBehaviour
{
    [Header("RutinaEnemy")]
    public float wanderRadius; // Radio para rondar
    public float chaseRadius; // Radio para que el enemigo empiece a perseguir
    public float attackRadius; // Radio para empezar a atacar
    public float wanderTimer; // Tiempo antes de cambiar de posicion
    private float timer;

    [Header("Vida")]
    public int maxVida;
    public int vida;
    public int damagePerHit;

    private Transform player; 
    private NavMeshAgent agent; 
    private Animator animator;

    [Header("CollidersAtaquesSoloMuscomorphSinAlas")]
    public GameObject colliderBite;
    public GameObject colliderSting;

    [Header("CollidersAtaqueArthromahre")]
    public GameObject colliderClaw1;
    public GameObject colliderClaw2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        animator = GetComponent<Animator>();
        vida = maxVida;
    }

    void Update()
    {
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
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
            animator.SetBool("Move", true);
            animator.SetBool("Attack", false);

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
        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("Attack", true);
        animator.SetBool("Move", false);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit; //El punto dentro del navmesh mas cercano al generado de forma random

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        if(damage <= 0)
        {
            agent.SetDestination(transform.position);
            animator.SetTrigger("Die");
        }
    }

    // METODOS PARA PRENDER COLLIDERS MUSCOMORPH (BICHO PEQUEÑO)
    public void OnStingAttack()
    {
        colliderSting.SetActive(true);
    }

    public void OffStingAttack()
    {
        colliderSting.SetActive(false);
        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);
    }
    public void OnBiteAttack()
    {
        colliderBite.SetActive(true);
    }

    public void OffBiteAttack()
    {
        colliderBite.SetActive(false);
    }

    // METODOS PARA PRENDER COLLIDERS Arthromahre (bicho grande)
    public void OnClawAttack()
    {
        colliderClaw1.SetActive(true);
        colliderClaw2.SetActive(true);
    }

    public void OffClawAttack()
    {
        colliderClaw1.SetActive(false);
        colliderClaw2.SetActive(false);

        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);
    }
}
