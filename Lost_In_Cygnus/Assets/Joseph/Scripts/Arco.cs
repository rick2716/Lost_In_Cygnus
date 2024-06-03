using UnityEngine;
using UnityEngine.UI;

public class Arco : MonoBehaviour
{
    public Slider sliderEnergia; // Referencia al slider de energ�a
    public float maxEnergia = 100f; // Valor m�ximo de energ�a
    public float regeneracionPorSegundo = 10f; // Velocidad de regeneraci�n de energ�a por segundo
    public float minDuracionClic = 0.1f; // Duraci�n m�nima de clic para usar energ�a
    public float maxDuracionClic = 1.0f; // Duraci�n m�xima de clic para usar energ�a
    public float energiaUsadaPorSegundo = 20f; // Cantidad de energ�a usada por segundo al cargar el arco
    public GameObject flechaPrefab; // Prefab de la flecha
    public Transform puntoDisparo; // Punto desde donde se disparan las flechas
    public float fuerzaDisparo = 10f; // Fuerza con la que se dispara la flecha

    private float currentEnergia; // Energ�a actual del arco
    private bool isCharging; // Indicador de si se est� cargando el arco
    private float startChargeTime; // Tiempo en el que se empez� a cargar el arco

    void Start()
    {
        currentEnergia = maxEnergia;
        sliderEnergia.maxValue = maxEnergia; // Configura el valor m�ximo del slider
        sliderEnergia.value = currentEnergia; // Inicializa el valor del slider
    }

    void Update()
    {
        // Controlar el disparo del arco
        if (Input.GetMouseButtonDown(0))
        {
            StartCargaArco();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DispararFlecha();
        }

        // Recargar energ�a autom�ticamente
        currentEnergia = Mathf.Clamp(currentEnergia + regeneracionPorSegundo * Time.deltaTime, 0f, maxEnergia);

        // Actualizar el slider de energ�a
        sliderEnergia.value = currentEnergia;
    }

    void StartCargaArco()
    {
        isCharging = true;
        startChargeTime = Time.time;
    }

    void DispararFlecha()
    {
        if (isCharging)
        {
            float chargeDuration = Time.time - startChargeTime;
            float energiaUsada = Mathf.Lerp(0, energiaUsadaPorSegundo, Mathf.Clamp01(chargeDuration / maxDuracionClic));
            currentEnergia -= energiaUsada;
            InstantiateFlecha(chargeDuration);
        }

        isCharging = false;
    }

    void InstantiateFlecha(float chargeDuration)
    {
        // Instantiate y configura la flecha
        GameObject flecha = Instantiate(flechaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = flecha.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Calcula la fuerza de disparo basada en la duraci�n de la carga
            float fuerza = Mathf.Lerp(0, fuerzaDisparo, Mathf.Clamp01(chargeDuration / maxDuracionClic));
            rb.AddForce(puntoDisparo.forward * fuerza, ForceMode.Impulse);

            // Debugging: Verificar que el Rigidbody tiene gravedad habilitada
            Debug.Log("Gravedad habilitada en la flecha: " + rb.useGravity);
        }
        else
        {
            Debug.LogError("El prefab de la flecha no tiene un componente Rigidbody.");
        }
    }
}
