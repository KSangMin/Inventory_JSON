//ĳ���� ���� ���̺� �ε� �뵵
using System;
using System.Collections.Generic;

[Serializable]
public abstract class ISaveLoader<Key, Value>
{
    public abstract Dictionary<Key, Value> MakeDict();
    public List<Value> data = new List<Value>();
}

[Serializable]
public class CharacterSaveData
{
    public int id;
    public string name;
    public int gold;
    public int characterDataIndex;
    public List<int> inventory;
    public List<bool> equipped;
}

[Serializable]
public class CharacterSaveDataLoader : ISaveLoader<int, CharacterSaveData>
{
    public override Dictionary<int, CharacterSaveData> MakeDict()
    {
        Dictionary<int, CharacterSaveData> dict = new Dictionary<int, CharacterSaveData>();
        foreach(CharacterSaveData character in data)
        {
            dict.Add(character.id, character);
        }

        return dict;
    }
}
