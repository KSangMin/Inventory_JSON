using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterSaveData _data;
    [SerializeField] private int _selectedCharacterIndex = 0;

    public Action<CharacterSaveData> OnCharacterChanged;
    public Action<CharacterSaveData> OnEquipChanged;
    [SerializeField] private EventChannel<int> OnEquipChangedEvent;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    private void Start()
    {
        OnCharacterChanged -= UIManager.Instance.GetUI<UI_Info>().SetCharacterData;
        OnCharacterChanged -= UIManager.Instance.GetUI<UI_Status>().SetCharacterData;
        OnCharacterChanged -= UIManager.Instance.GetUI<UI_Inventory>().SetCharacterData;
        OnEquipChanged -= UIManager.Instance.GetUI<UI_Status>().SetCharacterData;
        OnEquipChangedEvent.UnregisterListener(ToggleItem);
        UIManager.Instance.GetUI<UI_Info>().OnSetNextCharacter -= SetNextCharacter;
        UIManager.Instance.GetUI<UI_Inventory>().OnRandomButtonClicked -= GetRandomItem;

        OnCharacterChanged += UIManager.Instance.GetUI<UI_Info>().SetCharacterData;
        OnCharacterChanged += UIManager.Instance.GetUI<UI_Status>().SetCharacterData;
        OnCharacterChanged += UIManager.Instance.GetUI<UI_Inventory>().SetCharacterData;
        OnEquipChanged += UIManager.Instance.GetUI<UI_Status>().SetCharacterData;
        OnEquipChangedEvent.RegisterListener(ToggleItem);
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

        UIManager.Instance.GetUI<UI_Inventory>().AddItem(SaveManager.Instance.data.ItemDict[itemId]);
    }

    public void RemoveItem(int inventoryIndex)
    {
        _data.equipped.RemoveAt(inventoryIndex);
        _data.inventory.RemoveAt(inventoryIndex);
        UIManager.Instance.GetUI<UI_Inventory>().RemoveItem(inventoryIndex);
    }

    public void ToggleItem(int inventoryIndex)
    {
        _data.equipped[inventoryIndex] = !_data.equipped[inventoryIndex];
        OnEquipChanged?.Invoke(_data);
    }

    public void GetRandomItem()
    {
        int index = UnityEngine.Random.Range(0, SaveManager.Instance.data.ItemDict.Count);
        AddItem(index);
    } 
}
