using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDrawPool", menuName = "Scriptable Objects/UnitDrawPool")]
public class UnitDrawPool : ScriptableObject
{

    [SerializeField] private UnitRank rank;
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private int summonCost;

    public UnitRank Rank => rank;
    public IReadOnlyList<GameObject> Prefabs => prefabs;

    public int SummonCost => summonCost;

    
}
