using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_State : EditorWindow
{
    [Header("����State�б�  (�༭ģʽ��λ�ø����ӳ٣���������ģʽ��ʹ��)")]
    public List<AK.Wwise.State> WwiseStateList = new List<AK.Wwise.State>();
    [Header("�¼���������")]
    private SerializedObject Tools_StateList;
    private SerializedProperty Tools_StateListProperty;


    [MenuItem("Tools/ Wwise���Թ���/  State���Թ���")]
    private static void Window()
    {
        WwiseTestTool_State _editorTest = (WwiseTestTool_State)EditorWindow.GetWindow(typeof(WwiseTestTool_State), false, "Wwise State�������", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_StateList = new SerializedObject(this);
        Tools_StateListProperty = Tools_StateList.FindProperty("WwiseStateList");
    }

    private void OnGUI()
    {
        GameObject selectedObject = Selection.activeGameObject;
        //PostObject = selectedObject;
        Tools_StateList.Update();


        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(Tools_StateListProperty, true);

        if (EditorGUI.EndChangeCheck())
        {
            Tools_StateList.ApplyModifiedProperties();
        }
        if (EditorGUI.EndChangeCheck())
        {
            Tools_StateList.ApplyModifiedProperties();
        }

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();//����һ�����Կհ�

        if (GUILayout.Button("\n        �л�State       \n"))
        {
            if (WwiseStateList.Count != 0)
            {
                foreach (var item in WwiseStateList)
                {
                    if (item.Name != "")
                    {
                        item.SetValue();
                        Debug.Log("  ����Wwise State���Թ����л�StateΪ" + item.Name);
                    }
                    else
                    {
                        Debug.LogWarning("  ����Wwise State���Թ���  ���¼��б����п�State");
                    }
                }
            }
            else
            {
                Debug.LogWarning("����Wwise State���Թ��ߣ�δ���State");
            }
        }
        GUILayout.FlexibleSpace();//����һ�����Կհ�
        GUILayout.EndHorizontal();
    }
}
