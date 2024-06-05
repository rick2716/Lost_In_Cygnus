using UnityEngine;

public class DropEnemigo : MonoBehaviour
{
    public GameObject DropPrefab1; // El prefab de la versión pequeña de la piedra
    public GameObject DropPrefab2; // El prefab de la versión pequeña de la piedra
    public float separacion = 0.5f; // Distancia de separación entre los drops
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

    public void DestruirPiedra()
    {

        // Calcular posiciones para los drops
        Vector3 posicionDrop1 = transform.position + new Vector3(-separacion, 0, 0);
        Vector3 posicionDrop2 = transform.position + new Vector3(separacion, 0, 0);

        // Instanciar los drops
        GameObject Item1 = Instantiate(DropPrefab1, posicionDrop1, Quaternion.identity);
        GameObject Item2 = Instantiate(DropPrefab2, posicionDrop2, Quaternion.identity);

        // Añadir el componente que los hará flotar y rotar
        Item1.AddComponent<FlotarYRotar>();
        Item2.AddComponent<FlotarYRotar>();


        // Destruir este GameObject
        Destroy(gameObject);
    }
}
