using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddMenu : Editor {

    [MenuItem("Edit/Reset Playerprefs")]

    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
