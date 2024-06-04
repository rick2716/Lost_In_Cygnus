using UnityEngine;

public class DropEnemigo: MonoBehaviour
{
    public GameObject DropPrefab; // El prefab de la versión pequeña de la piedra
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

        // Instanciar la versión pequeña
        GameObject Item = Instantiate(DropPrefab, transform.position, Quaternion.identity);

        // Añadir el componente que la hará flotar y rotar
        Item.AddComponent<FlotarYRotar>();
    }
}
