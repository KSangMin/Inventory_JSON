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

    public void SetCharacterData(CharacterSaveData data)
    {
        attackText.text = data.GetAttack().ToString();
        defenseText.text = data.GetDefense().ToString();
        hpText.text = data.GetHP().ToString();
        critRateText.text = data.GetCritRate().ToString();
    }
}
