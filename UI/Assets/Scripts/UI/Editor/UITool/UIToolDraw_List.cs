using UnityEngine;
using UnityEditor;

/**
 * @brief UIToolDraw_List
 * @detail 유아이툴 UIPathRecord 리스트.
 * @date 2024-02-18
 * @version 1.0.0
 * @author kij
 */
public class UIToolDraw_List : UIToolDraw
{
    protected Vector2 m_scrollbar = Vector2.zero;

    protected string m_name;
    protected float m_width = 200f;
    public UIToolDraw_List(UITool _tool) : base(_tool)
    {
    }   

    public override void Draw()
    {
        base.Draw();

        GUILayout.BeginVertical(GUILayout.Width(m_width));

        Draw_KeyName();

        m_scrollbar = GUILayout.BeginScrollView(m_scrollbar, new GUIStyle(GUI.skin.box), GUILayout.ExpandHeight(true), GUILayout.Width(m_width));

        for ( int i=0; i<UIPathTable.Instance.list.Count; ++i )
        {
            UIPathRecord _record = UIPathTable.Instance.list[i];

            if( string.IsNullOrWhiteSpace(m_name) == false && _record.keyname.Contains(m_name, System.StringComparison.OrdinalIgnoreCase) == false)
            {
                continue;
            }

            bool _isCheck = _record == m_tool.select;
            string _name = $"{_record.id}.{_record.keyname}";

            GUILayout.BeginHorizontal();
            bool _isToggle = GUILayout.Toggle(_isCheck, _name);
            if (_isCheck == false && _isToggle == true)
            {
                m_tool.SetSelect(_record);
                GUIUtility.keyboardControl = 0;
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    /**
     * 키네임으로 유아이 등록.
     */
    void Draw_KeyName()
    {
        GUILayout.BeginHorizontal();
        m_name = EditorGUILayout.TextField(m_name);
        if (string.IsNullOrWhiteSpace(m_name) == false &&
            UIPathTable.Instance.IsHasName(m_name) == false &&
            GUILayout.Button("ADD", GUILayout.Width(60)))
        {
            UIPathRecord _record = UIPathTable.Instance.Add(UIPathTable.Instance.GetEmptyId());
            _record.keyname = m_name;
            _record.uiType = eUIPATH_TYPE.UIDialog;
            m_name = string.Empty;
            m_tool.SetSelect(_record);
            m_tool.Save();
            m_tool.createScript.BakeKeynameScript();
            GUIUtility.keyboardControl = 0;
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("SUCCESS", $"{_record.keyname} 추가했습니다.", "OK");
        }
        if (string.IsNullOrWhiteSpace(m_name) == false &&
           GUILayout.Button("CLEAR", GUILayout.Width(60)))
        {
            m_name = string.Empty;
            GUIUtility.keyboardControl = 0;
        }
        GUILayout.EndHorizontal();
    }
}
