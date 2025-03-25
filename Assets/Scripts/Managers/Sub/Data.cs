//ĳ����, ������ ���� �ʱⰪ �ε� �뵵
using System;
using System.Collections.Generic;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

#region CharacterData

[Serializable]
public class CharacterData
{
    public int id;
    public string description;
    public int attack;
    public int defense;
    public int hp;
    public int critRate;
}

public class CharacterDataLoader : ILoader<int, CharacterData>
{
    public List<CharacterData> data = new List<CharacterData>();

    public Dictionary<int, CharacterData> MakeDict()
    {
        Dictionary<int, CharacterData> dict = new Dictionary<int, CharacterData>();
        foreach (CharacterData character in data)
        {
            dict.Add(character.id, character);
        }

        return dict;
    }
}

#endregion

#region ItemData

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public string spriteName;
    public string description;
    public int attack;
    public int defense;
    public int hp;
    public int critRate;
}

[Serializable]
public class ItemDataLoader : ILoader<int, ItemData>
{
    public List<ItemData> data = new List<ItemData>();

    public Dictionary<int, ItemData> MakeDict()
    {
        Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();
        foreach(ItemData item in data)
        {
            dict.Add(item.id, item);
        }

        return dict;
    }
}

#endregion