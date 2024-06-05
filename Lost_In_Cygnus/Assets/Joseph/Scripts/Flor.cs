using UnityEngine;

public class Flor: MonoBehaviour
{
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
        if (cercaDelJugador && Input.GetKeyDown(KeyCode.F))
        {
            RecogerFlor();
        }
    }

    void RecogerFlor()
    {
        // Aquí puedes añadir lógica para lo que pasa cuando recoges la flor
        Debug.Log("Flor recogida");
        Destroy(gameObject); // Esto destruirá la flor
    }
}
