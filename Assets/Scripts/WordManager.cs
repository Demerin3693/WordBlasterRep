using UnityEngine;
using System.Collections.Generic;

public class WordManager : MonoBehaviour
{
    public static WordManager Instance;

    public TextAsset csvFile;      // ваш CSV
    [HideInInspector]
    public List<WordEntry> words = new List<WordEntry>();

    void Awake()
    {
        Instance = this;
        LoadCSV();
    }

    void LoadCSV()
    {
        words.Clear();

        string[] lines = csvFile.text.Split('\n');

        // пропускаємо заголовок
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] parts = line.Split(';');

            if (parts.Length >= 2)
            {
                WordEntry entry = new WordEntry();
                entry.word = parts[0].Trim();
                entry.translation = parts[1].Trim();

                words.Add(entry);
            }
        }
    }

    public WordEntry GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
}
