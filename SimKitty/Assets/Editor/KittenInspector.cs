using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Kitten))]
public class KittenInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        Kitten kitten = (Kitten)target;

        EditorGUILayout.LabelField("Walking Speed:", kitten.walkSpeed + "");
        EditorGUILayout.LabelField("Gender: ", kitten.Gender);

        EditorGUILayout.LabelField("Current status:", kitten.currentStatus.ToString());

        EditorGUILayout.LabelField("Hungryness:", "" + kitten.hungryness);
        EditorGUILayout.LabelField("Sleepyness:", "" + kitten.sleepyness);
        EditorGUILayout.LabelField("Playfulness:", "" + kitten.playfulness);
        EditorGUILayout.LabelField("Angryness:", "" + kitten.angryness);
        EditorGUILayout.LabelField("Lazyness:", "" + kitten.lazyness);
        EditorGUILayout.LabelField("Funness:", "" + kitten.funness);
        EditorGUILayout.LabelField("Cuteness:", "" + kitten.cuteness);
        EditorGUILayout.LabelField("Cuddlyness:", "" + kitten.cuddlyness);
        EditorGUILayout.LabelField("Fatness:", "" + kitten.fatness);
    }
}

    