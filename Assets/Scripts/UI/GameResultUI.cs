using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameResultUI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text resultTitle;

    [SerializeField]
    private TMP_Text waveText;
    [SerializeField]
    private TMP_Text killCountText;
    [SerializeField]
    private TMP_Text playTimeText;


    [Header("Scenes")]
    [SerializeField]
    private string mainMenuSceneName = "MainMenu";


    [SerializeField]
    private WaveManager waveManager;


    [SerializeField]
    private RunStats runStats;


    void Awake()
    {


    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(bool victory)
    {
        

        if (resultTitle != null)
            resultTitle.text = victory ? "승리" : "패배";

        if(runStats !=null)
        {
            if(killCountText!=null)
            {

                killCountText.text = $"죽인 적의 수 : {runStats.KillCount}";

            }

            if(playTimeText!=null)
            {
                int totalSeconds = Mathf.FloorToInt(runStats.ElapsedTime);
                int min = totalSeconds / 60;
                int sec = totalSeconds % 60;

                playTimeText.text = $"플레이 타임 : {min:D2}:{sec:D2}";
            }

        }



        if (waveManager != null && waveText != null)
        {
            int currentWave = waveManager.CurrentWave;

            waveText.text = $"웨이브 :  {waveManager.CurrentWave}/{waveManager.MaxWave}";
        }

        gameObject.SetActive(true);

    }

    public void OnClickRetry()
    {
        // 일시정지 상태가 남아 있지 않도록 복원
        Time.timeScale = 1f;
        // 현재 전투 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);

    }
}
