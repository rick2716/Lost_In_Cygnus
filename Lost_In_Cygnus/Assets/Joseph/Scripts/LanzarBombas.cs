using UnityEngine;

public class LanzarBombas: MonoBehaviour
{
    public GameObject bombaPrefab; // El prefab de la bomba
    public Transform puntoDeLanzamiento; // El punto desde donde se lanzar�n las bombas
    public float fuerzaLanzamiento = 10f; // La fuerza con la que se lanzar�n las bombas
    public Camera camara; // La c�mara que sigue al personaje

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 es el bot�n izquierdo del mouse
        {
            LanzarBomba();
        }
    }

    void LanzarBomba()
    {
        // Instanciar la bomba en el punto de lanzamiento
        GameObject bomba = Instantiate(bombaPrefab, puntoDeLanzamiento.position, puntoDeLanzamiento.rotation);

        // Obtener el Rigidbody de la bomba
        Rigidbody rb = bomba.GetComponent<Rigidbody>();

        // Calcular la direcci�n de lanzamiento usando la direcci�n de la c�mara
        Vector3 direccionLanzamiento = camara.transform.forward;
        direccionLanzamiento.y = Mathf.Max(direccionLanzamiento.y, 0.1f); // Asegurar que siempre haya un componente hacia arriba

        // Aplicar la fuerza en la direcci�n de lanzamiento
        rb.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.Impulse);
    }
}
