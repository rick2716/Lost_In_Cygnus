using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject inventory;
    public bool isOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            inventory.SetActive(true);
        }
        else if (isOpen && Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            inventory.SetActive(false);
        }
    }
}
