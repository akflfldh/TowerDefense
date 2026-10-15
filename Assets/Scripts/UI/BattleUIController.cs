using UnityEngine;
/*
 
전투 씬의  UI 총 책임역할  

 */ 

public class BattleUIController : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;




    [SerializeField]
    private GameResultUI resultUI;


    [SerializeField]
    private GameObject pausePanel;



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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void HandleGameStateChanged(GameState state)
    {

        if(state == GameState.Victory || state ==GameState.GameOver)
        {

            if (resultUI != null)
                resultUI.Show(state == GameState.Victory);

        }

    }



    public void OpenPause()
    {

        if (gameManager == null ||
       gameManager.State != GameState.Playing)
            return;


        gameManager.PauseGame();


        CloseAllPanels();

        pausePanel.SetActive(true);

    }


    public void ResumeFromPause()
    {


        if (gameManager == null ||
       gameManager.State != GameState.Paused)
            return;



        pausePanel.SetActive(false);
        gameManager.ResumeGame();



    }


    void CloseAllPanels()
    {
        if (resultUI != null)
            resultUI.gameObject.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

    }

}
