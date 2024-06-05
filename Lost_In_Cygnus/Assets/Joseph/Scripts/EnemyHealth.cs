using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Vida m�xima del enemigo
    private int currentHealth; // Vida actual del enemigo
    private DropEnemigo dropEnemigo; // Referencia al script DropEnemigo

    void Start()
    {
        currentHealth = maxHealth;
        dropEnemigo = GetComponent<DropEnemigo>(); // Obtener referencia al script DropEnemigo
    }

    // M�todo para recibir da�o
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

    // M�todo que se llama cuando la vida llega a 0
    public void Die()
    {
        // Aqu� puedes a�adir l�gica para la muerte del enemigo, como jugar una animaci�n de muerte
        Debug.Log("Enemy has died!");

        // Llamar al m�todo DestruirPiedra del script DropEnemigo si est� disponible
        if (dropEnemigo != null)
        {
            dropEnemigo.DestruirPiedra();
        }

        Destroy(gameObject);
    }
}
