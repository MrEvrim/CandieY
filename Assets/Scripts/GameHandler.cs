using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject pausePanel; 
    public TextMeshProUGUI killCounterText; 

    private bool isPaused = false;
    private int killCount = 0;

    void Update()
    {
        UpdateKillCounterText();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void OnButtonClick()
    {
        //geri açıldıgında 
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    void PauseGame()
    {
        pausePanel.SetActive(true); 
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        pausePanel.SetActive(false); 
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void IncrementKillCount()
    {
        killCount++;
        UpdateKillCounterText();
    }

    void UpdateKillCounterText()
    {
        if (killCounterText != null)
        {
            killCounterText.text = "Kills: " + killCount;
        }
    }

    void LateUpdate()
    {
        UpdateKillCounterText();
    }
}
