using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/**
 * @brief UIToolDraw_List
 * @detail 유아이툴 선택한 UIPathRecord 정보.
 * @date 2024-02-18
 * @version 1.0.0
 * @author kij
 */
public class UIToolDraw_Info : UIToolDraw
{
    #region -static   
    public static string directory_create
    {
        get
        {
            return PlayerPrefs.GetString("uitool_create_script", "UI");
        }
        set
        {
            PlayerPrefs.SetString("uitool_create_script", value);
        }
    }
    #endregion

    Vector2 m_scrollbar = Vector2.zero;
    UICreateGameObject.eSLOTLIST_TYPE m_slotListType;
    


    public UIToolDraw_Info(UITool _tool) : base(_tool)
    {
    }

    public override void Draw()
    {
        base.Draw();

        m_scrollbar = GUILayout.BeginScrollView(m_scrollbar, new GUIStyle(GUI.skin.box), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

        Draw_UIPathRecord(m_tool.select);

        GUILayout.EndScrollView();
    }

    void Draw_UIPathRecord(UIPathRecord _record)
    {
        if (null == _record)
            return;

        // delete
        if (GUILayout.Button("Delete", GUILayout.Width(100))&& EditorUtility.DisplayDialog("DELETE", "delete?", "OK", "CANCEL"))
        {
            UIPathTable.Instance.Delete(_record.id);
            m_tool.SetSelect(null);
            return;
        }
        GUILayout.Label("");
        GUILayout.Label("-- infomation");
        GUILayout.Label($"keyname : {_record.keyname}", GUILayout.Width(200));
        _record.isBaseClass = EditorGUILayout.Toggle("use base class :", _record.isBaseClass);
        _record.uiType = (eUIPATH_TYPE)EditorGUILayout.EnumPopup(_record.uiType, GUILayout.Width(100));

        if (m_tool.select_resource != null)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(m_tool.select_resource, typeof(GameObject), false, GUILayout.Width(200));
            if (GUILayout.Button("APPLY", GUILayout.Width(100)))
            {
                m_tool.ApplySelectObject(_record);
                m_tool.DeleteSelectGameObject();
            }
            if (GUILayout.Button("LOAD", GUILayout.Width(100)))
            {
                m_tool.LoadSelectObject(_record);
            }
            if (GUILayout.Button("CLEAR", GUILayout.Width(100)))
            {
                m_tool.DeleteSelectGameObject();
            }
            GUILayout.EndHorizontal();
            if (null != m_tool.select_gameobject && _record.uiType == eUIPATH_TYPE.UIDialog )
            {
                UIDialog _dialog = m_tool.select_gameobject.GetComponent<UIDialog>();
                if( null != _dialog)
                {
                    Draw_AddButton(_dialog);
                    Draw_AddSlotList(_dialog);
                }
            }
        }

        GUILayout.Label("");
        Draw_Directory(_record);
        Draw_CreateScript(_record);
        Draw_CreatePrefab(_record);
    }

    void Draw_AddButton(UIDialog _dialog)
    {
        if (null == _dialog)
            return;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("UIButton", GUILayout.Width(150)))
        {
            UIButton _button = m_tool.createGameObject.CreateButton(_dialog);
            if (null != _button)
            {
                Selection.activeObject = _button;
            }
        }

        if (GUILayout.Button("Close UIButton", GUILayout.Width(150)))
        {
            UIButton _button = m_tool.createGameObject.CreateCloseButton(_dialog);
            if (null != _button)
            {
                Selection.activeObject = _button;
            }
        }

        if (GUILayout.Button("Text UIButton", GUILayout.Width(150)))
        {
            UIButton _button = m_tool.createGameObject.CreateTextButton(_dialog);
            if (null != _button)
            {
                Selection.activeObject = _button;
            }
        }

