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
    [SerializeField] Button InventoryButton;
}
