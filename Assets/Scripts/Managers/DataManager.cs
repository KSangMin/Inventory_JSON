using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, Item> ItemDict { get; private set; } = new Dictionary<int, Item>();
    public Dictionary<int, Character> characterDict {  get; private set; } = new Dictionary<int, Character>();

    public void Init()
    {
        LoadItem();
        LoadCharacter();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    void LoadItem()
    {
        Dictionary<int, ItemData> ItemDataDict = LoadJson<ItemDataLoader, int, ItemData>(nameof(ItemData)).MakeDict();

        foreach (ItemData item in ItemDataDict.Values)
        {
            ItemDict[item.id] = new Item(item);
        }
    }

    void LoadCharacter()
    {
        Dictionary<int, CharacterData> characterDataDict = LoadJson<CharacterDataLoader, int, CharacterData>(nameof(CharacterData)).MakeDict();

        foreach (CharacterData character in characterDataDict.Values)
        {
            characterDict[character.id] = new Character(character);
        }
    }
}
