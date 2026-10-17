using UnityEngine;

public class UnitUpgradeController : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GoldManager goldManager;

    public int GetDamageUpgradeCost(UnitRuntimeStats unit)
    {
        if (unit == null)
            return 0;

        return unit.Level * 10;



    }

    public bool TryUpgradeDamage(UnitRuntimeStats unit)
    {

        if (unit == null || goldManager == null || gameManager == null || gameManager.State != GameState.Playing)
            return false;


        int cost = GetDamageUpgradeCost(unit);


        if(!goldManager.TrySpendGold(cost))
        {

            Debug.Log("골드 부족, 업그레이드 실패");
            return false;
        }


        unit.UpgradeDamage();
        Debug.Log("유닛 업그레이드");
        return true;

    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
