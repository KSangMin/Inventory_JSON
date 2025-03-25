using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterSaveData data;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    public void AddItem(int itemId)
    {
        data.inventory.Add(itemId);
        data.equipped.Add(false);
    }

    public void RemoveItem(int inventoryIndex)
    {
        data.equipped[inventoryIndex] = false;
        data.inventory.RemoveAt(inventoryIndex);
    }

    public void EquipItem(int inventoryIndex)
    {
        data.equipped[inventoryIndex] = true;
    }

    public void UnequipItem(int inventoryIndex)
    {
        data.equipped[inventoryIndex] = false;
    }
}
