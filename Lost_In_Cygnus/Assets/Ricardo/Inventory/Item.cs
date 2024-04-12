using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public bool stackable = true;
}

public enum ItemType
{
    Energy,
    Flora,
    Animal,
    Resource,
    Fuel
}
