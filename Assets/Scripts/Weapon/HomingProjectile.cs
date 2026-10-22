using UnityEngine;
using System.Collections;

public class HomingProjectile : MonoBehaviour
{

    [SerializeField, Min(0.1f)]
    private float moveSpeed = 8f;

    private GameManager gameManager;

    private int damage;

    private EnemyHealth target;

    private Vector2 targetLastPosition;

    private bool isFading = false;


    public void Initialize(EnemyHealth target, int damage, GameManager gameManager)
    {

        this.target = target;
        this.damage = damage;
        this.gameManager = gameManager;


        if (target != null)
            targetLastPosition = target.transform.position;



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

        if (isFading)
            return;


        Vector2 current = transform.position;
        Vector2 destination;

        // 적이 먼저 죽거나 기지에 도착한 경우 ( 그대로 target의 마지막 위치로 날라가고, 1초에 걸쳐 서서히 사라진다.)
        if (target == null || target.CurrentHealth <= 0f)
        {

            destination = targetLastPosition;
        }
        else
        {
            destination = target.transform.position;



        }

        Vector2 direction = destination - current;

        float step = moveSpeed * Time.deltaTime;

        // 이번 프레임에 목표 지점까지 도달
        if (direction.magnitude <= step)
        {
            if (target != null)
            {
                //target에 적중하면 바로 제거
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                //적중하지못했다. (적이 이미죽었거나 등등 )
                //0.5초에 걸쳐 서서히 사라진다.
                StartCoroutine(FadeOut());

            }


            return;
        }

        Vector2 nextPosition = Vector2.MoveTowards(current, destination, step);

        transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);

        targetLastPosition = destination;

        //화살이 기본 (1,0,0) 방향이라고 가정하고 right설정으로 방향 수정   
        transform.right = direction.normalized;

    }


    private IEnumerator FadeOut()
    {
        isFading = true;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite == null)
        {
            Destroy(gameObject);
            yield break;
        }


        Color color = sprite.color;
        float startAlpha = color.a;

        const float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(startAlpha, 0.0f, elapsed / duration);

            sprite.color = color;


            yield return null;

        }

        Destroy(gameObject);

    }

}
