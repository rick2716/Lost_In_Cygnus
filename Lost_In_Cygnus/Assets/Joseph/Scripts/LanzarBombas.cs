using UnityEngine;
using System.Collections;

public class LanzarBombas : MonoBehaviour
{
    public GameObject bombaPrefab; // El prefab de la bomba
    public Transform puntoDeLanzamiento; // El punto desde donde se lanzarán las bombas
    public float fuerzaLanzamiento = 10f; // La fuerza con la que se lanzarán las bombas
    public Camera camara; // La cámara que sigue al personaje

    private bool puedeLanzar = true; // Controla si se puede lanzar una bomba

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && puedeLanzar) // 0 es el botón izquierdo del mouse
        {
            StartCoroutine(LanzarBomba());
        }
    }

    IEnumerator LanzarBomba()
    {
        puedeLanzar = false; // Desactivar lanzamiento

        // Instanciar la bomba en el punto de lanzamiento
        GameObject bomba = Instantiate(bombaPrefab, puntoDeLanzamiento.position, puntoDeLanzamiento.rotation);

        // Obtener el Rigidbody de la bomba
        Rigidbody rb = bomba.GetComponent<Rigidbody>();

        // Calcular la dirección de lanzamiento usando la dirección de la cámara
        Vector3 direccionLanzamiento = camara.transform.forward;
        direccionLanzamiento.y = Mathf.Max(direccionLanzamiento.y, 0.1f); // Asegurar que siempre haya un componente hacia arriba

        // Aplicar la fuerza en la dirección de lanzamiento
        rb.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.Impulse);

        // Esperar 1 segundo
        yield return new WaitForSeconds(1f);

        puedeLanzar = true; // Permitir lanzamiento de nuevo
    }
}
