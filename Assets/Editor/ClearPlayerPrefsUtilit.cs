using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefsUtilit : ScriptableObject
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    static void DoIt()
    {
        if (EditorUtility.DisplayDialog("Player Prefs", "Are you sure to delete all player prefs?", "OK", "CANCEL"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}