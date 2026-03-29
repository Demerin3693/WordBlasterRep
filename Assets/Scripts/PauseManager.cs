using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject deathPanel;
    public GameObject settingsPanel;
    public GameObject helpPanel;
    public GameObject cdPanel;

    private bool isPaused = false;
    public KeepFocusTMP keepFocus;
    public TMP_InputField attackInput;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
        
    }

    public void PauseGame()
    {
        HideWords();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenSetting()
    {
        attackInput.DeactivateInputField();
        attackInput.shouldActivateOnSelect = false;
        attackInput.enabled = false;
        
        HideWords();
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        attackInput.enabled = true;
        attackInput.shouldActivateOnSelect = true;
        attackInput.ActivateInputField();


        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        ShowWords();
        isPaused = false;
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void DeathScreen()
    {
        HideWords();
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SlowDown()
    {
        SoundManager.instance.PlaySlowdown();
        Time.timeScale = 0.5f;
        StartCoroutine(TimeResetDelay(3.5f));
        StartCoroutine(CDResetDelay(15.0f));
        cdPanel.SetActive(true);
    }

     IEnumerator TimeResetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TimeReset();
    }

    IEnumerator CDResetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CDReset();
    }

    void TimeReset()
    {
        Time.timeScale = 1f;
    }

    void CDReset()
    {
        cdPanel.SetActive(false);
    }


   public void ShowWords()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies){enemy.wordText.alpha = 1f; enemy.translationText.alpha = 1f;}
    }
       public void HideWords()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies){enemy.wordText.alpha = 0f; enemy.translationText.alpha = 0f;}
    }

}
