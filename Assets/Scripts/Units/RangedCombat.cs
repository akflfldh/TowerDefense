using UnityEngine;

public class RangedCombat : MonoBehaviour, IUnitInitializer
{

    [Header("Reference")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private HomingProjectile projectilePrefab;

    [Header("Combat")]
    [SerializeField] private LayerMask enemyLayer;

     private UnitRuntimeStats unitStats;





    private GameManager gameManager;
    private Animator animator;
    private EnemyHealth currentTarget;

    private float nextAttackTime;
    private bool isAttacking;
    private bool projectileFired;



    private static int AttackHash = Animator.StringToHash("Attack");

    void Start()
    {

    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        unitStats = GetComponent<UnitRuntimeStats>();
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("Combat update 시작");

        if (gameManager == null || gameManager.State != GameState.Playing)
            return;

        Debug.Log("Combat attack 가능여부");
        if (isAttacking || Time.time < nextAttackTime)
            return;

        Debug.Log("타켓 체크");
        EnemyHealth target = FindTarget();


        if (target == null)
            return;

        Debug.Log("Enemy 타켓 찾았다.!");

        currentTarget = target;
        isAttacking = true;
        projectileFired = false;

        nextAttackTime = Time.time + unitStats.AttackInterval;

        animator.SetTrigger(AttackHash);


    }

    EnemyHealth FindTarget()
    {

        Vector2 origin = transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, unitStats.AttackRange, enemyLayer);


        EnemyHealth nearestEnemy = null;
        float nearestDistance = float.MaxValue;
        foreach (Collider2D hit in hits)
        {

            EnemyHealth enemy = hit.GetComponentInParent<EnemyHealth>();

            if (enemy == null || enemy.CurrentHealth <= 0)
            {
                continue;
            }

            float distance = ((Vector2)enemy.transform.position - origin).sqrMagnitude;

            if(nearestDistance > distance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;

            }


        }

        return nearestEnemy;


    }

    //Attack Animation Event
    public void OnShootProjecttile()
    {

        //공격중이 아니거나, 이미  발사했다면 리턴
        if (!isAttacking || projectileFired)
            return;


        if(gameManager == null || gameManager.State !=GameState.Playing)
        {
            return;
        }

        projectileFired = true;


        if (currentTarget == null ||
           currentTarget.CurrentHealth <= 0f ||
           projectilePrefab == null)
            return;

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;

        HomingProjectile homingProjectile =        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        homingProjectile.Initialize(currentTarget, unitStats.Damage, gameManager);
        Debug.Log("HomingProjectile Spawn");

    }



    //Attack Animation Event
    public void OnAttackFinished()
    {
        isAttacking = false;
        currentTarget = null;

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, unitStats.AttackRange);

    }


}
