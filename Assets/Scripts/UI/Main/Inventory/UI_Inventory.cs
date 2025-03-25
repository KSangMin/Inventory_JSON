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

    protected override void Awake()
    {
        base.Awake();

        Hide();

        closeButton.onClick.AddListener(Hide);
    }
}
