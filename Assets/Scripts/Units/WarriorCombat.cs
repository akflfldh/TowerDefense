using UnityEngine;

public class WarriorCombat : MonoBehaviour , IUnitInitializer
{

    private Animator animator;

    [Header("Reference")]
     private GameManager gameManager;
    [SerializeField] private Transform attackOrigin;


    [Header("Combat")]
    [SerializeField] private LayerMask enemyLayer;

    private UnitRuntimeStats unitStats;


    private EnemyHealth currentTarget;

    private float nextAttackTime = 0.0f;
    private bool isAttacking = false;
    private bool hitApplied = false;




    private static readonly int AttackHash =
       Animator.StringToHash("Attack");

    void Awake()
    {
        animator = GetComponent<Animator>();
        unitStats = GetComponent<UnitRuntimeStats>();
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager == null || gameManager.State != GameState.Playing)
            return;



        if (isAttacking || Time.time < nextAttackTime)
            return;

        //공격범위에있는 적 찾기
        EnemyHealth target = FindTarget();

        if (target == null)
        {
            currentTarget = null;
            return;
        }

        //공격 시작
        currentTarget = target;
        isAttacking = true;
        hitApplied = false;

        nextAttackTime = Time.time + unitStats.AttackInterval;
        Debug.Log("Attack Trigger 호출!");
        animator.SetTrigger(AttackHash);

        Debug.Log($"Animator: {animator.name}");
        Debug.Log($"Controller: {animator.runtimeAnimatorController?.name}");
        Debug.Log($"Attack Trigger Hash: {AttackHash}");

    }
    

    //실제 타격 에니메이션 프레임에서 호출
    public void OnAttackHit()
    {
        if (!isAttacking || hitApplied)
            return;

        if (gameManager == null || gameManager.State != GameState.Playing)
            return;

        hitApplied = true;

        //타격직전까지 포착한 적이 유효한지 검사 (다른 타워가 이미 적을 죽였을수도있기에)
        if (currentTarget == null)
            return;

        float distance = Vector2.Distance(GetAttackPosition(), currentTarget.transform.position);

        // 타격 시점에도 사거리 안에 있는지 검사
        if (distance > unitStats.AttackRange)
            return;


        currentTarget.TakeDamage(unitStats.Damage);
    }

    // Attack 애니메이션의 마지막 프레임에서 호출
    public void OnAttackFinished()
    {
        isAttacking = false;
        currentTarget = null;
    }

    EnemyHealth FindTarget()
    {
        Vector2 origin = GetAttackPosition();
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, unitStats.AttackRange, enemyLayer);


        EnemyHealth nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach(Collider2D hit  in hits)
        {
            EnemyHealth enemy =            hit.GetComponent<EnemyHealth>();

            if (enemy == null || enemy.CurrentHealth <= 0.0f)
                continue;


            float distance = ((Vector2)enemy.transform.position - origin).sqrMagnitude;

            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }

        }

        Debug.Log("Find Enemy!");
        return nearestEnemy;

    }

    Vector2 GetAttackPosition()
    {

        return attackOrigin != null ? attackOrigin.position : transform.position;

    }


    //공격범위 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            attackOrigin != null
                ? attackOrigin.position
                : transform.position,
            unitStats.AttackRange
        );
    }
}
