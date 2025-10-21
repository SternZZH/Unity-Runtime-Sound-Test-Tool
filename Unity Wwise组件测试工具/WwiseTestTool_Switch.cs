using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_Switch : EditorWindow
{
    [Header("测试Swtich列表  (编辑模式下位置更新延迟，建议运行模式下使用)")]
    public List<AK.Wwise.Switch> WwiseSwitchList = new List<AK.Wwise.Switch>();
    [Header("事件播放物体")]
    public GameObject PostObject;
    private SerializedObject Tools_SwitchList;
    private SerializedProperty Tools_SwitchListProperty;
    public SerializedObject PostGameobject;
    private SerializedProperty PostGameobjectProperty;
    public GameObject DeafultObject;

    [MenuItem("Tools/ Wwise测试工具/  Switch测试工具")]
    private static void Window()
    {
        WwiseTestTool_Switch _editorTest = (WwiseTestTool_Switch)EditorWindow.GetWindow(typeof(WwiseTestTool_Switch), false, "Wwise Swtich测试组件", true);
        _editorTest.Show();
    }

    private void OnEnable()
    {
        Tools_SwitchList = new SerializedObject(this);
        Tools_SwitchListProperty = Tools_SwitchList.FindProperty("WwiseSwitchList");
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

        if (GUILayout.Button("设置为默认听筒物体"))
        {
            FindAkAudioListener();
        }
        if (GUILayout.Button("设置为所选物体   "))
        {
            PostObject = selectedObject;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();//插入一个弹性空白

        if (GUILayout.Button("\n      测试Switch     \n"))
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
                            Debug.Log("  来自Wwise Switch测试工具  ：在" + PostObject.name + "上切换Swtich为" + item.Name);
                        }
                        else
                        {
                            Debug.LogWarning("  来自Wwise Switch测试工具  ：Switch列表中有空Switch");
                        }

                    }
                }
                else
                {
                    Debug.LogWarning("来自Wwise Switch测试工具：未添加Switch");
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
