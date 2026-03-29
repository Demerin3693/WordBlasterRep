using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("References")]
    public GameSettings settings;    // GameSettings.asset

    [Header("Input fields (Text values)")]
    public TMP_InputField spawnIntervalField;
    public TMP_InputField maxEnemiesField;
    public TMP_InputField initialSpawnField;

    public TMP_InputField movementSpeedField;

    public Toggle translationToggle;
    public Toggle transcriptionToggle;

    [Header("Slider (volume)")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Optional: Live objects")]
    public LevelManager levelManager;
    public EnemyMovement enemyMovement;


    public AudioSource musicSource;
    public AudioSource sfxSource;
    void Start()
    {
        LoadValuesIntoUI();
        AudioListener.volume = settings.masterVolume;
        musicSource.volume = settings.musicVolume;
        sfxSource.volume = settings.sfxVolume;
    }


    // ---------- LOAD ----------
    public void LoadValuesIntoUI()
    {
        spawnIntervalField.text = settings.spawnInterval.ToString();
        maxEnemiesField.text = settings.maxEnemies.ToString();
        initialSpawnField.text = settings.initialSpawn.ToString();
        movementSpeedField.text = settings.moveSpeed.ToString();
        translationToggle.isOn = settings.enableTranslation;
        transcriptionToggle.isOn = settings.EnglishToUkrainian;
        masterVolumeSlider.value = settings.masterVolume;
        musicVolumeSlider.value = settings.musicVolume;
        sfxVolumeSlider.value = settings.sfxVolume;
    }


    // ---------- APPLY ----------
    public void ApplySettings()
    {
        float parsedFloat;
        int parsedInt;

        if (float.TryParse(spawnIntervalField.text, out parsedFloat))
            settings.spawnInterval = parsedFloat;

        if (float.TryParse(movementSpeedField.text, out parsedFloat))
            settings.moveSpeed = parsedFloat;

        if (int.TryParse(maxEnemiesField.text, out parsedInt))
            settings.maxEnemies = parsedInt;

        if (int.TryParse(initialSpawnField.text, out parsedInt))
            settings.initialSpawn = parsedInt;
        
        

        settings.masterVolume = masterVolumeSlider.value;
        settings.musicVolume = musicVolumeSlider.value;
        settings.sfxVolume = sfxVolumeSlider.value;
        settings.enableTranslation = translationToggle.isOn;
        settings.EnglishToUkrainian = transcriptionToggle.isOn;
        UpdateRuntimeObjects();

        Debug.Log("Settings applied (with slider volume)");
    }


    // ---------- UPDATE GAME ----------
    private void UpdateRuntimeObjects()
    {
        if (levelManager != null)
        {
            levelManager.spawnInterval = settings.spawnInterval;
            levelManager.maxEnemies = settings.maxEnemies;
            levelManager.initialSpawn = settings.initialSpawn;
            enemyMovement.moveSpeed = settings.moveSpeed;
        }

        musicSource.volume = settings.musicVolume;
        sfxSource.volume = settings.sfxVolume;
        AudioListener.volume = settings.masterVolume;
    }
}
