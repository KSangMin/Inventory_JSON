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
    {//UI�� �θ� �Ǵ� ��Ʈ Ʈ�������� ��ȯ�ϸ�, ������ �����մϴ�.
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
    {//UI �Ŵ����� ��ϵ� Ư�� Ÿ���� UI�� ã�� ��ȯ�մϴ�.
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            return existingUI as T;
        }

        throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    public T ShowPopupUI<T>() where T : UI_Popup
    {//�˾� UI�� �ν��Ͻ�ȭ�ϰ� ǥ���� ��, �˾� ���ÿ� �߰�
        Type uiType = typeof(T);

        T ui = Util.InstantiatePrefabAndGetComponent<T>(path: $"UI/{uiType.Name}", parent: Root);
        ui.SetCanvasOrder(_popupOrder++);
        _popupUIs.Push(ui);

        return ui;
    }

    public void RemovePopupUI()
    {//�˾� ���ÿ��� �ֻ��� �˾� UI�� �����ϰ� �ش� ���� ������Ʈ�� �ı�.
        if (_popupUIs.Count <= 0) return;

        UI_Popup popup = _popupUIs.Pop();
        Destroy(popup.gameObject);
        _popupOrder--;
        return;
    }

    public T HideUI<T>() where T : UI
    {//UI SetActive(false); ������ ����� false
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
    {//�θ� ������ ��� �� �� ���
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
    {//�θ� �������� �ʾƵ� �� �� ���
        return ShowUI<T>(Root);
    }

    //�ٸ� Ŭ�����鿡�� ȣ���ϴ� �޼���
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

    //UI�� Close���� ȣ���ϴ� �޼���
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
