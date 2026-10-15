using System;
using UnityEngine;


/*
 * 

GameManager의 가장 중요한 역할은 게임 전체의 진행 상태를 관리하고, 다른 시스템들이 게임의 진행 상태에 맞게 동작하도록 조율하는 것. 


Enemy가 없어지는경우 - 사망, 도달 을 구분해야한다.

 
 *
 */

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private BaseHealth baseHealth;

    [SerializeField]
    private WaveManager waveManager;


    [SerializeField]
    private WaveRewardManager waveRewardManager;


    public GameState State { get; private set; } = GameState.Ready;


    



    public event Action<GameState> OnStateChanged;

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        if (State != GameState.Ready)
            return;


        if(baseHealth == null || waveManager == null)
        {
            Debug.Log("GameManager에 baseHealth,waveManager바인딩이 필요함!");
            return;
        }


        //초기화 및 시작 보상(스타터팩 제공) 
        waveRewardManager.InitializeRun();

        ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        if (State != GameState.Playing)
            return;


        ChangeState(GameState.GameOver);

    }

    void OnEnable()
    {

        if (baseHealth != null)
            baseHealth.OnBaseDestroyed += HandleBaseDestroyed;

        if (waveManager != null)
            waveManager.OnWaveCompleted += HandleWaveCompleted;

    }

    void OnDisable()
    {
        if (baseHealth != null)
            baseHealth.OnBaseDestroyed -= HandleBaseDestroyed;

        if (waveManager != null)
            waveManager.OnWaveCompleted -= HandleWaveCompleted;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleBaseDestroyed()
    {
        GameOver();
    }
    

    private void HandleWaveCompleted(int wave)
    {

        if (State != GameState.Playing)
            return;


        Debug.Log($"Wave {wave} Completed!");


        if(wave == waveManager.MaxWave  && baseHealth.CurrentHealth >0 )
        {
            Victory();
        }

    }

   public  void Victory()
    {
        if (State != GameState.Playing)
            return;

        Debug.Log("GAME VICTORY!");

        ChangeState(GameState.Victory);

    }

    private void ChangeState(GameState newState)
    {
        if (State == newState)
            return;

        State = newState;

        OnStateChanged?.Invoke(State);

    }

    public void PauseGame()
    {

        if (State != GameState.Playing)
            return;

        ChangeState(GameState.Paused);

        Time.timeScale = 0.0f;

    }
    
    public void ResumeGame()
    {
        if (State != GameState.Paused)
            return;





        Time.timeScale = 1.0f;


        ChangeState(GameState.Playing);
    }
}
