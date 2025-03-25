using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UI
{
    [SerializeField] private TextMeshProUGUI quantityText;

    [SerializeField] private Transform inventoryTransform;
    [SerializeField] private List<UI_ItemSlot> slots = new List<UI_ItemSlot>();

    [SerializeField] private Button closeButton;
    [SerializeField] private Button randomItemButton;
    public Action OnRandomButtonClicked;

    protected override void Awake()
    {
        base.Awake();

        Hide();

        closeButton.onClick.AddListener(Hide);
        randomItemButton.onClick.AddListener(() => OnRandomButtonClicked?.Invoke());

        ClearInventory();
    }

    public void SetCharacterData(CharacterSaveData data)
    {
        quantityText.text = $"<color=orange>{data.inventory.Count}</color> / <color=grey>{data.maxInventoryQuantity}</color>";

        SetInventory(data);
    }

    void SetInventory(CharacterSaveData data)
    {
        ClearInventory();

        for(int i = 0; i < data.inventory.Count; i++)
        {
            Item item = SaveManager.Instance.data.ItemDict[data.inventory[i]];
            slots.Add(Util.InstantiatePrefabAndGetComponent<UI_ItemSlot>(path: $"UI/{nameof(UI_ItemSlot)}", parent: inventoryTransform));
            slots[i].SetSlot(i, item, data.equipped[i]);
        }
    }

    void ClearInventory()
    {
        foreach(Transform child in inventoryTransform)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();
    }

    public void AddSlotandItem(Item item)
    {
        var slot = Util.InstantiatePrefabAndGetComponent<UI_ItemSlot>(path: $"UI/{nameof(UI_ItemSlot)}", parent: inventoryTransform);
        slot.SetSlot(slots.Count, item, false);
        slots.Add(slot);
    }

    public void RemoveSlot(int index)
    {
        for(int i = index + 1; i < slots.Count; i++)
        {
            slots[i].index--;
        }

        var slot = slots[index];
        slots.RemoveAt(index);
        Destroy(slot.gameObject);
    }
}
