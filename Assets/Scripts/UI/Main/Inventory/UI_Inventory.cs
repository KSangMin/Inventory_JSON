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
    [SerializeField] private int slotCount = 20;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button randomItemButton;
    public Action OnRandomButtonClicked;

    protected override void Awake()
    {
        base.Awake();

        Hide();

        closeButton.onClick.AddListener(Hide);
        randomItemButton.onClick.AddListener(() => OnRandomButtonClicked?.Invoke());

        ClearInventorySlot();
    }

    public void SetCharacterData(CharacterSaveData data)
    {
        ClearInventoryItem();

        quantityText.text = $"<color=orange>{data.inventory.Count}</color> / <color=grey>{data.maxInventoryQuantity}</color>";

        for (int i = 0; i < data.inventory.Count; i++)
        {
            Item item = SaveManager.Instance.data.ItemDict[data.inventory[i]];
            slots[i].SetSlot(i, item, data.equipped[i]);
        }
    }

    void ClearInventorySlot()
    {
        foreach(Transform child in inventoryTransform)
        {
            Destroy(child.gameObject);
        }

        slots.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(Util.InstantiatePrefabAndGetComponent<UI_ItemSlot>(path: $"UI/{nameof(UI_ItemSlot)}", parent: inventoryTransform));
            slots[i].ClearSlot();
        }
    }

    void ClearInventoryItem()
    {
        for (int i = 0; i < slotCount; i++)
        {
            slots[i].ClearSlot();
        }
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < slotCount; i++)
        {
            if (slots[i].curItem == null)
            {
                slots[i].SetSlot(i, item, false);
                return;
            }
        }
    }

    public void RemoveItem(int index)
    {
        var slot = slots[index];
        slot.ClearSlot();
    }
}
