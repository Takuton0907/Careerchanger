using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;

[CustomEditor(typeof(ComboData))]
public class ComboDataEditor : Editor
{
    private ReorderableList m_reorderableList;

    private void OnEnable()
    {
        var _list = serializedObject.FindProperty("m_comboData");

        m_reorderableList = new ReorderableList(serializedObject, _list)
        {
            // 要素の描画時のコールバック
            drawElementCallback = (rect, index, active, focused) =>
            {
                rect.xMin += 10;
                EditorGUI.PropertyField(rect, _list.GetArrayElementAtIndex(index), new GUIContent("コンボパターン" + index), true);
            },
            elementHeightCallback = index => EditorGUI.GetPropertyHeight(_list.GetArrayElementAtIndex(index)),
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "コンボデータ"),
        };
    }

    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示する
        base.OnInspectorGUI();

        serializedObject.Update();
        m_reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}