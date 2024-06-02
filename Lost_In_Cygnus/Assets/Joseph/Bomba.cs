using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float radioDeDa�o = 5f; // Radio del �rea de da�o
    public float fuerzaDeDa�o = 700f; // Fuerza de empuje de la explosi�n
    public LayerMask capasAfectadas; // Capas que ser�n afectadas por la explosi�n
    public GameObject explosionEffectPrefab; // Prefab del sistema de part�culas para la explosi�n

    void OnCollisionEnter(Collision collision)
    {
        // Obtener el punto de impacto de la colisi�n
        Vector3 puntoDeImpacto = collision.contacts[0].point;

        // Obtener la posici�n del suelo en el punto de impacto
        Destroy(gameObject);
        RaycastHit hit;
        if (Physics.Raycast(puntoDeImpacto, Vector3.down, out hit, Mathf.Infinity, capasAfectadas))
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
        ParticleSystem ps = explosionEffect.GetComponent<ParticleSystem>();

        // Ajustar el radio de la base del cono y la altura
        var shape = ps.shape;
        shape.radius = radioDeDa�o; // Ajustar el radio de la base del cono
        shape.angle = 25f; // Ajustar el �ngulo del cono si es necesario


        ps.Play();

        // Destruir el efecto de part�culas despu�s de su duraci�n
        Destroy(explosionEffect, ps.main.duration);

        // Encontrar todos los colliders en el radio de da�o
        Collider[] colliders = Physics.OverlapSphere(posici�n, radioDeDa�o, capasAfectadas);

        foreach (Collider cercano in colliders)
        {
            Rigidbody rb = cercano.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaDeDa�o, posici�n, radioDeDa�o);
            }

            // Aqu� puedes agregar l�gica para aplicar da�o a objetos espec�ficos
            // Por ejemplo:
            // Vida vida = cercano.GetComponent<Vida>();
            // if (vida != null)
            // {
            //     vida.TomarDa�o(damageAmount);
            // }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDa�o);
    }
}
