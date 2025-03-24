using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    public int index = -1;
    //public ItemData itemData;
    [SerializeField] private Image Icon;
    [SerializeField] private GameObject selectedMark;
    [SerializeField] private GameObject equippedMark;
}
