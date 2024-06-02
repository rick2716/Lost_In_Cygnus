using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float radioDeDaño = 5f; // Radio del área de daño
    public float fuerzaDeDaño = 700f; // Fuerza de empuje de la explosión
    public LayerMask capasAfectadas; // Capas que serán afectadas por la explosión
    public GameObject explosionEffectPrefab; // Prefab del sistema de partículas para la explosión

    void OnCollisionEnter(Collision collision)
    {
        // Obtener el punto de impacto de la colisión
        Vector3 puntoDeImpacto = collision.contacts[0].point;

        // Obtener la posición del suelo en el punto de impacto
        Destroy(gameObject);
        RaycastHit hit;
        if (Physics.Raycast(puntoDeImpacto, Vector3.down, out hit, Mathf.Infinity, capasAfectadas))
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
        ParticleSystem ps = explosionEffect.GetComponent<ParticleSystem>();

        // Ajustar el radio de la base del cono y la altura
        var shape = ps.shape;
        shape.radius = radioDeDaño; // Ajustar el radio de la base del cono
        shape.angle = 25f; // Ajustar el ángulo del cono si es necesario


        ps.Play();

        // Destruir el efecto de partículas después de su duración
        Destroy(explosionEffect, ps.main.duration);

        // Encontrar todos los colliders en el radio de daño
        Collider[] colliders = Physics.OverlapSphere(posición, radioDeDaño, capasAfectadas);

        foreach (Collider cercano in colliders)
        {
            Rigidbody rb = cercano.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaDeDaño, posición, radioDeDaño);
            }

            // Aquí puedes agregar lógica para aplicar daño a objetos específicos
            // Por ejemplo:
            // Vida vida = cercano.GetComponent<Vida>();
            // if (vida != null)
            // {
            //     vida.TomarDaño(damageAmount);
            // }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDaño);
    }
}
