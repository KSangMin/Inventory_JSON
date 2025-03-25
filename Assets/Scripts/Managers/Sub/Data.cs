//캐릭터, 아이템 등의 초기값 로드 용도
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

#region CharacterData

[Serializable]
public class Character
{
    public int id;
    public Sprite icon;
    public string description;
    public int attack;
    public int defense;
    public int hp;
    public int critRate;

    public Character(CharacterData character)
    {
        id = character.id;
        icon = Resources.Load<Sprite>($"Faces/Face_{character.id}");
        description = character.description;
        attack = character.attack;
        defense = character.defense;
        hp = character.hp;
        critRate = character.critRate;
    }
}

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
public class Item
{
    public int id;
    public string displayName;
    public Sprite icon;
    public string description;
    public int attack;
    public int defense;
    public int hp;
    public int critRate;

    public Item(ItemData item)
    {
        id = item.id;
        displayName = item.name;
        icon = Resources.Load<Sprite>($"Items/{item.spriteName}");
        description = item.description;
        attack = item.attack;
        defense = item.defense;
        hp = item.hp;
        critRate = item.critRate;
    }

    public Item(Item item)
    {//인벤토리의 아이템이 서로 다른 스탯, 내구도 등을 가지고 있어야 할 때 사용
        id = item.id;
        displayName = item.displayName;
        icon = item.icon;
        description = item.description;
        attack = item.attack;
        defense = item.defense;
        hp = item.hp;
        critRate = item.critRate;
    }
}

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