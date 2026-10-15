using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{

    [SerializeField]
    private string battleSceneName = "Battle";



    public void OnClickStart()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(battleSceneName);

    }


    public void OnClickQuit()
    {
        Application.Quit();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
