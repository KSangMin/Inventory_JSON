using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, ItemData> ItemDict {  get; private set; } = new Dictionary<int, ItemData>();
    public Dictionary<int, CharacterData> characterDict {  get; private set; } = new Dictionary<int, CharacterData>();

    public void Init()
    {
        ItemDict = LoadJson<ItemDataLoader, int, ItemData>(nameof(ItemData)).MakeDict();
        characterDict = LoadJson<CharacterDataLoader, int, CharacterData>(nameof(CharacterData)).MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
