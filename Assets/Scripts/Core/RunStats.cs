using UnityEngine;




/*
 
현재 플레이 전투의 정보를 기록, 관리 한다 . 
-죽인 적의 수
-플레이 시간



 */
public class RunStats : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;


    public float ElapsedTime { get; private set; }
    public int KillCount { get; private set; }


    public void RecordKill()
    {
        KillCount++;

    }

    public void ResetForRun()
    {
        KillCount = 0;
        ElapsedTime = 0.0f;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null)
            return;

        if (gameManager.State != GameState.Playing)
            return;


        //플레이중에만 시간 누적  //정지나,게임종료,등은 무시됨 
        ElapsedTime += Time.deltaTime;
    }
}
