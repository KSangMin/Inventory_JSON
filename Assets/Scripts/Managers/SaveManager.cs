using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public DataManager data = new DataManager();

    public override void Awake()
    {
        base.Awake();

        data.Init();
    }
}