        GUILayout.EndHorizontal();
    }

    void Draw_AddSlotList(UIDialog _dialog)
    {
        GUILayout.BeginHorizontal();
        m_slotListType = (UICreateGameObject.eSLOTLIST_TYPE)EditorGUILayout.EnumPopup(m_slotListType, GUILayout.Width(200));
        if (GUILayout.Button("Add SlotList", GUILayout.Width(100)))
        {
            UISlotList _slotList = m_tool.createGameObject.CreateSlotList(_dialog, m_slotListType, 100);
            if( null != _slotList )
            {
                Selection.activeObject = _slotList;
            }
        }
        GUILayout.EndHorizontal();
    }

    void Draw_Directory(UIPathRecord _recrod )
    {
        GUILayout.BeginHorizontal();
        if (string.IsNullOrWhiteSpace(directory_create) == false && GUILayout.Button("RESET", GUILayout.Width(60)))
        {
            directory_create = "UI";
            GUIUtility.keyboardControl = 0;
        }
        GUILayout.Label("PATH : ", GUILayout.Width(50));
        directory_create = EditorGUILayout.TextField(directory_create, GUILayout.Width(Mathf.Min(300, directory_create.Length * 10 + 50)));
        GUILayout.EndHorizontal();
    }

    void Draw_CreateScript(UIPathRecord _record)
    {
        string _path = $"{m_tool.createScript.directory_script_create}/{directory_create}/{_record.keyname}.cs";
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Script", GUILayout.Width(100)))
        {
            if (m_tool.createScript.CreateScript(_record.uiType, _path))
            {
                _record.isBaseClass = false;
            }
        }

        string _className = System.IO.Path.GetFileNameWithoutExtension(_path);
        System.Type _classType = EditorUtil.GetTypeFromAssemblies(_className);
        Color _color = _classType == null ? Color.red : Color.white;
        GUIStyle _style = EditorUtil.GetGUIStyle(_color, EditorUtil.GetWidth(_path));
        GUILayout.Label(_path, _style);
        GUILayout.EndHorizontal();
    }

    void Draw_CreatePrefab(UIPathRecord _record)
    {
        GUILayout.BeginHorizontal();
        string _path = $"{directory_create}/{_record.keyname}";
        if (GUILayout.Button("Create Prefab", GUILayout.Width(100)))
        {
            string _savePath = EditorUtil.GetPrefabPath(_path);
            string _directory = System.IO.Path.GetDirectoryName(_savePath);
            if (System.IO.Directory.Exists(_directory) == false)
            {
                System.IO.Directory.CreateDirectory(_directory);
            }

            string _uiname = System.IO.Path.GetFileNameWithoutExtension(_savePath);


            UIBase _uiBase = null;
            switch (_record.uiType)
            {
                case eUIPATH_TYPE.UIDialog:
                    _uiBase = m_tool.createGameObject.CreateDialog(_record.isBaseClass ? typeof(UIDialog) : EditorUtil.GetTypeFromAssemblies(_uiname), _uiname, UIManager.Instance.dialog.transform, 100, 100);
                    break;
                case eUIPATH_TYPE.UISlot:
                    _uiBase = m_tool.createGameObject.CreateSlot(_record.isBaseClass ? typeof(UISlot) : EditorUtil.GetTypeFromAssemblies(_uiname), _uiname, UIManager.Instance.dialog.transform, 100, 100);
                    break;
                case eUIPATH_TYPE.UIHud:
                    _uiBase = m_tool.createGameObject.CreateHud(_record.isBaseClass ? typeof(UIHud) : EditorUtil.GetTypeFromAssemblies(_uiname), _uiname, UIManager.Instance.dialog.transform, 100, 100);
                    break;
                default:
                    Debug.LogError($"no code {_record.uiType}");
                    break;
            }


            if (null == _uiBase)
            {
                UnityEditor.EditorUtility.DisplayDialog("error", $"failed prefab : {_uiname}", "OK");
                return;
            }

            _record.path = EditorUtil.GetResourcePath(_savePath);
            PrefabUtility.SaveAsPrefabAsset(_uiBase.gameObject, EditorUtil.GetPrefabPath(_path));
            GameObject.DestroyImmediate(_uiBase.gameObject);
            m_tool.SetSelect(_record);

            UnityEditor.EditorUtility.DisplayDialog("complete", $"success cretae prefab : {_uiname}", "OK");
        }
        GUIStyle _style = EditorUtil.GetGUIStyle(m_tool.select_resource==null?Color.red:Color.white, EditorUtil.GetWidth(_path));
        GUILayout.Label(_path, _style);
        GUILayout.EndHorizontal();
    }
}

