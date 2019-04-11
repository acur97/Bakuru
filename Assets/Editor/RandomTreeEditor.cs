using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomTree))]
public class RandomTreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomTree myScript = (RandomTree)target;
        if (GUILayout.Button("Random Seed"))
        {
            myScript.RandomSeed();
        }
    }
}
