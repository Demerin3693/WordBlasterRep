using UnityEngine;
using System.Diagnostics;
using System.IO;

public class OpenCSV : MonoBehaviour
{
public string relativePath = "Assets/WordsList.csv";
public void OpenCSVFile()
{
    string fullPath = System.IO.Path.Combine(Application.dataPath, "..", relativePath);

    Process.Start(new ProcessStartInfo(fullPath)
    {
        UseShellExecute = true
    });
}

}
