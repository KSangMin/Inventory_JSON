using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public DataManager data = new DataManager();

    public Dictionary<int, CharacterSaveData> characterSaveDict = new Dictionary<int, CharacterSaveData>();

    public void Init()
    {
        data.Init();

        characterSaveDict = LoadCharacters().MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string fileName) where Loader : ISaveLoader<Key, Value>, new()
    {
        string path = $"{Application.persistentDataPath}/{fileName}.json";
        if (!System.IO.File.Exists(path))
        {
            throw new System.NullReferenceException($"������ ã�� �� �����ϴ�. ���� �����մϴ�: {path}");
            //Debug.LogWarning($"������ ã�� �� �����ϴ�. ���� �����մϴ�: {path}");
            //Loader newLoader = new Loader();
            //SaveDict<Loader, Key, Value>(newLoader.MakeDict(), fileName); // �� ������ ����
            //return newLoader;
        }

        string json = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<Loader>(json);
    }

    CharacterSaveDataLoader LoadCharacters()
    {
        string path = $"{Application.persistentDataPath}/{nameof(CharacterSaveData)}.json";
        if (!System.IO.File.Exists(path))
        {
            Debug.LogWarning($"ĳ���� ������ ã�� �� �����ϴ�. ���� �����մϴ�: {path}");
            CharacterSaveDataLoader newLoader = new CharacterSaveDataLoader();
            SaveDict<CharacterSaveDataLoader, int, CharacterSaveData>(newLoader.MakeDict(), nameof(CharacterSaveData)); // �� ������ ����
            return newLoader;
        }

        string json = System.IO.File.ReadAllText(path);
        Debug.Log($"ĳ���� ������ �ε��߽��ϴ�: {path}");
        return JsonUtility.FromJson<CharacterSaveDataLoader>(json);
    }

    public void SaveDict<Loader, Key, Value>(Dictionary<Key, Value> dict, string fileName) where Loader : ISaveLoader<Key, Value> , new()
    {
        Loader loader = new Loader();
        loader.data = new List<Value>(dict.Values);

        string json = JsonUtility.ToJson(loader, true);

        string path = $"{Application.persistentDataPath}/{fileName}.json";
        System.IO.File.WriteAllText(path, json);

        Debug.Log($"{path} ���� �Ϸ�");
    }

    public void SaveAll()
    {
        SaveDict<CharacterSaveDataLoader, int, CharacterSaveData>(characterSaveDict, nameof(CharacterSaveData));
    }
}
