using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Arco : MonoBehaviour
{
    public Slider sliderEnergia;
    public float maxEnergia = 100f;
    public float regeneracionPorSegundo = 10f;
    public float minDuracionClic = 0.1f;
    public float maxDuracionClic = 1.0f;
    public float energiaUsadaPorSegundo = 20f;
    public GameObject flechaPrefab;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 10f;
    public int daño = 20; // Cantidad de daño que hace la bomba

    private float currentEnergia;
    private bool isCharging;
    private float startChargeTime;
    private bool puedeDisparar = true;

    void Start()
    {
        currentEnergia = maxEnergia;
        sliderEnergia.maxValue = maxEnergia;
        sliderEnergia.value = currentEnergia;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && puedeDisparar)
        {
            StartCargaArco();
        }
        else if (Input.GetMouseButtonUp(0) && isCharging)
        {
            StartCoroutine(DispararFlecha());
        }

        currentEnergia = Mathf.Clamp(currentEnergia + regeneracionPorSegundo * Time.deltaTime, 0f, maxEnergia);

        sliderEnergia.value = currentEnergia;
    }

    void StartCargaArco()
    {
        isCharging = true;
        startChargeTime = Time.time;
    }

    IEnumerator DispararFlecha()
    {
        if (isCharging)
        {
            float chargeDuration = Time.time - startChargeTime;
            float energiaUsada = Mathf.Lerp(0, energiaUsadaPorSegundo, Mathf.Clamp01(chargeDuration / maxDuracionClic));
            currentEnergia -= energiaUsada;
            InstantiateFlecha(chargeDuration);
        }

        isCharging = false;
        puedeDisparar = false;

        yield return new WaitForSeconds(1f);

        puedeDisparar = true;
    }

    void InstantiateFlecha(float chargeDuration)
    {
        GameObject flecha = Instantiate(flechaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = flecha.GetComponent<Rigidbody>();

        if (rb != null)
        {
            float fuerza = Mathf.Lerp(0, fuerzaDisparo, Mathf.Clamp01(chargeDuration / maxDuracionClic));
            rb.AddForce(puntoDisparo.forward * fuerza, ForceMode.Impulse);
            Debug.Log("Gravedad habilitada en la flecha: " + rb.useGravity);

            // Para causar daño al enemigo cuando la flecha colisiona
            Flecha flechaScript = flecha.GetComponent<Flecha>();
            if (flechaScript != null)
            {
                flechaScript.SetearArco(this); // Establece una referencia al arco para que la flecha pueda causar daño al enemigo
            }
            else
            {
                Debug.LogError("El prefab de la flecha no tiene un componente Flecha.");
            }
        }
        else
        {
            Debug.LogError("El prefab de la flecha no tiene un componente Rigidbody.");
        }

        Destroy(flecha, 8f);
    }

    // Método para causar daño al enemigo
    public void CausarDañoEnemigo(Collider enemigoCollider)
    {
        EnemyHealth enemyHealth = enemigoCollider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(daño);
        }
    }
}
