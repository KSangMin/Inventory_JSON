using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    public int index;
    [SerializeField] private Image icon;
    [SerializeField] private Button selectedMark;
    [SerializeField] private GameObject equippedMark;
    public Item curItem;
    [SerializeField] private EventChannel<int> OnEquipToggle;

    private void Awake()
    {
        selectedMark.onClick.AddListener(ToggleEquip);
    }

    public void ClearSlot()
    {
        curItem = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        selectedMark.gameObject.SetActive(false);
        equippedMark.SetActive(false);
    }

    public void SetSlot(int index, Item item, bool isEquipped)
    {
        this.index = index;
        curItem = item;
        icon.sprite = curItem.icon;
        icon.gameObject.SetActive(true);
        selectedMark.gameObject.SetActive(true);
        equippedMark.SetActive(isEquipped);
    }

    public void BindEquipListener(Action<int> listener)
    {
        OnEquipToggle.UnregisterListener(listener);
        OnEquipToggle.RegisterListener(listener);
    }

    public void ToggleEquip()
    {
        OnEquipToggle?.RaiseEvent(index);
        equippedMark.SetActive(!equippedMark.activeSelf);
    }
}
