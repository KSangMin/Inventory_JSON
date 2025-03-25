using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info : UI
{
    [SerializeField] private TextMeshProUGUI goldText;

    [SerializeField] private Image characterImage;
    
    [SerializeField] private TextMeshProUGUI characterName;

    [SerializeField] private TextMeshProUGUI characterLevel;
    [SerializeField] private Image expImage;
    [SerializeField] private TextMeshProUGUI expText;

    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] Button statusButton;
    [SerializeField] Button inventoryButton;

    private CharacterSaveData _curCharacter;

    protected override void Awake()
    {  
        base.Awake();

        statusButton.onClick.AddListener(() => UIManager.Instance.ShowUI<UI_Status>());
        inventoryButton.onClick.AddListener(() => UIManager.Instance.ShowUI<UI_Inventory>());
    }

    public void SetCharacterData(CharacterSaveData data)
    {
        goldText.text = data.gold.ToString();
        characterName.text = data.name;
        characterLevel.text = data.level.ToString();
        expImage.fillAmount = (float)data.exp / data.maxExp;
        expText.text = $"{data.exp.ToString()} / {data.maxExp.ToString()}";
        infoText.text = data.GetInfo();
    }
}
