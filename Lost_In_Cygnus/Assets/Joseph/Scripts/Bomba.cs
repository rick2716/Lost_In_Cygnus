using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float radioDeDa�o = 5f; // Radio del �rea de da�o
    public float fuerzaDeDa�o = 700f; // Fuerza de empuje de la explosi�n
    public int da�o = 20; // Cantidad de da�o que hace la bomba
    public GameObject explosionEffectPrefab; // Prefab del sistema de part�culas para la explosi�n

    void OnCollisionEnter(Collision collision)
    {
        // Obtener el punto de impacto de la colisi�n
        Vector3 puntoDeImpacto = collision.contacts[0].point;
        // Destruir la bomba despu�s de la explosi�n
        Destroy(gameObject);

        // Obtener la posici�n del suelo en el punto de impacto
        RaycastHit hit;
        if (Physics.Raycast(puntoDeImpacto, Vector3.down, out hit, Mathf.Infinity))
        {
            // Si se encuentra el suelo, llamar al m�todo Explosi�n con la posici�n del suelo
            Explosi�n(hit.point);
        }
        else
        {
            // Si no se encuentra el suelo, llamar al m�todo Explosi�n con el punto de impacto original
            Explosi�n(puntoDeImpacto);
        }
    }

    void Explosi�n(Vector3 posici�n)
    {
        // Instanciar el efecto de part�culas de la explosi�n en la posici�n del suelo
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, posici�n, Quaternion.identity);
        // Destruir el efecto de part�culas despu�s de 8 segundos
        Destroy(explosionEffect, 8f);

        // Encontrar todos los colliders en el radio de da�o
        Collider[] colliders = Physics.OverlapSphere(posici�n, radioDeDa�o);

        foreach (Collider cercano in colliders)
        {
            // Filtrar por tag "Enemy"
            if (cercano.CompareTag("Enemy"))
            {
                Rigidbody rb = cercano.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(fuerzaDeDa�o, posici�n, radioDeDa�o);
                }

                // Aplicar da�o a los enemigos
                Enemies enemyHealth = cercano.GetComponent<Enemies>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(da�o);
                }

            }
            if (cercano.CompareTag("Boss"))
            {
                Rigidbody rb = cercano.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(fuerzaDeDa�o, posici�n, radioDeDa�o);
                }

                // Aplicar da�o a los enemigos
                Boss enemyHealth = cercano.GetComponent<Boss>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(da�o);
                }

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDa�o);
    }
}
