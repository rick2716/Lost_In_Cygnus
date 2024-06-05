using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo
    private DropEnemigo dropEnemigo; // Referencia al script DropEnemigo

    void Start()
    {
        currentHealth = maxHealth;
        dropEnemigo = GetComponent<DropEnemigo>(); // Obtener referencia al script DropEnemigo
    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Método que se llama cuando la vida llega a 0
    public void Die()
    {
        // Aquí puedes añadir lógica para la muerte del enemigo, como jugar una animación de muerte
        Debug.Log("Enemy has died!");

        // Llamar al método DestruirPiedra del script DropEnemigo si está disponible
        if (dropEnemigo != null)
        {
            dropEnemigo.DestruirPiedra();
        }

        Destroy(gameObject);
    }
}
