using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public Image[] hearts;          // Масив сердечок
    public Sprite fullHeart;        // Спрайт повного сердечка
    public Sprite emptyHeart;       // Спрайт порожнього сердечка

    public PauseManager pauseManager;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    public void TakeDamage(int amount)
    {
        SoundManager.instance.PlayExplosion();
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHeartsUI();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }


    
    void Die()
    {
        SoundManager.instance.PlayDefeat();
        Debug.Log("Player DIED!");
        pauseManager.DeathScreen();
    }
}
