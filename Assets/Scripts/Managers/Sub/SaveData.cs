//캐릭터 정보 세이브 로드 용도
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

    public CharacterSaveData()
    {
        id = 0;
        name = "Chad";
        gold = 20000;
        characterDataIndex = 0;
        inventory = new List<int>();
        equipped = new List<bool>();
    }
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

    public CharacterSaveDataLoader()
    {
        data.Add(new CharacterSaveData());
    }
}
