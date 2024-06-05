using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20; // Da�o que hace el jugador con cada ataque
    public float attackRange = 2f; // Rango de ataque
    public LayerMask enemyLayers; // Capas que corresponden a los enemigos

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Ajusta esto seg�n el bot�n que uses para atacar
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detectar enemigos en el rango de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);

        // Infligir da�o a los enemigos detectados
        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el rango de ataque en la escena para facilitar la configuraci�n
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
