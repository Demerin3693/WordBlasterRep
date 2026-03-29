using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Config/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    public int initialSpawn = 3;

    public float moveSpeed = 1f;

    [Header("Audio")]
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    [Header("Translation")]
    public bool enableTranslation = true;  
    public bool EnglishToUkrainian = true;  

    

}
