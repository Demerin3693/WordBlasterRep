using UnityEngine;
using System.Diagnostics;
using System.IO;

public class OpenCSV1 : MonoBehaviour
{
    public string fileName = "WordsList.csv";

    public void OpenCSVFile()
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, fileName);

        Process.Start(new ProcessStartInfo(fullPath)
        {
            UseShellExecute = true
        });
    }
}
