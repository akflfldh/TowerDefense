using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{


    [Header("Enemy")]
    [SerializeField]
    private EnemyType enemyType;



    [SerializeField]
    private EnemyMovement enemyPrefab;




    [Header("Stat Multipliers")]
    [SerializeField,Min(0.1f)]
    private float healthMultiplier = 1.0f;



    [SerializeField, Min(0.1f)]
    private float speedMultiplier = 1.0f;



    public EnemyType EnemyType=> enemyType;
    public EnemyMovement Prefab => enemyPrefab;



    public float HealthMultiplier => healthMultiplier;
    public float SpeedMultiplier => speedMultiplier;


}
