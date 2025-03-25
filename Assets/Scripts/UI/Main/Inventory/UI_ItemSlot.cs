using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    public int index = -1;
    //public ItemData itemData;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject selectedMark;
    [SerializeField] private GameObject equippedMark;
    public Item curItem;

    public void SetSlot(int index, Item item, bool isEquipped)
    {
        this.index = index;
        curItem = item;
        icon.sprite = curItem.icon;
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
