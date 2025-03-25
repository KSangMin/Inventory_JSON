using UnityEngine;

public class Scene_Main : Scene
{
    protected override void Init()
    {
        base.Init();

        SaveManager.Instance.Init();

        UIManager.Instance.ShowUI<UI_Info>();
        UIManager.Instance.ShowUI<UI_Status>();
        UIManager.Instance.ShowUI<UI_Inventory>();
    }
}
