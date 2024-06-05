using UnityEngine;

public class FlotarYRotar: MonoBehaviour
{
    public float velocidadRotacion = 50f;
    public float amplitud = 0.5f;
    public float frecuencia = 1f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Rotación
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);

        // Movimiento de flotación
        float nuevaAltura = Mathf.Sin(Time.time * frecuencia) * amplitud;
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y + nuevaAltura, posicionInicial.z);
    }
}
