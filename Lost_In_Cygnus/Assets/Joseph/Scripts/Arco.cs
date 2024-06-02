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

    private float currentEnergia; // Energ�a actual del arco
    private bool isCharging; // Indicador de si se est� cargando el arco
    private float startChargeTime; // Tiempo en el que se empez� a cargar el arco

    void Start()
    {
        currentEnergia = maxEnergia;
        sliderEnergia.maxValue = maxEnergia; // Configura el valor m�ximo del slider
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
            InstantiateFlecha();
        }

        isCharging = false;
    }

    void InstantiateFlecha()
    {
        // Instantiate y configura la flecha
        GameObject flecha = Instantiate(flechaPrefab, transform.position, transform.rotation);
        // Aqu� puedes a�adir cualquier configuraci�n adicional de la flecha, como la velocidad o la direcci�n de lanzamiento
    }
}
