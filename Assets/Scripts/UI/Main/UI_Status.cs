using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : UI
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI critRateText;

    [SerializeField] private Button closeButton;

    protected override void Awake()
    {
        base.Awake();

        Hide();

        closeButton.onClick.AddListener(Hide);
    }
}
