using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRecolectable : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item itemToPickup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemToPickup);
            Destroy(gameObject);
        }
    }
}
