using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20; // Daño que hace el jugador con cada ataque
    public float attackRange = 2f; // Rango de ataque
    public LayerMask enemyLayers; // Capas que corresponden a los enemigos

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Ajusta esto según el botón que uses para atacar
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detectar enemigos en el rango de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);

        // Infligir daño a los enemigos detectados
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
        // Dibujar el rango de ataque en la escena para facilitar la configuración
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
