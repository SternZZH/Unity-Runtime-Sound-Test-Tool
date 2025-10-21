using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_Event : EditorWindow
{
    [Header("测试事件列表  (编辑模式下位置更新延迟，建议运行模式下使用)")]
    public List<AK.Wwise.Event> WwiseEventList = new List<AK.Wwise.Event>();
    [Header("事件播放物体")]
    public GameObject PostObject;
    private SerializedObject Tools_EventList;
    private SerializedProperty Tools_EventListProperty;
    public SerializedObject PostGameobject;
    private SerializedProperty PostGameobjectProperty;
    public GameObject DeafultObject;

    [MenuItem("Tools/ Wwise测试工具/  事件测试工具")]
    private static void Window()
    {
        WwiseTestTool_Event _editorTest = (WwiseTestTool_Event)EditorWindow.GetWindow(typeof(WwiseTestTool_Event), false, "Wwise 测试组件", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_EventList = new SerializedObject(this);
        Tools_EventListProperty = Tools_EventList.FindProperty("WwiseEventList");
        PostGameobject = new SerializedObject(this);
        PostGameobjectProperty = PostGameobject.FindProperty("PostObject");
        //找到挂载Wwise听筒的物体
        FindAkAudioListener();
    }
    public void FindAkAudioListener()//找到挂载Wwise听筒的物体
    {
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))//遍历场景中的所有物体 
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
        if (GUILayout.Button("\n   设置为Ak听筒物体   \n"))
        {
            FindAkAudioListener();
        }
        if (GUILayout.Button("\n   设置为所选物体   \n"))
        {
            PostObject = selectedObject;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space(15);
        GUILayout.BeginHorizontal();
        
        GUILayout.FlexibleSpace();//插入一个弹性空白
        
        if (GUILayout.Button("\n      测试事件     \n"))
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
                            Debug.Log("  来自Wwise Event测试工具  ：在" + PostObject.name + "上播放了" + item.Name);
                        }
                        else
                        {
                            Debug.LogWarning("  来自Wwise Event测试工具  ：事件列表中有空事件");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("来自Wwise Event测试工具：未添加事件");
                }
            }
            else
            {
                Debug.LogWarning("来自Wwise 组件测试工具：当前场景中没有AkAudioListener");
            }
        }
        GUILayout.FlexibleSpace();//插入一个弹性空白
        GUILayout.EndHorizontal();
    }
    
}