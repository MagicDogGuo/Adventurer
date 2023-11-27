using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnkaEditor.SceneGUI;
using UnkaEditor.Utitlites;

[CustomEditor(typeof(MapSource))]
public class MapToolEditor : Editor {

    //USceneSelector selector;

    MapSource Instance;
    bool GetItemObjectsFoldout = true;
    bool IsOpenOriginInspector = false;

    private void OnEnable()
    {
        Instance = (MapSource)target;

        //selector = new USceneSelector();

    }

    public override void OnInspectorGUI()
    {
        IsOpenOriginInspector = GUILayout.Toggle(IsOpenOriginInspector, new GUIContent("開啟原始面板"));
        if (IsOpenOriginInspector)
        {
            base.OnInspectorGUI();
            GUILayout.Space(20f);
        }
        serializedObject.Update();

        GUILayout.BeginVertical("Box");

        EditorGUILayout.PropertyField(serializedObject.FindProperty("BGM"), new GUIContent("BGM"));

        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");

        EditorGUILayout.PropertyField(serializedObject.FindProperty("RoleObjects").FindPropertyRelative("ObjPos"), new GUIContent("主角位置"));

        GUILayout.EndVertical();


        GUILayout.Space(20f);


        GUILayout.BeginVertical("Box");

        Instance.InterRoleObjects.IsAppear = EditorGUILayout.Toggle("是否出現互動角色", Instance.InterRoleObjects.IsAppear);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("InterRoleObjects").FindPropertyRelative("ObjSprite"), new GUIContent("互動角色圖片"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("InterRoleObjects").FindPropertyRelative("ObjPos"), new GUIContent("互動角色位置"));

        GUILayout.EndVertical();


        GUILayout.Space(20f);


        GetItemObjectsFoldout = EditorGUILayout.Foldout(GetItemObjectsFoldout, "拿取物件 是否為關鍵/圖片/位置");
        if (GetItemObjectsFoldout)
        {

            UEditorGUI.ArrayEditor(serializedObject.FindProperty("GetItemObjects"), typeof(GetItemObject), GetItemObject_ArrayEditorMiddle, GetItemObject_ArrayEditorTrail);

        }

        GUILayout.Space(20f);

        GetItemObjectsFoldout = EditorGUILayout.Foldout(GetItemObjectsFoldout, "沾濕物件 圖片/位置");
        if (GetItemObjectsFoldout)
        {

            UEditorGUI.ArrayEditor(serializedObject.FindProperty("WetObjects"), typeof(WetObject), GetItemObject_ArrayEditorMiddle, GetItemObject_ArrayEditorTrail);

        }

        GUILayout.Space(20f);


        GetItemObjectsFoldout = EditorGUILayout.Foldout(GetItemObjectsFoldout, "關卡會出現的方塊(Max:10)");
        if (GetItemObjectsFoldout)
        {

            UEditorGUI.ArrayEditor(serializedObject.FindProperty("LevelBlocks"), typeof(LevelBlock), GetItemObject_ArrayEditorMiddle, GetItemObject_ArrayEditorTrail);

        }

        GUILayout.Space(20f);

        if (GUI.changed)
        {

            EditorUtility.SetDirty(Instance);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(Instance.gameObject.scene);
        }

        serializedObject.ApplyModifiedProperties();
    }

    protected void GetItemObject_ArrayEditorMiddle(int index)
    {
       
    }

    protected void GetItemObject_ArrayEditorTrail(int index)
    {

    }
}
