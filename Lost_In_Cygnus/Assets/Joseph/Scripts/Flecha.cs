using UnityEngine;

public class Flecha : MonoBehaviour
{
    private Arco arco; // Referencia al arco que dispara la flecha

    // Método para establecer una referencia al arco
    public void SetearArco(Arco arco)
    {
        this.arco = arco;
    }

    // Cuando la flecha colisiona con otro objeto
    void OnTriggerEnter(Collider other)
    {
        // Si colisiona con un enemigo
        if (other.CompareTag("Enemy"))
        {
            if (arco != null)
            {
                // Causar daño al enemigo a través del arco
                arco.CausarDañoEnemigo(other);
            }
            Destroy(gameObject);
        }
    }
}
