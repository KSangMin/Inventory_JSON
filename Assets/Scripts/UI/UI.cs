using UnityEngine;

public class UI : MonoBehaviour
{
    private GameObject _panel;

    protected virtual void Awake()
    {
        _panel = transform.GetChild(0).gameObject;
    }

    public virtual void Show()
    {
        _panel.SetActive(true);
    }

    public virtual void Hide()
    {
        _panel.SetActive(false);
    }

    public virtual void Close()
    {
        UIManager.Instance.RemoveUI(this);
    }
}
