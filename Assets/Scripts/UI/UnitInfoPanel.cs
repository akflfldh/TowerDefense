using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class UnitInfoPanel : MonoBehaviour
{

    [Header("Manager")]
    [SerializeField] private GoldManager goldManager;
    [SerializeField] private UnitUpgradeController upgradeController;

    [Header("UI")]
    [SerializeField] private Image unitIcon;
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] private TMP_Text unitLevelText;
    [SerializeField] private TMP_Text unitDescriptionText;
    [SerializeField] private TMP_Text unitDamageText;
    [SerializeField] private TMP_Text unitRangeText;
    [SerializeField] private TMP_Text unitIntervalText;

    [SerializeField] private Button upgradeButton;

    [SerializeField] private TMP_Text upgradeCostText;



    private UnitRuntimeStats selectedUnit;

    public void Show(UnitRuntimeStats unit)
    {
        if (selectedUnit != null)
            selectedUnit.OnStatsChanged -= Refresh;

        selectedUnit = unit;

        if (selectedUnit == null)
        {
            Hide();
            return;
        }

        selectedUnit.OnStatsChanged += Refresh;
        gameObject.SetActive(true);
        Refresh();

    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Refresh()
    {
        if (selectedUnit == null)
            return;



        UnitStats stats = selectedUnit.BaseStats;

        if (stats == null) return;


        unitNameText.text = stats.UnitName;
        unitDescriptionText.text = stats.Description;

        unitIcon.sprite = stats.Icon;

        unitLevelText.text = $"Lv. {selectedUnit.Level}";
        unitDamageText.text = $"데미지 :    {selectedUnit.Damage}";
        unitRangeText.text = $"사거리 ;    {selectedUnit.AttackRange:F1}";

        unitIntervalText.text =
          $"공격 딜레이 : {selectedUnit.AttackInterval:F1}초";

        RefreshUpgradeUI();

    }

    void OnEnable()
    {

        if (goldManager != null)
            goldManager.OnGoldChanged += HadnleGoldChanged;

    }

    void OnDisable()
    {

        if (selectedUnit != null)
            selectedUnit.OnStatsChanged -= Refresh;

        selectedUnit = null;


        if (goldManager != null)
            goldManager.OnGoldChanged -= HadnleGoldChanged;

    }

    public void OnClickUpgradeDamage()
    {
        if (upgradeController == null || selectedUnit == null)
            return;

        if (upgradeController.TryUpgradeDamage(selectedUnit))
        {

            Refresh();

        }


    }


    void RefreshUpgradeUI()
    {
        if (goldManager == null || selectedUnit == null || upgradeController == null)
        {
            if (upgradeButton != null)
                upgradeButton.interactable = false;

            return;
        }

        int cost = upgradeController.GetDamageUpgradeCost(selectedUnit);

        upgradeCostText.text = $"공격력 +1 ({cost} G)";

        upgradeButton.interactable = goldManager.CurrentGold >= cost;

    }

    private void HadnleGoldChanged(int gold)
    {
        RefreshUpgradeUI();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
