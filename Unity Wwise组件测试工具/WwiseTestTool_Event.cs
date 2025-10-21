using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_Event : EditorWindow
{
    [Header("�����¼��б�  (�༭ģʽ��λ�ø����ӳ٣���������ģʽ��ʹ��)")]
    public List<AK.Wwise.Event> WwiseEventList = new List<AK.Wwise.Event>();
    [Header("�¼���������")]
    public GameObject PostObject;
    private SerializedObject Tools_EventList;
    private SerializedProperty Tools_EventListProperty;
    public SerializedObject PostGameobject;
    private SerializedProperty PostGameobjectProperty;
    public GameObject DeafultObject;

    [MenuItem("Tools/ Wwise���Թ���/  �¼����Թ���")]
    private static void Window()
    {
        WwiseTestTool_Event _editorTest = (WwiseTestTool_Event)EditorWindow.GetWindow(typeof(WwiseTestTool_Event), false, "Wwise �������", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_EventList = new SerializedObject(this);
        Tools_EventListProperty = Tools_EventList.FindProperty("WwiseEventList");
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
        Tools_EventList.Update();
        PostGameobject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(Tools_EventListProperty, true);
        EditorGUILayout.Space(20);
        EditorGUILayout.PropertyField(PostGameobjectProperty, true);
        if (EditorGUI.EndChangeCheck())
        {
            Tools_EventList.ApplyModifiedProperties();
        }

        EditorGUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("\n   ����ΪAk��Ͳ����   \n"))
        {
            FindAkAudioListener();
        }
        if (GUILayout.Button("\n   ����Ϊ��ѡ����   \n"))
        {
            PostObject = selectedObject;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space(15);
        GUILayout.BeginHorizontal();
        
        GUILayout.FlexibleSpace();//����һ�����Կհ�
        
        if (GUILayout.Button("\n      �����¼�     \n"))
        {
            if (PostObject != null)
            {
                if(WwiseEventList.Count!=0)
                {
                    foreach (var item in WwiseEventList)
                    {
                        if(item.Name != "")
                        {
                            item.Post(PostObject);
                            Debug.Log("  ����Wwise Event���Թ���  ����" + PostObject.name + "�ϲ�����" + item.Name);
                        }
                        else
                        {
                            Debug.LogWarning("  ����Wwise Event���Թ���  ���¼��б����п��¼�");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("����Wwise Event���Թ��ߣ�δ����¼�");
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