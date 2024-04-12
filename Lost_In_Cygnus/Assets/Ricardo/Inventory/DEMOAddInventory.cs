using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMOAddInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Added");
        }
        else
        {
            Debug.Log("Not added");
        }
    }
}
