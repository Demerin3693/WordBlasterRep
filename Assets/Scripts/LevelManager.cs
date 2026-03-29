using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn settings")]
    public GameObject enemyPrefab;         // префаб ворога
    public BoxCollider2D spawnZone;        // зона спавну (встановити в інспекторі)
    public Transform enemiesParent;        // куди ставити створених ворогів (опціонально)

    [Header("Timing & limits")]
    public float spawnInterval = 2f;       // інтервал між спавнами (сек)
    public int maxEnemies = 10;            // максимум на сцені
    public int initialSpawn = 3;           // скільки спавнити відразу на старті
    public bool spawnOnStart = true;       // чи починати спавнити автоматично

    public GameSettings settings;

    // внутрішній трекер
    private readonly List<GameObject> spawned = new List<GameObject>();
    private Coroutine spawnRoutine;

    void Start()
    {
        if (spawnZone == null)
        {
            Debug.LogError("LevelManager: призначте spawnZone (BoxCollider2D) в інспекторі.");
            enabled = false;
            return;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("LevelManager: призначте enemyPrefab в інспекторі.");
            enabled = false;
            return;
        }

        if (spawnOnStart)
        {
            // початкові вороги
            for (int i = 0; i < settings.initialSpawn; i++)
                SpawnOne();

            // запустити корутину
            spawnRoutine = StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            CleanupNulls();

            if (spawned.Count < settings.maxEnemies)
                SpawnOne();

            yield return new WaitForSeconds(settings.spawnInterval);
        }
    }

    // спавнить одного ворога в випадковому місці всередині BoxCollider2D
    public GameObject SpawnOne()
    {
        Vector2 pos = GetRandomPointInBounds(spawnZone.bounds);

        GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity, enemiesParent);
        spawned.Add(go);
        return go;
    }

    // отримує випадкову точку в межах Bounds
    private Vector2 GetRandomPointInBounds(Bounds b)
    {
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        return new Vector2(x, y);
    }

    // видаляє записи про вже знищені вороги
    private void CleanupNulls()
    {
        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            if (spawned[i] == null) spawned.RemoveAt(i);
        }
    }

    // опціонально: зупинити/запустити спавн зовні
    public void StartSpawning()
    {
        if (spawnRoutine == null) spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    // для візуалізації зони в сцені
    void OnDrawGizmosSelected()
    {
        if (spawnZone != null)
        {
            Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.25f);
            Gizmos.DrawCube(spawnZone.bounds.center, spawnZone.bounds.size);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(spawnZone.bounds.center, spawnZone.bounds.size);
        }
    }
}
