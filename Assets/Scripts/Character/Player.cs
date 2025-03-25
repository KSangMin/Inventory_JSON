using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterSaveData _data;
    public int selectedCharacterIndex = 0;

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

        LoadCharacter();

        OnCharacterChanged?.Invoke(_data);
    }

    [ContextMenu("Load")]
    public void LoadCharacter()
    {
        _data = SaveManager.Instance.characterSaveDict[selectedCharacterIndex];
    }

    [ContextMenu("Save")]
    public void SaveCharacter()
    {
        SaveManager.Instance.SaveAll();
    }

    public void AddItem(int itemId)
    {
        _data.inventory.Add(itemId);
        _data.equipped.Add(false);
    }

    public void RemoveItem(int inventoryIndex)
    {
        _data.equipped[inventoryIndex] = false;
        _data.inventory.RemoveAt(inventoryIndex);
    }

    public void EquipItem(int inventoryIndex)
    {
        _data.equipped[inventoryIndex] = true;
    }

    public void UnequipItem(int inventoryIndex)
    {
        _data.equipped[inventoryIndex] = false;
    }
}
