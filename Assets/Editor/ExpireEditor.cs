using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Expire))]
public class ExpireEditor : Editor {

    Expire thisExpire;
    
    bool expireOn = false;

    public override void OnInspectorGUI()
    {
        thisExpire = (Expire)target;

        expireOn = EditorGUILayout.Foldout(expireOn, "Expire On");
        if (expireOn)
        {
            EditorGUI.indentLevel++;
            thisExpire.expireOnVelocity = EditorGUILayout.BeginToggleGroup("Velocity", thisExpire.expireOnVelocity);
            thisExpire.velocityCutoff = EditorGUILayout.FloatField("Cutoff", thisExpire.velocityCutoff);
            EditorGUILayout.EndToggleGroup();
            thisExpire.expireOnScale = EditorGUILayout.BeginToggleGroup("Scale", thisExpire.expireOnScale);
            thisExpire.scaleCutoff = EditorGUILayout.FloatField("Cutoff", thisExpire.scaleCutoff);
            EditorGUILayout.EndToggleGroup();
            thisExpire.expireOnTime = EditorGUILayout.BeginToggleGroup("Time", thisExpire.expireOnTime);
            thisExpire.timeCutoff = EditorGUILayout.FloatField("Cutoff", thisExpire.timeCutoff);
            EditorGUILayout.EndToggleGroup();
            EditorGUI.indentLevel--;
        }
    }
}
