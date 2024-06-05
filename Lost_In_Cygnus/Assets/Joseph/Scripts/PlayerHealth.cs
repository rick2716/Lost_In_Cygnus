using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del jugador
    private int currentHealth; // Vida actual del jugador
    public Slider healthSlider; // Referencia al Slider de UI

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Método para recibir daño
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

    // Método que se llama cuando la vida llega a 0
    void Die()
    {
        // Aquí puedes añadir lógica para la muerte del jugador
        Debug.Log("Player has died!");
    }
}
