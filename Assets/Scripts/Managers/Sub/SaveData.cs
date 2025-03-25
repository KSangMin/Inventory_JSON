//캐릭터 정보 세이브 로드 용도
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public int level;
    public int exp;
    public int maxExp;
    public int maxInventoryQuantity;
    public List<int> inventory;
    public List<bool> equipped;

    public CharacterSaveData()
    {
        id = 0;
        name = "Temp";
        gold = 0;
        characterDataIndex = 0;
        level = 0;
        exp = 0;
        maxExp = 10;
        maxInventoryQuantity = 120;
        inventory = new List<int>();
        equipped = new List<bool>();
    }

    public string GetInfo()
    {
        return SaveManager.Instance.data.characterDict[characterDataIndex].description;
    }

    public int GetAttack()
    {
        int sum = SaveManager.Instance.data.characterDict[characterDataIndex].attack;

        for (int i = 0; i <  inventory.Count; i++)
        {
            if (equipped[i]) sum += SaveManager.Instance.data.ItemDict[inventory[i]].attack;
        }

        return sum;
    }

    public int GetDefense()
    {
        int sum = SaveManager.Instance.data.characterDict[characterDataIndex].defense;

        for (int i = 0; i <  inventory.Count; i++)
        {
            if (equipped[i]) sum += SaveManager.Instance.data.ItemDict[inventory[i]].defense;
        }

        return sum;
    }

    public int GetHP()
    {
        int sum = SaveManager.Instance.data.characterDict[characterDataIndex].hp;

        for (int i = 0; i <  inventory.Count; i++)
        {
            if (equipped[i]) sum += SaveManager.Instance.data.ItemDict[inventory[i]].hp;
        }

        return sum;
    }

    public int GetCritRate()
    {
        int sum = SaveManager.Instance.data.characterDict[characterDataIndex].critRate;

        for (int i = 0; i <  inventory.Count; i++)
        {
            if (equipped[i]) sum += SaveManager.Instance.data.ItemDict[inventory[i]].critRate;
        }

        return sum;
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
