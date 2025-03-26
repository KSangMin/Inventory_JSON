using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private GameObject selectedMark;
    [SerializeField] private GameObject equippedMark;
    public Item curItem;

    public void ClearSlot()
    {
        curItem = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        selectedMark.SetActive(false);
        equippedMark.SetActive(false);
    }

    public void SetSlot(Item item, bool isEquipped)
    {
        curItem = item;
        icon.sprite = curItem.icon;
        icon.gameObject.SetActive(true);
        equippedMark.SetActive(isEquipped);
    }

    public void Equipped()
    {
        equippedMark.SetActive(true);
    }

    public void UnEquipped()
    {
        equippedMark.SetActive(false);
    }
}
