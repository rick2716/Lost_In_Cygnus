using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float radioDeDaño = 5f; // Radio del área de daño
    public float fuerzaDeDaño = 700f; // Fuerza de empuje de la explosión
    public int daño = 20; // Cantidad de daño que hace la bomba
    public GameObject explosionEffectPrefab; // Prefab del sistema de partículas para la explosión

    void OnCollisionEnter(Collision collision)
    {
        // Obtener el punto de impacto de la colisión
        Vector3 puntoDeImpacto = collision.contacts[0].point;
        // Destruir la bomba después de la explosión
        Destroy(gameObject);

        // Obtener la posición del suelo en el punto de impacto
        RaycastHit hit;
        if (Physics.Raycast(puntoDeImpacto, Vector3.down, out hit, Mathf.Infinity))
        {
            // Si se encuentra el suelo, llamar al método Explosión con la posición del suelo
            Explosión(hit.point);
        }
        else
        {
            // Si no se encuentra el suelo, llamar al método Explosión con el punto de impacto original
            Explosión(puntoDeImpacto);
        }
    }

    void Explosión(Vector3 posición)
    {
        // Instanciar el efecto de partículas de la explosión en la posición del suelo
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, posición, Quaternion.identity);
        // Destruir el efecto de partículas después de 8 segundos
        Destroy(explosionEffect, 8f);

        // Encontrar todos los colliders en el radio de daño
        Collider[] colliders = Physics.OverlapSphere(posición, radioDeDaño);

        foreach (Collider cercano in colliders)
        {
            // Filtrar por tag "Enemy"
            if (cercano.CompareTag("Enemy"))
            {
                Rigidbody rb = cercano.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(fuerzaDeDaño, posición, radioDeDaño);
                }

                // Aplicar daño a los enemigos
                Enemies enemyHealth = cercano.GetComponent<Enemies>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(daño);
                }

            }
            if (cercano.CompareTag("Boss"))
            {
                Rigidbody rb = cercano.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(fuerzaDeDaño, posición, radioDeDaño);
                }

                // Aplicar daño a los enemigos
                Boss enemyHealth = cercano.GetComponent<Boss>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(daño);
                }

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDaño);
    }
}
