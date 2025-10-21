using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WwiseTestTool_State : EditorWindow
{
    [Header("测试State列表  (编辑模式下位置更新延迟，建议运行模式下使用)")]
    public List<AK.Wwise.State> WwiseStateList = new List<AK.Wwise.State>();
    [Header("事件播放物体")]
    private SerializedObject Tools_StateList;
    private SerializedProperty Tools_StateListProperty;


    [MenuItem("Tools/ Wwise测试工具/  State测试工具")]
    private static void Window()
    {
        WwiseTestTool_State _editorTest = (WwiseTestTool_State)EditorWindow.GetWindow(typeof(WwiseTestTool_State), false, "Wwise State测试组件", true);
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

        GUILayout.FlexibleSpace();//插入一个弹性空白

        if (GUILayout.Button("\n        切换State       \n"))
        {
            if (WwiseStateList.Count != 0)
            {
                foreach (var item in WwiseStateList)
                {
                    if (item.Name != "")
                    {
                        item.SetValue();
                        Debug.Log("  来自Wwise State测试工具切换State为" + item.Name);
                    }
                    else
                    {
                        Debug.LogWarning("  来自Wwise State测试工具  ：事件列表中有空State");
                    }
                }
            }
            else
            {
                Debug.LogWarning("来自Wwise State测试工具：未添加State");
            }
        }
        GUILayout.FlexibleSpace();//插入一个弹性空白
        GUILayout.EndHorizontal();
    }
}
