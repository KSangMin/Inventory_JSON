using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public DataManager data = new DataManager();

    public Dictionary<int, CharacterSaveData> characterSaveDict = new Dictionary<int, CharacterSaveData>();

    public void Init()
    {
        data.Init();

        characterSaveDict = LoadJson<CharacterSaveDataLoader, int, CharacterSaveData>(nameof(CharacterSaveData)).MakeDict();     
    }

    Loader LoadJson<Loader, Key, Value>(string fileName) where Loader : ISaveLoader<Key, Value>, new()
    {
        string path = $"{Application.persistentDataPath}/{fileName}.json";
        if (!System.IO.File.Exists(path))
        {
            Debug.LogWarning($"파일을 찾을 수 없습니다. 새로 생성합니다: {path}");
            //새 캐릭터 생성해야 함
            Loader newLoader = new Loader();
            SaveDict<Loader, Key, Value>(newLoader.MakeDict(), fileName);
            return newLoader;
        }

        string json = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<Loader>(json);
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
}
