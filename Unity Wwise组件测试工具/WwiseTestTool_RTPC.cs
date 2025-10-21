using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_RTPC : EditorWindow
{
    [Header("测试RTPC列表  (编辑模式下位置更新延迟，建议运行模式下使用)")]
    public List<AK.Wwise.RTPC> WwiseRTPCList = new List<AK.Wwise.RTPC>();
    [Header("RTPC改变物体")]
    public GameObject PostObject;
    
    private SerializedObject Tools_RTPCList;
    private SerializedProperty Tools_RTPCListProperty;
    public float floatValue;
    public SerializedObject PostGameobject;
    private SerializedProperty PostGameobjectProperty;
    public GameObject DeafultObject;

    [MenuItem("Tools/ Wwise测试工具/  RTPC测试工具")]
    private static void Window()
    {
        WwiseTestTool_RTPC _editorTest = (WwiseTestTool_RTPC)EditorWindow.GetWindow(typeof(WwiseTestTool_RTPC), false, "Wwise RTPC 测试组件", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_RTPCList = new SerializedObject(this);
        Tools_RTPCListProperty = Tools_RTPCList.FindProperty("WwiseRTPCList");
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

        if (GUILayout.Button("设置为默认听筒物体"))
        {
            FindAkAudioListener();
        }
        if (GUILayout.Button("设置为所选物体   "))
        {
            PostObject = selectedObject;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Label("输入要改变的值  ");
        floatValue = EditorGUILayout.FloatField("Float Value", floatValue);
        
        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();//插入一个弹性空白

        if (GUILayout.Button("\n      在object上设置RTPC     \n"))
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
                            Debug.Log("  来自Wwise RTPC测试工具  ：在" + PostObject.name + "上改变了" + item.Name + "为" + floatValue);
                        }
                        else
                        {
                            Debug.LogWarning("  来自Wwise RTPC测试工具  ：RTPC列表中有空RTPC");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("来自Wwise RTPC测试工具：未添加RTPC");
                }
            }
            else
            {
                Debug.LogWarning("来自Wwise 组件测试工具：当前场景中没有AkAudioListener");
            }
        }

        if (GUILayout.Button("\n      全局设置RTPC     \n"))
        {
            foreach (var item in WwiseRTPCList)
            {
                if (item.Name != "")
                {
                    item.SetGlobalValue(floatValue);
                    Debug.Log("  来自Wwise RTPC测试工具  ：RTPC被全局修改为" + floatValue);
                }
                else
                {
                    Debug.LogWarning("  来自Wwise RTPC测试工具  ：RTPC列表中有空RTPC");
                }
            }
        }
        GUILayout.FlexibleSpace();//插入一个弹性空白
        GUILayout.EndHorizontal();
    }
}
