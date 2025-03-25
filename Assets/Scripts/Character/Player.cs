using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterSaveData _data;
    [SerializeField] private int _selectedCharacterIndex = 0;

    public Action<CharacterSaveData> OnCharacterChanged;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    private void Start()
    {
        OnCharacterChanged -= UIManager.Instance.GetUI<UI_Info>().SetCharacterData;
        OnCharacterChanged -= UIManager.Instance.GetUI<UI_Status>().SetCharacterData;
        OnCharacterChanged -= UIManager.Instance.GetUI<UI_Inventory>().SetCharacterData;

        OnCharacterChanged += UIManager.Instance.GetUI<UI_Info>().SetCharacterData;
        OnCharacterChanged += UIManager.Instance.GetUI<UI_Status>().SetCharacterData;
        OnCharacterChanged += UIManager.Instance.GetUI<UI_Inventory>().SetCharacterData;

        UIManager.Instance.GetUI<UI_Info>().OnSetNextCharacter -= SetNextCharacter;
        UIManager.Instance.GetUI<UI_Inventory>().OnRandomButtonClicked -= GetRandomItem;

        UIManager.Instance.GetUI<UI_Info>().OnSetNextCharacter += SetNextCharacter;
        UIManager.Instance.GetUI<UI_Inventory>().OnRandomButtonClicked += GetRandomItem;

        LoadCharacter();
    }

    [ContextMenu("Load")]
    public void LoadCharacter()
    {
        _data = SaveManager.Instance.characterSaveDict[_selectedCharacterIndex];

        OnCharacterChanged?.Invoke(_data);
    }

    [ContextMenu("Save")]
    public void SaveCharacter()
    {
        SaveManager.Instance.SaveAll();
    }

    void SetNextCharacter()
    {
        int count = SaveManager.Instance.characterSaveDict.Count;
        _selectedCharacterIndex++;
        _selectedCharacterIndex %= count;
        LoadCharacter();
    }

    public void AddItem(int itemId)
    {
        _data.inventory.Add(itemId);
        _data.equipped.Add(false);

        UIManager.Instance.GetUI<UI_Inventory>().AddSlotandItem(SaveManager.Instance.data.ItemDict[itemId]);
    }

    public void RemoveItem(int inventoryIndex)
    {
        _data.equipped.RemoveAt(inventoryIndex);
        _data.inventory.RemoveAt(inventoryIndex);
        UIManager.Instance.GetUI<UI_Inventory>().RemoveSlot(inventoryIndex);
    }

    public void EquipItem(int inventoryIndex)
    {
        _data.equipped[inventoryIndex] = true;
    }

    public void UnequipItem(int inventoryIndex)
    {
        _data.equipped[inventoryIndex] = false;
    }

    public void GetRandomItem()
    {
        int index = UnityEngine.Random.Range(0, SaveManager.Instance.data.ItemDict.Count);
        AddItem(index);
    } 
}
