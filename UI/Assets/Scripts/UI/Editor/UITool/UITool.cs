using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
 * @brief UITool
 * @detail 유아이 관련 클래스.
 * @date 2024-02-18
 * @version 1.0.0
 * @author kij
 */

public class UITool : EditorWindow
{
    [MenuItem("GameTool/UITool")]
    static void Init()
    {
        EditorWindow editorWindow = GetWindow(typeof(UITool));
        editorWindow.Show();
    }


    private int m_select;
    private List<UIToolDraw> m_drawList = new List<UIToolDraw>();

    public GameObject select_resource;
    public GameObject select_gameobject;
    public UICreateGameObject createGameObject = new UICreateGameObject();
    public UICreateScript createScript = new UICreateScript();
    

    public UIPathRecord select { get { if (m_select <= 0) return null; return UIPathTable.Instance.Get( m_select ); } }

    public void SetSelect(UIPathRecord _select)
    {
        m_select = 0;
        DeleteSelectGameObject();
        select_resource = null;       
        if (null == _select)
            return;

        m_select = _select.id;
        select_resource = AssetDatabase.LoadMainAssetAtPath(EditorUtil.GetPrefabPath(select.path)) as GameObject;
        LoadSelectObject(_select);
    }

    private void OnEnable()
    {
        m_drawList.Clear();
        m_drawList.Add(new UIToolDraw_Toolbar(this));
        m_drawList.Add(new UIToolDraw_List(this));
        m_drawList.Add(new UIToolDraw_Info(this));

        for (int i = 0; i < m_drawList.Count; ++i)
        {
            if (null == m_drawList[i])
                continue;
            m_drawList[i].OnEnable();
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < m_drawList.Count; ++i)
        {
            if (null == m_drawList[i])
                continue;

            m_drawList[i].OnDisable();
        }
        m_drawList.Clear();
       
        SetSelect(null);
    }

    void OnGUI()
    {
        DrawList(UIToolDraw.eDRAW_TYPE.TOOLBAR);

        EditorGUILayout.BeginHorizontal();
        DrawList(UIToolDraw.eDRAW_TYPE.LIST);
        DrawList(UIToolDraw.eDRAW_TYPE.INFO);
        EditorGUILayout.EndHorizontal();
    }

    void DrawList( UIToolDraw.eDRAW_TYPE _type)
    {
        for (int i = 0; i < m_drawList.Count; ++i)
        {
            if (m_drawList[i] == null)
                continue;
            if (m_drawList[i].drawType != _type)
                continue;
            m_drawList[i].Draw();
        }
    }


    public void DeleteSelectGameObject()
    {
        if (null == select_gameobject)
            return;

        GameObject.DestroyImmediate(select_gameobject);
        select_gameobject = null;
    }

    public void LoadSelectObject(UIPathRecord _record)
    {
        DeleteSelectGameObject();

        if (null == _record)
            return;

        if (null == select_resource)        
            return;        

        select_gameobject = (GameObject)PrefabUtility.InstantiatePrefab(select_resource);
        select_gameobject.transform.SetParent(UIManager.Instance.dialog.transform);
        (select_gameobject.transform as RectTransform).sizeDelta = Vector2.zero;
        select_gameobject.transform.localScale = Vector3.one;
        select_gameobject.transform.localPosition = Vector3.zero;
        Selection.activeGameObject = select_gameobject;
    }

    public GameObject ApplySelectObject(UIPathRecord _record)
    {
        if (select_gameobject == null)
            return null;

        return PrefabUtility.SaveAsPrefabAsset(select_gameobject, EditorUtil.GetPrefabPath(_record.path));
    }

    /**
    * UIPathTable을 파일에 저장한다.
    */
    public void Save()
    {
        UIPathTable.Instance.Save();
        AssetDatabase.SaveAssets();
    }
}