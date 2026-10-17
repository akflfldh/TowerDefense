using System;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;

public class UnitRuntimeStats : MonoBehaviour
{

    [SerializeField]
    private UnitStats baseStats;

    private int level = 1;
    private int bonusDamage = 0;


    public UnitStats BaseStats =>baseStats;

    public int Level => level;

    public int Damage => baseStats.Damage + bonusDamage;

    public float AttackRange => baseStats.AttackRange;
    public float AttackInterval => baseStats.AttackInterval;


    public event Action OnStatsChanged;


    //Level Up 


    public void UpgradeDamage()
    {
        if (baseStats == null)
            return;

        bonusDamage++;
        level++;

        OnStatsChanged?.Invoke();

    }

    [ContextMenu("Test Damage Upgrade")]
    private void TestUpgrade()
    {
        if (!Application.isPlaying)
            return;

        UpgradeDamage();
        Debug.Log($"{baseStats.UnitName} Lv.{level}/ Damage: {Damage}");

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
