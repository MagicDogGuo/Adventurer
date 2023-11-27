using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnkaEditor.Utitlites;



[CustomEditor(typeof(LevelGoalRecord))]
public class LevelGoalRecordInspector : Editor {

    public static int ErorrTime=0;

    //LevelGoalRecord levelGoalRecord;
    bool GetItemObjectsFoldout = true;

    private void OnEnable()
    {
        //levelGoalRecord = (LevelGoalRecord)target;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();

        GetItemObjectsFoldout = EditorGUILayout.Foldout(GetItemObjectsFoldout, "關卡目標");

        for (int i = 0; i < serializedObject.FindProperty("LevelGaolDatas").arraySize; i++)
        {
            if (serializedObject.FindProperty("LevelGaolDatas").GetArrayElementAtIndex(i).FindPropertyRelative("m_GoalObjects").arraySize > 3)
            {
                serializedObject.FindProperty("LevelGaolDatas").GetArrayElementAtIndex(i).FindPropertyRelative("m_GoalObjects").arraySize = 3;
                EditorWindow.GetWindow(typeof(EorrorWindow));
                ErorrTime++;
            }
        }

        if (GetItemObjectsFoldout)
        {
            UEditorGUI.ArrayEditor(serializedObject.FindProperty("LevelGaolDatas"), typeof(LevelGaolData));
        }
        serializedObject.ApplyModifiedProperties();

    }
}


