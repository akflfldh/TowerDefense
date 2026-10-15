using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    /*
     -역할 
     Enemy 스폰 
     Enemy alive Count 
     Path 소유
     Wave 시작,종료 

    CheckWaveComplete 웨이브기 종료되면 GameManager?에서 플레이어의 생명수를 보고 패배인지 계속 진행이가능한지 판단해야할듯 

     */
    [SerializeField]
    private GameManager gameManager;


    [SerializeField]
    private RunStats runStats;



    [SerializeField]
    private BaseHealth baseHealth;

    [SerializeField]
    private EnemyMovement enemyPrefab;


    [SerializeField]
    private WayPointPath path;


    [SerializeField]
    private int baseEnemyCount = 5;


    [SerializeField]
    private float spawnInterval = 0.8f;


    [SerializeField ,Min(1)]
    private int maxWave = 10;

    public int MaxWave => maxWave;

    private int currentWave = 0;
    private int aliveEnemyCount = 0;

    private bool isSpawning = false;
    private bool waveActive = false;

    public bool isWaveRunning => waveActive || isSpawning;
    public int CurrentWave => currentWave;

    private readonly HashSet<EnemyMovement> activeEnemies = new();

    public event Action<int> OnWaveCompleted;


    void Start()
    {

    }

   void OnEnable()
    {

        if (gameManager != null)
            gameManager.OnStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
        if (gameManager != null)
            gameManager.OnStateChanged -= HandleGameStateChanged;

    }


    [ContextMenu("Start next wave")]
    public void BeginNextWave()
    {
        if (isWaveRunning)
            return;

        if (gameManager == null || gameManager.State != GameState.Playing)
            return;
        
        //아직 wave 진행중
        if (waveActive)
            return;


        //마지막 웨이브 이후는 시작 불가능
        if(currentWave >= maxWave)
        {
            Debug.Log("최대 웨이브 도달 ");
            return;

        }




        currentWave++;
        aliveEnemyCount = 0;
        isSpawning = true;
        waveActive = true;

        StartCoroutine(SpawnWave());


    }

    private IEnumerator SpawnWave()
    {
        int enemyCount = baseEnemyCount + (currentWave - 1) * 3;
        for(int i=0; i<enemyCount;++ i)
        {
            EnemyMovement enemy = Instantiate(enemyPrefab, path.GetPoint(0), Quaternion.identity);

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            enemy.OnReachedGoal += HandleReachedGoal;
            enemyHealth.OnDied += HandleEnemyDied;


            activeEnemies.Add(enemy);
            aliveEnemyCount++;

            enemy.Initialize(path);

            if (i < enemyCount - 1)
                yield return new WaitForSeconds(spawnInterval);


        }


        isSpawning = false;

        CheckWaveComplete();
    
    
    
    }


    private void HandleEnemyDied(EnemyHealth health)
    {
        EnemyMovement movement = health.GetComponent<EnemyMovement>();
        movement.enabled = false;
        if (RemoveEnemy(movement))
        {
            runStats?.RecordKill();
        }
    }

    private void HandleReachedGoal(EnemyMovement enemy)
    {

        if (!activeEnemies.Contains(enemy))
            return;

        //기지에 피해적용
        baseHealth.TakeDamage(1);


        //적 제거 
        RemoveEnemy(enemy);


    }

    private bool RemoveEnemy(EnemyMovement enemy)
    {
        //중복 처리 방지
        if (!activeEnemies.Remove(enemy))
            return false;


        EnemyHealth health =
           enemy.GetComponent<EnemyHealth>();


        health.OnDied -= HandleEnemyDied;
        enemy.OnReachedGoal -= HandleReachedGoal;

        aliveEnemyCount--;

        CheckWaveComplete();

        return true;

    }

    void CheckWaveComplete()
    {
        if (!waveActive)
            return;

        if (isSpawning)
            return;

        if (gameManager == null ||
       gameManager.State != GameState.Playing)
            return;

        if (aliveEnemyCount > 0)
            return;

        waveActive = false;
        
        Debug.Log($"Wave {currentWave} Completed!");


        //wave를 통과했을때만 호출되게된다. 
        //마지막 enemy가 들어오면서 base hp도 0일경우 HandleReachedGoal에서 base take damage로인해 먼저 기지가 파괴되고
        //gameState가 playing에서 전환됨으로 잘못된 victory를  방지한다.
        OnWaveCompleted?.Invoke(currentWave);
    }



    // Update is called once per frame
    void Update()
    {

    }

    void HandleGameStateChanged(GameState state)
    {
        if(state == GameState.GameOver)
        {


            StopWave();
        }


    }

    void StopWave()
    {
        //새로운 적 생성 중단
        StopAllCoroutines();

        waveActive = false;
        isSpawning = false;

        // 기존 적 이벤트 해제 및 제거

        foreach(EnemyMovement enemy in activeEnemies)
        {

            if (enemy == null)
                continue;

            //콜백 해제
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            if(health)
                health.OnDied -= HandleEnemyDied;

            enemy.OnReachedGoal -= HandleReachedGoal;

            Destroy(enemy.gameObject);

        }

        activeEnemies.Clear();
        aliveEnemyCount = 0;

    }
}
