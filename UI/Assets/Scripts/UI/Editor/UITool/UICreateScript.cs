using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreateScript
{
    public string directory_script_create;

    public UICreateScript()
    {
        directory_script_create = string.Format("{0}/Scripts", Application.dataPath);
    }
   
    public bool CreateScript(eUIPATH_TYPE _type, string _path)
    {
        if (_type == eUIPATH_TYPE.NONE)
        {
            UnityEditor.EditorUtility.DisplayDialog("ERROR", "NONE는 스크립트를 생성할수 없습니다.", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(_path) == true)
        {
            UnityEditor.EditorUtility.DisplayDialog("ERROR", "파일 경로가 없습니다.", "OK");
            return false;
        }
       
        if (System.IO.File.Exists(_path) == true)
        {
            UnityEditor.EditorUtility.DisplayDialog("ERROR", $"{_path} 스크립트가 있어 생성할수 없습니다.", "OK");
            return false;
        }

        string _directory = System.IO.Path.GetDirectoryName(_path);
        if (System.IO.Directory.Exists(_directory) == false)
        {
            System.IO.Directory.CreateDirectory(_directory);
        }

        string _className = System.IO.Path.GetFileNameWithoutExtension(_path);

        System.Text.StringBuilder _sb = new System.Text.StringBuilder();

        _sb.Append("using System.Collections.Generic;\n");
        _sb.Append("using UnityEngine;\n");
        _sb.Append("using UnityEngine.UI;\n");
        _sb.Append("\n");       
        _sb.AppendFormat("public class {0} : {1} \n", _className, _type.ToString());
        _sb.Append("{\n");

        _sb.Append("\n}");
        if (FileUtil.Save(_path, _sb.ToString()) == false)
            return false;

        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorUtility.DisplayDialog("SUCCESS", $"{_className} 생성완료했습니다.", "OK");

        return true;
    }

    public bool BakeKeynameScript()
    {
        string _keyName = "keyname";
        string _className = "UIPathTable";
        string _path = string.Format("{0}/Scripts/UI/UISystem/{1}_{2}.cs", Application.dataPath, _className, _keyName);

        System.Text.StringBuilder _sb = new System.Text.StringBuilder();

        _sb.AppendFormat("public static class {0}_{1} \n", _className, _keyName);
        _sb.Append("{\n");


        for (int i = 0; i < UIPathTable.Instance.list.Count; ++i)
        {
            UIPathRecord _record = UIPathTable.Instance.list[i];
            _sb.AppendFormat("\tstatic public readonly int {0} = {1};", _record.keyname, _record.id);
            _sb.Append("\n");
        }

        _sb.Append("\n}");
        FileUtil.Save(_path, _sb.ToString());
        return true;
    }
}
