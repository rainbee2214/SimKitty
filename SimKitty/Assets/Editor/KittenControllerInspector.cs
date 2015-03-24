using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(KittenController))]
public class KittenControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        KittenController kittenController = (KittenController)target;

        EditorGUILayout.LabelField("Total Kittens: ", ""+kittenController.kittens.Count);
        EditorGUILayout.LabelField("Kittens eating: ", ""+kittenController.kittensEating);
        EditorGUILayout.LabelField("Kittens sleeping: ", ""+kittenController.kittensSleeping);
        EditorGUILayout.LabelField("Kittens playing: ", ""+kittenController.kittensPlaying);
    }
}

