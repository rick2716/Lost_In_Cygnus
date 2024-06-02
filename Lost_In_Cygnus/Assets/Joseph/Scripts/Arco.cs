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

    private float currentEnergia; // Energía actual del arco
    private bool isCharging; // Indicador de si se está cargando el arco
    private float startChargeTime; // Tiempo en el que se empezó a cargar el arco

    void Start()
    {
        currentEnergia = maxEnergia;
        sliderEnergia.maxValue = maxEnergia; // Configura el valor máximo del slider
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
            InstantiateFlecha();
        }

        isCharging = false;
    }

    void InstantiateFlecha()
    {
        // Instantiate y configura la flecha
        GameObject flecha = Instantiate(flechaPrefab, transform.position, transform.rotation);
        // Aquí puedes añadir cualquier configuración adicional de la flecha, como la velocidad o la dirección de lanzamiento
    }
}
