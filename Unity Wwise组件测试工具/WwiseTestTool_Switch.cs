using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_Switch : EditorWindow
{
    [Header("����Swtich�б�  (�༭ģʽ��λ�ø����ӳ٣���������ģʽ��ʹ��)")]
    public List<AK.Wwise.Switch> WwiseSwitchList = new List<AK.Wwise.Switch>();
    [Header("�¼���������")]
    public GameObject PostObject;
    private SerializedObject Tools_SwitchList;
    private SerializedProperty Tools_SwitchListProperty;
    public SerializedObject PostGameobject;
    private SerializedProperty PostGameobjectProperty;
    public GameObject DeafultObject;

    [MenuItem("Tools/ Wwise���Թ���/  Switch���Թ���")]
    private static void Window()
    {
        WwiseTestTool_Switch _editorTest = (WwiseTestTool_Switch)EditorWindow.GetWindow(typeof(WwiseTestTool_Switch), false, "Wwise Swtich�������", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_SwitchList = new SerializedObject(this);
        Tools_SwitchListProperty = Tools_SwitchList.FindProperty("WwiseSwitchList");
        PostGameobject = new SerializedObject(this);
        PostGameobjectProperty = PostGameobject.FindProperty("PostObject");
        //�ҵ�����Wwise��Ͳ������
        FindAkAudioListener();
    }
    public void FindAkAudioListener()//�ҵ�����Wwise��Ͳ������
    {
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))//���������е��������� 
        {
            if (obj.GetComponent<AkAudioListener>() != null)
            {
                PostObject = obj;
                break;
            }
        }
    }
    private void OnGUI()
    {
        GameObject selectedObject = Selection.activeGameObject;
        //PostObject = selectedObject;
        Tools_SwitchList.Update();
        PostGameobject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(Tools_SwitchListProperty, true);
        EditorGUILayout.PropertyField(PostGameobjectProperty, true);
        if (EditorGUI.EndChangeCheck())
        {
            Tools_SwitchList.ApplyModifiedProperties();
        }
        if (EditorGUI.EndChangeCheck())
        {
            Tools_SwitchList.ApplyModifiedProperties();
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("����ΪĬ����Ͳ����"))
        {
            FindAkAudioListener();
        }
        if (GUILayout.Button("����Ϊ��ѡ����   "))
        {
            PostObject = selectedObject;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();//����һ�����Կհ�

        if (GUILayout.Button("\n      ����Switch     \n"))
        {
            if (PostObject != null)
            {
                if (WwiseSwitchList.Count != 0)
                {
                    foreach (var item in WwiseSwitchList)
                    {
                        if (item.Name != "")
                        {
                            item.SetValue(PostObject);
                            Debug.Log("  ����Wwise Switch���Թ���  ����" + PostObject.name + "���л�SwtichΪ" + item.Name);
                        }
                        else
                        {
                            Debug.LogWarning("  ����Wwise Switch���Թ���  ��Switch�б����п�Switch");
                        }

                    }
                }
                else
                {
                    Debug.LogWarning("����Wwise Switch���Թ��ߣ�δ���Switch");
                }
            }
            else
            {
                Debug.LogWarning("����Wwise ������Թ��ߣ���ǰ������û��AkAudioListener");
            }
        }
        GUILayout.FlexibleSpace();//����һ�����Կհ�
        GUILayout.EndHorizontal();
    }

}
