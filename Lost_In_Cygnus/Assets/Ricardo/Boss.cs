using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [Header("RutinaBoss")]
    public float walkRadius;
    public float chaseRadius; // Radio para que el enemigo empiece a perseguir
    public float attackRadius; // Radio para empezar a atacar
    public float wanderTimer; // Tiempo antes de cambiar de posicion
    public float waitToChase;
    private float timer;
    [SerializeField] private int randomAttack;
    private float initialSpeed;

    [Header("Ataques")]
    public int numAttacks;
    public GameObject colliderBite;
    public GameObject colliderTentacle1;
    public GameObject colliderTentacle2;
    public Transform spawnPointSpit;
    public GameObject prefabSpit;

    [Header("Vida")]
    public int maxHealth = 30; // Vida máxima del enemigo
    [SerializeField] private int currentHealth; // Vida actual del enemigo
    private DropEnemigo dropEnemigo; // Referencia al script DropEnemigo

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        timer = 0;
        initialSpeed = agent.speed;

        currentHealth = maxHealth;
        dropEnemigo = GetComponent<DropEnemigo>(); // Obtener referencia al script DropEnemigo
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (timer <= waitToChase)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
        else if (distanceToPlayer <= attackRadius)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            randomAttack = Random.Range(0, numAttacks);
            animator.SetTrigger("Attack");
            animator.SetInteger("Random", randomAttack);
        }
        else if (distanceToPlayer <= chaseRadius)
        {
            agent.speed = 7;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
        else if (distanceToPlayer <= walkRadius)
        {
            agent.SetDestination(player.position);
            agent.speed = initialSpeed;
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);

        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            agent.SetDestination(transform.position);
            agent.speed = 0;
        }
    }

    public void DestruirEnemigo()
    {
        dropEnemigo.DestruirPiedra();
        Destroy(gameObject);
    }

    public void TurnOffColliders()
    {
        colliderBite.SetActive(false);
        colliderTentacle1.SetActive(false);
        colliderTentacle2.SetActive(false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        timer = 0;
        waitToChase = 0;
        agent.speed = initialSpeed;
    }

    public void SpitAttack()
    {
        Instantiate(prefabSpit, spawnPointSpit.position, Quaternion.identity);
    }

    public void BiteAttack()
    {
        colliderBite.SetActive(true);
    }

    public void TencaclesAttack()
    {
        colliderTentacle1.SetActive(true);
        colliderTentacle2.SetActive(true);
    }
}
