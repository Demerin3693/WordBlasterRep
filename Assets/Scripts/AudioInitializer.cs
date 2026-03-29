using UnityEngine;

public class AudioInitializer : MonoBehaviour
{
    public GameSettings settings;      // GameSettings.asset
    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        ApplyVolumes();
    }

    void ApplyVolumes()
    {
        // Головна гучність
        AudioListener.volume = settings.masterVolume;

        // Якщо немає — просто пропускаємо
        if (musicSource != null)
            musicSource.volume = settings.musicVolume;

        if (sfxSource != null)
            sfxSource.volume = settings.sfxVolume;
    }
}
