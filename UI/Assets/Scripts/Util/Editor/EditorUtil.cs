using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public static class EditorUtil
{
    static public GUIStyle titleStype
    {
        get
        {
            GUIStyle _style = new GUIStyle();
            _style.fontStyle = FontStyle.Bold;
            _style.fontSize = 15;
            _style.normal.textColor = Color.white;           
            return _style;
        }
    }

    static public GUIStyle GetGUIStyle( Color _color, int _width)
    {
        GUIStyle _style = new GUIStyle();
        _style.fixedWidth = _width;
        _style.normal.textColor = _color;
        return _style;
    }

    static public int GetWidth(string _test)
    {
        if (null == _test)
            return 10;

        return 10 + _test.Length * 10;
    }

    public static void OpenScene(string _strSceneName)
    {
        string strScene = string.Format("Assets/{0}.unity", _strSceneName);
        string _curSceneName = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name;
        if (0 == string.Compare(strScene, _curSceneName, true))
        {
            return;
        }
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(strScene);
    }

    public static string GetSelectedResPath()
    {
        if (null == Selection.activeObject)
            return null;

        string sourcePath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeObject));
        string strName = Selection.activeObject.name;

        string strPath = string.Empty;
        sourcePath = sourcePath.ToLower();
        string[] words = sourcePath.Split('\\');
        bool sw = false;
        foreach (string word in words)
        {
            if (sw)
            {
                strPath = strPath + word + "/";
            }

            if (word.CompareTo("resources") == 0)
                sw = true;
        }
        strPath += strName;
        return strPath;
    }
   

    static public string GetAssetPath(string _path, string _title)
    {
        GUILayout.BeginHorizontal();
        if (Selection.activeObject != null && GUILayout.Button("Apply", GUILayout.Width(80f)))
        {
            GUIUtility.keyboardControl = 0;
            DirectoryInfo _info = System.IO.Directory.GetParent(AssetDatabase.GetAssetPath(Selection.activeObject));
            _path = _info.FullName;
        }

        _path = EditorGUILayout.TextField(_title, _path);
        GUILayout.EndHorizontal();

        return _path;
    }

    public static string GetSelectName()
    {
        GameObject _obj = Selection.activeObject as GameObject;
        if (null == _obj)
        {
            return Selection.activeObject.name;
        }

        List<string> _parentList = new List<string>();
        Transform _parent = _obj.transform.parent;
        while (true)
        {
            if (null == _parent)
                break;

            _parentList.Add(_parent.name);
            _parent = _parent.parent;
        }

        if (_parentList.Count <= 0)
        {
            return Selection.activeObject.name;
        }

        System.Text.StringBuilder _sb = new System.Text.StringBuilder();
        for (int i = _parentList.Count - 1; i >= 0; --i)
        {
            _sb.Append(_parentList[i]);
            _sb.Append("/");
        }
        _sb.Append(Selection.activeObject.name);
        return _sb.ToString();
    }

    static public string GetSelectPath(string _path, string _title)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply", GUILayout.Width(80f)))
        {
            GUIUtility.keyboardControl = 0;
            _path = GetSelectName();
        }

        _path = EditorGUILayout.TextField(_title, _path);
        GUILayout.EndHorizontal();

        return _path;
    }

    public static System.Type GetTypeFromAssemblies(string TypeName)
    {
        var type = System.Type.GetType(TypeName);
        if (type != null)
            return type;
     
        var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in referencedAssemblies)
        {
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            if (assembly != null)
            {
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;
            }
        }

        return null;
    }

    static public GUIStyle GetBoxStyle(int _lef, int _top)
    {
        GUIStyle _style = new GUIStyle("box");
        _style.margin = new RectOffset(_lef, 0, _top, 0);
        return _style;
    }


    static public string GetHierachyPath(string _path, string _title, string _tag)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply", GUILayout.Width(80f)))
        {
            GUIUtility.keyboardControl = 0;
            _path = GetSelectResPath_Hierachy(_tag);
        }
        _path = EditorGUILayout.TextField(_title, _path);
        GUILayout.EndHorizontal();
        return _path;
    }

    static public string GetAssetFilePath(string _path, string _title)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply", GUILayout.Width(80f)))
        {
            GUIUtility.keyboardControl = 0;
            _path = GetSelectedResPath();
        }

        _path = EditorGUILayout.TextField(_title, _path);
        GUILayout.EndHorizontal();

        return _path;
    }
    public static string GetSelectResPath_Hierachy(string _tag)
    {
        Transform _trans = Selection.activeTransform;
        string _path = null;
        if (_trans != null)
        {
            _path = _trans.name;
            while (_trans.parent != null)
            {
                _trans = _trans.parent;
                if (_trans.tag == _tag)
                    break;
                _path = _trans.name + "/" + _path;
            }
        }
        return _path;
    }

    public static readonly string resources_path = "Assets/Resources/";
    public static readonly string resources_extension = ".prefab";

    public static string GetPrefabPath(string _path)
    {
        if (string.IsNullOrWhiteSpace(_path) == true)
            return null;

        return resources_path + _path + resources_extension;
    }

    public static string GetResourcePath(string _path)
    {
        _path = _path.Replace(resources_path, "");
        _path = _path.Replace(resources_extension, "");
        return _path;
    }

    public static bool GetObjectField_Resources(ref Object _resource, ref string _path, int _width)
    {
        Object _temp_resource = _resource;
        _resource = EditorGUILayout.ObjectField(_resource, typeof(GameObject), false, GUILayout.Width(_width));
        if (_temp_resource != _resource)
        {
            if (_resource == null)
            {
                _path = null;
            }
            else
            {
                string _tempPath = AssetDatabase.GetAssetPath(_resource);
                if (_tempPath.Contains(resources_path) == false)
                {
                    _resource = _temp_resource;
                    EditorUtility.DisplayDialog("ERROR", "Resources 폴더의 프리팹을 선택해야합니다.", "OK");
                    return false;
                }
                else
                {
                    _path = GetResourcePath(_tempPath);
                }
            }

            return true;
        }

        return false;
    }

    public static bool GetObjectField_Hierarchy(ref GameObject _resource, ref string _path, int _width)
    {
        GameObject _temp_resource = _resource;
        _resource = (GameObject)EditorGUILayout.ObjectField(_resource, typeof(GameObject), true, GUILayout.Width(_width));
        if (_temp_resource != _resource)
        {
            if (_resource == null)
            {
                _path = null;
            }
            else
            {
                _path = _resource.name;
            }

            return true;
        }

        return false;
    }
}