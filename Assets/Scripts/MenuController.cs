using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;       
    [SerializeField]
    private Button howToPlayButton;   
    [SerializeField]
    private Button exitButton;        
    [SerializeField]
    private Button backButton;        

    [SerializeField]
    private GameObject mainMenuPanel; 
    [SerializeField]
    private GameObject howToPlayPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);

        startButton.onClick.AddListener(OnStartButtonClicked);
        howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainGameScene");  
    }

    private void OnHowToPlayButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    private void OnBackButtonClicked()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
