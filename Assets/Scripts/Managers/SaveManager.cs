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
            throw new System.NullReferenceException($"파일을 찾을 수 없습니다. 새로 생성합니다: {path}");
            //Debug.LogWarning($"파일을 찾을 수 없습니다. 새로 생성합니다: {path}");
            //Loader newLoader = new Loader();
            //SaveDict<Loader, Key, Value>(newLoader.MakeDict(), fileName); // 빈 데이터 저장
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
            Debug.LogWarning($"캐릭터 파일을 찾을 수 없습니다. 새로 생성합니다: {path}");
            CharacterSaveDataLoader newLoader = new CharacterSaveDataLoader();
            SaveDict<CharacterSaveDataLoader, int, CharacterSaveData>(newLoader.MakeDict(), nameof(CharacterSaveData)); // 빈 데이터 저장
            return newLoader;
        }

        string json = System.IO.File.ReadAllText(path);
        Debug.Log($"캐릭터 파일을 로드했습니다: {path}");
        return JsonUtility.FromJson<CharacterSaveDataLoader>(json);
    }

    public void SaveDict<Loader, Key, Value>(Dictionary<Key, Value> dict, string fileName) where Loader : ISaveLoader<Key, Value> , new()
    {
        Loader loader = new Loader();
        loader.data = new List<Value>(dict.Values);

        string json = JsonUtility.ToJson(loader, true);

        string path = $"{Application.persistentDataPath}/{fileName}.json";
        System.IO.File.WriteAllText(path, json);

        Debug.Log($"{path} 저장 완료");
    }

    public void SaveAll()
    {
        SaveDict<CharacterSaveDataLoader, int, CharacterSaveData>(characterSaveDict, nameof(CharacterSaveData));
    }
}
