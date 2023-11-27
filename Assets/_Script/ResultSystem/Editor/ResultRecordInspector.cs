using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnkaEditor.Utitlites;


[CustomEditor(typeof(ResultRecord))]
public class ResultRecordInspector : Editor {

    //ResultRecord resultRecord;
    bool GetItemObjectsFoldout = true;

    private void OnEnable()
    {
        //resultRecord = (ResultRecord)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // EditorGUILayout.LabelField("條件");
        // EditorGUILayout.BeginHorizontal();
        // //EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(50));
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("ResultDatas")[.FindPropertyRelative("Level"), new GUIContent("關卡"));
        //// EditorGUILayout.LabelField("Name1", GUILayout.MaxWidth(50));
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("ResultDatas").FindPropertyRelative("Role_TakeKeyItemAmount"), new GUIContent("是否淋雨"));
        //// EditorGUILayout.LabelField("Name2", GUILayout.MaxWidth(50));
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("ResultDatas").FindPropertyRelative("Role_IsWetting"), new GUIContent("是否淋雨"));

        // EditorGUILayout.EndHorizontal();


        GetItemObjectsFoldout = EditorGUILayout.Foldout(GetItemObjectsFoldout, "過關條件");
        if (GetItemObjectsFoldout)
        {
            UEditorGUI.ArrayEditor(serializedObject.FindProperty("ResultDatas"), typeof(ResultData));
        }

        serializedObject.ApplyModifiedProperties();
    }

}
