using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_RTPC : EditorWindow
{
    [Header("����RTPC�б�  (�༭ģʽ��λ�ø����ӳ٣���������ģʽ��ʹ��)")]
    public List<AK.Wwise.RTPC> WwiseRTPCList = new List<AK.Wwise.RTPC>();
    [Header("RTPC�ı�����")]
    public GameObject PostObject;
    
    private SerializedObject Tools_RTPCList;
    private SerializedProperty Tools_RTPCListProperty;
    public float floatValue;
    public SerializedObject PostGameobject;
    private SerializedProperty PostGameobjectProperty;
    public GameObject DeafultObject;

    [MenuItem("Tools/ Wwise���Թ���/  RTPC���Թ���")]
    private static void Window()
    {
        WwiseTestTool_RTPC _editorTest = (WwiseTestTool_RTPC)EditorWindow.GetWindow(typeof(WwiseTestTool_RTPC), false, "Wwise RTPC �������", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_RTPCList = new SerializedObject(this);
        Tools_RTPCListProperty = Tools_RTPCList.FindProperty("WwiseRTPCList");
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
        GUILayout.Space(5);
        //PostObject = selectedObject;
        Tools_RTPCList.Update();
        PostGameobject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(Tools_RTPCListProperty, true);
        EditorGUILayout.PropertyField(PostGameobjectProperty, true);
        if (EditorGUI.EndChangeCheck())
        {
            Tools_RTPCList.ApplyModifiedProperties();
        }
        if (EditorGUI.EndChangeCheck())
        {
            Tools_RTPCList.ApplyModifiedProperties();
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
        
        GUILayout.Label("����Ҫ�ı��ֵ  ");
        floatValue = EditorGUILayout.FloatField("Float Value", floatValue);
        
        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();//����һ�����Կհ�

        if (GUILayout.Button("\n      ��object������RTPC     \n"))
        {
            if (PostObject != null)
            {
                if (WwiseRTPCList.Count != 0)
                {
                    foreach (var item in WwiseRTPCList)
                    {
                        if (item.Name != "")
                        {
                            item.SetValue(PostObject, floatValue);
                            Debug.Log("  ����Wwise RTPC���Թ���  ����" + PostObject.name + "�ϸı���" + item.Name + "Ϊ" + floatValue);
                        }
                        else
                        {
                            Debug.LogWarning("  ����Wwise RTPC���Թ���  ��RTPC�б����п�RTPC");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("����Wwise RTPC���Թ��ߣ�δ���RTPC");
                }
            }
            else
            {
                Debug.LogWarning("����Wwise ������Թ��ߣ���ǰ������û��AkAudioListener");
            }
        }

        if (GUILayout.Button("\n      ȫ������RTPC     \n"))
        {
            foreach (var item in WwiseRTPCList)
            {
                if (item.Name != "")
                {
                    item.SetGlobalValue(floatValue);
                    Debug.Log("  ����Wwise RTPC���Թ���  ��RTPC��ȫ���޸�Ϊ" + floatValue);
                }
                else
                {
                    Debug.LogWarning("  ����Wwise RTPC���Թ���  ��RTPC�б����п�RTPC");
                }
            }
        }
        GUILayout.FlexibleSpace();//����һ�����Կհ�
        GUILayout.EndHorizontal();
    }
}
