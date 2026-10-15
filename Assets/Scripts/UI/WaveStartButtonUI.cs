using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveStartButtonUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private WaveManager waveManager;


    [SerializeField]
    private TMP_Text buttonText;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(HandleClick);

        if(gameManager !=null)
        {
            gameManager.OnStateChanged += HandleGameStateChanged;
        }

        if(waveManager !=null)
        {
            waveManager.OnWaveCompleted += HandleWaveCompleted;
        }
        Refresh();
    }


    void OnDisable()
    {
        button.onClick.RemoveListener(HandleClick);

        if (gameManager != null)
        {
            gameManager.OnStateChanged -= HandleGameStateChanged;
        }

        if (waveManager != null)
        {
            waveManager.OnWaveCompleted -= HandleWaveCompleted;
        }

    }



    void HandleClick()
    {
        if (gameManager == null || waveManager == null)
            return;

        if ( gameManager.State != GameState.Playing)
            return;

        if (waveManager.isWaveRunning)
            return;


        waveManager.BeginNextWave();

        Refresh();

    }

    void HandleWaveCompleted(int wave)
    {

        Refresh();

    }

    void HandleGameStateChanged(GameState state)
    {

        Refresh();

    }

    void Refresh()
    {
        if (gameManager == null || waveManager == null)
        {
            button.interactable = false;
            return;
        }

        bool isPlaying =
           gameManager.State == GameState.Playing;

        bool waveRunning =
            waveManager.isWaveRunning;


        button.interactable =
         isPlaying && !waveRunning;


        if (buttonText == null)
            return;
        if (waveRunning)
        {
            buttonText.text =
                $"웨이브 {waveManager.CurrentWave} 진행 중";
        }
        else
        {
            buttonText.text =
                $"웨이브 {waveManager.CurrentWave + 1} 시작";
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
