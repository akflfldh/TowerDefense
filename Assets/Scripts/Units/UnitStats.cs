using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Scriptable Objects/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Identity")]
    public JobType JobType;

    public UnitRank Rank;


    public string UnitName;

    public string Description;

    public Sprite Icon;


    [Header("Damage")]
    public int Damage = 5;
    public float AttackRange = 3f;
    public float AttackInterval = 1f;


}
