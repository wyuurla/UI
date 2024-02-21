using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;


/**
 * @brief UIToolDraw_Toolbar
 * @detail 유아이툴 툴바.
 * @date 2024-02-18
 * @version 1.0.0
 * @author kij
 */
public class UIToolDraw_Toolbar : UIToolDraw
{
    static public int resolution_width
    {
        get
        {
            return PlayerPrefs.GetInt("uitool_resolution_width", 720);
        }
        set
        {
            PlayerPrefs.SetInt("uitool_resolution_width", value);
        }
    }

    static public int resolution_height
    {
        get
        {
            return PlayerPrefs.GetInt("uitool_resolution_height", 1280);
        }
        set
        {
            PlayerPrefs.SetInt("uitool_resolution_height", value);
        }
    }

    public UIToolDraw_Toolbar(UITool _tool) : base(_tool)
    {
        drawType = eDRAW_TYPE.TOOLBAR;
    }

    public override void Draw()
    {
        base.Draw();
        GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));
        GUILayout.FlexibleSpace();
        Draw_CreateUIManager();
        if (GUILayout.Button("keyname Bake", EditorStyles.toolbarButton, GUILayout.Width(130)))
        {
            m_tool.createScript.BakeKeynameScript();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("SUCCESS", "UIPathTable_keyname 스크립트를 생성완료했습니다.", "OK");
        }

        if ( GUILayout.Button("SAVE", EditorStyles.toolbarButton, GUILayout.Width(60)))
        {
            m_tool.Save();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("SUCCESS", "SAVE SUCCESS", "OK");
        }
        GUILayout.EndHorizontal();
    }

    /**
     * UIManager를 생성하는데 필요한 에디터 유아이.
     */
    public void Draw_CreateUIManager()
    {
        float _resolution_value_ui_width = 50;
        float _resolution_title_ui_width = 40;

        if( GUILayout.Button("SWAP", EditorStyles.toolbarButton, GUILayout.Width(50)) )
        {
            int _temp = resolution_width;
            resolution_width = resolution_height;
            resolution_height = _temp;
        }
       
        GUILayout.Label("width :", GUILayout.Width(_resolution_title_ui_width));
        resolution_width = EditorGUILayout.IntField(resolution_width, GUILayout.Width(_resolution_value_ui_width));

        GUILayout.Label("height :", GUILayout.Width(_resolution_title_ui_width));
        resolution_height = EditorGUILayout.IntField(resolution_height, GUILayout.Width(_resolution_value_ui_width));

        if (GUILayout.Button("CreateUIManager", EditorStyles.toolbarButton, GUILayout.Width(130)))
        {
            m_tool.createGameObject.CreateUIManager(resolution_width, resolution_height);
            EditorUtility.DisplayDialog("SUCCESS", "UIManager Complete", "OK");
        }
    }
}

