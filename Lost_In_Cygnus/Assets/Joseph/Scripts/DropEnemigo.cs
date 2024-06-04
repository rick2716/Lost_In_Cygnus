using UnityEngine;

public class DropEnemigo: MonoBehaviour
{
    public GameObject DropPrefab; // El prefab de la versi�n peque�a de la piedra
    private bool cercaDelJugador = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelJugador = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelJugador = false;
        }
    }

    void Update()
    {
        if (cercaDelJugador && Input.GetKeyDown(KeyCode.V))
        {
            DestruirPiedra();
        }
    }

    void DestruirPiedra()
    {
        // Destruir la piedra grande
        Destroy(gameObject);

        // Instanciar la versi�n peque�a
        GameObject Item = Instantiate(DropPrefab, transform.position, Quaternion.identity);

        // A�adir el componente que la har� flotar y rotar
        Item.AddComponent<FlotarYRotar>();
    }
}
