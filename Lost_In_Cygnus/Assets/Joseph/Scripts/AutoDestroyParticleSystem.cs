using UnityEngine;
using System.Collections;


public class AutoDestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(CheckIfAliveAndDestroy());
    }

    private IEnumerator CheckIfAliveAndDestroy()
    {
        while (ps != null && ps.IsAlive())
        {
            yield return null; // Espera un frame y vuelve a comprobar
        }

        // Espera 8 segundos despu�s de que el sistema de part�culas haya terminado
        yield return new WaitForSeconds(8f);

        // Destruye el objeto
        Destroy(gameObject);
    }
}
