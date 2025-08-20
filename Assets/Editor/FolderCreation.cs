using System.IO;
using UnityEditor;
using UnityEngine;

public class FolderCreation: EditorWindow
{
    [MenuItem("Tools/Create Project Folders100")]
    public static void CreateFolders()
    {
        string mainFolder = "Assets/Scripts";
        string subFolder = Path.Combine(mainFolder, "SubFolder");

        // Create main folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(mainFolder))
        {
            AssetDatabase.CreateFolder("Assets", "Scripts");
            Debug.Log("Created folder: " + mainFolder);
        }

        // Create subfolder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(subFolder))
        {
            AssetDatabase.CreateFolder(mainFolder, "Common");
            Debug.Log("Created subfolder: " + subFolder);
        }
        // Create subfolder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(subFolder))
        {
            AssetDatabase.CreateFolder(mainFolder, "Camerar");
            Debug.Log("Created subfolder: " + subFolder);
        }
        AssetDatabase.Refresh();
    }
}
