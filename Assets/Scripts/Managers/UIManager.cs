using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, UI> _sceneDict = new Dictionary<Type, UI>();
    private Stack<UI_Popup> _popupUIs = new Stack<UI_Popup>();

    private int _popupOrder = 10;

    Transform _root;
    Transform Root
    {//UI의 부모가 되는 루트 트랜스폼을 반환하며, 없으면 생성합니다.
        get
        {
            if(_root == null)
            {
                _root = new GameObject("@UI_Root").transform;
            }
            return _root;
        }
    }

    public T GetUI<T>() where T : UI
    {//UI 매니저에 등록된 특정 타입의 UI를 찾아 반환합니다.
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            return existingUI as T;
        }

        throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    public T ShowPopupUI<T>() where T : UI_Popup
    {//팝업 UI를 인스턴스화하고 표시한 후, 팝업 스택에 추가
        Type uiType = typeof(T);

        T ui = Util.InstantiatePrefabAndGetComponent<T>(path: $"UI/{uiType.Name}", parent: Root);
        ui.SetCanvasOrder(_popupOrder++);
        _popupUIs.Push(ui);

        return ui;
    }

    public void RemovePopupUI()
    {//팝업 스택에서 최상위 팝업 UI를 제거하고 해당 게임 오브젝트를 파괴.
        if (_popupUIs.Count <= 0) return;

        UI_Popup popup = _popupUIs.Pop();
        Destroy(popup.gameObject);
        _popupOrder--;
        return;
    }

    public T HideUI<T>() where T : UI
    {//UI SetActive(false); 없으면 만들고 false
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            existingUI.Hide();
            return existingUI as T;
        }

        T ui = Util.InstantiatePrefabAndGetComponent<T>(path: $"UI/{uiType.Name}", parent: Root);
        _sceneDict[uiType] = ui;
        ui.Hide();

        return null;
    }

    public T ShowUI<T>(Transform par) where T : UI
    {//부모를 설정해 줘야 할 때 사용
        if (par == null) return ShowUI<T>();

        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            existingUI.Show();
            return existingUI as T;
        }

        T ui = Util.InstantiatePrefabAndGetComponent<T>(path: $"UI/{uiType.Name}", parent: par);
        _sceneDict[uiType] = ui;

        return ui;
    }

    public T ShowUI<T>() where T : UI
    {//부모 설정하지 않아도 될 때 사용
        return ShowUI<T>(Root);
    }

    //다른 클래스들에서 호출하는 메서드
    public void RemoveUI<T>() where T: UI
    {
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            _sceneDict.Remove(uiType);
            Destroy(existingUI.gameObject);
            return;
        }
        else throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    //UI의 Close에서 호출하는 메서드
    public void RemoveUI(UI ui)
    {
        Type uiType = ui.GetType();

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            _sceneDict.Remove(uiType);
            Destroy(existingUI.gameObject);
            return;
        }
        else throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    public void RemoveAllUI()
    {
        foreach (UI ui in _sceneDict.Values.ToList())
        {
            Destroy(ui.gameObject);
        }
        _sceneDict.Clear();

        while (_popupUIs.Count > 0)
        {
            RemovePopupUI();
        }
    }

    public void Clear()
    {
        RemoveAllUI();
        Destroy(Root.gameObject);
        _root = null;
    }
}
