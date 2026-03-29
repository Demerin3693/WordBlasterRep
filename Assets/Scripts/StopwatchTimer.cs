using UnityEngine;
using TMPro;

public class StopwatchTimer : MonoBehaviour
{
    public TMP_Text timerText;     
    public bool isRunning = false;

    private float timeElapsed = 0f;

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        isRunning = false;
        timeElapsed = 0f;
        UpdateTimerUI();
    }
}
