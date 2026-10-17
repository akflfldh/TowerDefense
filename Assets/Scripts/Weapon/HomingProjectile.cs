using UnityEngine;

public class HomingProjectile : MonoBehaviour
{

    [SerializeField ,Min(0.1f)]
    private float moveSpeed = 8f;

    private GameManager gameManager;

    private int damage;

    private EnemyHealth target;


   public void Initialize(EnemyHealth target, int damage , GameManager gameManager)
    {

        this.target = target;
        this.damage = damage;
        this.gameManager = gameManager;

    }



    // Update is called once per frame
    void Update()
    {
        if (gameManager == null ||
           gameManager.State == GameState.GameOver ||
           gameManager.State == GameState.Victory)
        {
            Destroy(gameObject);
            return;
        }


        if (gameManager.State != GameState.Playing)
            return;

        // 적이 먼저 죽거나 기지에 도착한 경우
        if (target == null || target.CurrentHealth <= 0f)
        {
            Destroy(gameObject);
            return;
        }



        Vector2 current = transform.position;
        Vector2 destination = target.transform.position;
        Vector2 direction = destination - current;

        float step = moveSpeed * Time.deltaTime;

        // 이번 프레임에 목표 지점까지 도달
        if(direction.magnitude <= step)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Vector2 nextPosition = Vector2.MoveTowards(current, destination, step);

        transform.position = new Vector3(nextPosition.x , nextPosition.y ,transform.position.z);

        

        //화살이 기본 (1,0,0) 방향이라고 가정하고 right설정으로 방향 수정   
        transform.right = direction.normalized;

    }
}
