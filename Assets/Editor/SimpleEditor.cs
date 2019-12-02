using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SimpleEditor : Editor
{

    [MenuItem("Edit/Alternative Play _F5", priority = 160)]
    public static void Play()
    {
        EditorApplication.isPlaying = !EditorApplication.isPlaying;
    }

    [MenuItem("Edit/Alternative Pause _F6", priority = 161)]
    public static void Pause()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }
}