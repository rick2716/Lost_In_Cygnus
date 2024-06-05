using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima del jugador
    private int currentHealth; // Vida actual del jugador
    public Slider healthSlider; // Referencia al Slider de UI

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // M�todo para recibir da�o
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthSlider.value = currentHealth;

        if (currentHealth == 0)
        {
            Die();
        }
    }

    // M�todo que se llama cuando la vida llega a 0
    void Die()
    {
        // Aqu� puedes a�adir l�gica para la muerte del jugador
        Debug.Log("Player has died!");
    }
}
