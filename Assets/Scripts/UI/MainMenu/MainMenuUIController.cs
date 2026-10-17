using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{

    [SerializeField]
    private string battleSceneName = "Battle";

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject settingPanel;

    public void OnClickStart()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(battleSceneName);

    }


    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OpenSettingPanel()
    {
        mainMenuPanel.SetActive(false);
        settingPanel.SetActive(true);

    }


    public void BackToMainMenu()
    {
        settingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
