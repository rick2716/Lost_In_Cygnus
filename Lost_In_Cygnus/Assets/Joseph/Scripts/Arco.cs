using UnityEngine;
using UnityEngine.UI;

public class Arco : MonoBehaviour
{
    public Slider sliderEnergia; // Referencia al slider de energía
    public float maxEnergia = 100f; // Valor máximo de energía
    public float regeneracionPorSegundo = 10f; // Velocidad de regeneración de energía por segundo
    public float minDuracionClic = 0.1f; // Duración mínima de clic para usar energía
    public float maxDuracionClic = 1.0f; // Duración máxima de clic para usar energía
    public float energiaUsadaPorSegundo = 20f; // Cantidad de energía usada por segundo al cargar el arco
    public GameObject flechaPrefab; // Prefab de la flecha
    public Transform puntoDisparo; // Punto desde donde se disparan las flechas
    public float fuerzaDisparo = 10f; // Fuerza con la que se dispara la flecha

    private float currentEnergia; // Energía actual del arco
    private bool isCharging; // Indicador de si se está cargando el arco
    private float startChargeTime; // Tiempo en el que se empezó a cargar el arco

    void Start()
    {
        currentEnergia = maxEnergia;
        sliderEnergia.maxValue = maxEnergia; // Configura el valor máximo del slider
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

        // Recargar energía automáticamente
        currentEnergia = Mathf.Clamp(currentEnergia + regeneracionPorSegundo * Time.deltaTime, 0f, maxEnergia);

        // Actualizar el slider de energía
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
            // Calcula la fuerza de disparo basada en la duración de la carga
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
