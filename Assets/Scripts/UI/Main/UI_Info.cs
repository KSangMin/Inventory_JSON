using System;
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

    [SerializeField] Button saveButton;
    [SerializeField] Button statusButton;
    [SerializeField] Button inventoryButton;
    [SerializeField] Button nextCharacterButton;
    public Action OnSetNextCharacter;

    protected override void Awake()
    {  
        base.Awake();

        saveButton.onClick.AddListener(() => SaveManager.Instance.SaveAll());
        statusButton.onClick.AddListener(() => UIManager.Instance.ShowUI<UI_Status>());
        inventoryButton.onClick.AddListener(() => UIManager.Instance.ShowUI<UI_Inventory>());
        nextCharacterButton.onClick.AddListener(() => OnSetNextCharacter?.Invoke());
    }

    public void SetCharacterData(CharacterSaveData data)
    {
        goldText.text = data.gold.ToString();
        characterImage.sprite = SaveManager.Instance.data.characterDict[data.characterDataIndex].icon;
        characterName.text = data.name;
        characterLevel.text = data.level.ToString();
        expImage.fillAmount = (float)data.exp / data.maxExp;
        expText.text = $"{data.exp.ToString()} / {data.maxExp.ToString()}";
        infoText.text = data.GetInfo();
    }
}
