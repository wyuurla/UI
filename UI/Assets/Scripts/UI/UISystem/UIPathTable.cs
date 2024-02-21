using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public enum eUIPATH_TYPE
{
    NONE,   
    UIDialog, 
    UISlot, 
    UIHud,
}


[System.Serializable]
public class UIPathRecord
{
    public int id;
    public bool isBaseClass = true;
    public eUIPATH_TYPE uiType;
    public string keyname;
    public string path;
}


[System.Serializable]
public class UIPathTable : ScriptableObject
{
    public static UIPathTable m_instnace;
    public static UIPathTable Instance
    {
        get
        {
            if(m_instnace == null )
            {
                if (ResUtil.IsHave(resPath) == true)
                {
                    m_instnace = ResUtil.Load<UIPathTable>(resPath);
                }
                else
                {
                    m_instnace = ScriptableObject.CreateInstance<UIPathTable>();
                }
            }

            return m_instnace;
        }
    }
    public static string directory
    {
        get
        {
            return string.Format("{0}/Resources/Table", Application.dataPath);
        }
    }
    public static string path
    {
        get
        {
            return "Assets/Resources/Table/UIPathTable.asset";
        }
    }
    public static string resPath
    {
        get
        {
            return "Table/UIPathTable";
        }
    }

    public List<UIPathRecord> list = new List<UIPathRecord>();

    public UIPathRecord Get(int _id)
    {
        return list.Find(item => item.id == _id);
    }

    public string GetPath(int _id)
    {
        UIPathRecord _record = Get(_id);
        if (null == _record)
            return null;

        return _record.path;
    }

    public void Sort()
    {
        list.Sort((item1, item2) => item1.id.CompareTo(item2.id));
    }
    public bool IsHas(int _id)
    {
        return Get(_id) != null;
    }

    public void Delete(int _id )
    {
        UIPathRecord _find = Get(_id);
        list.Remove(_find);
        Sort();
    }
    public UIPathRecord Add(int _id)
    {
        if (Get(_id) != null)
        {
            Debug.LogError(this.ToString() + "::Add() same id : " + _id);
            return null;
        }

        UIPathRecord _addRecord = new UIPathRecord();
        _addRecord.id = _id;
        list.Add(_addRecord);
        Sort();
        return _addRecord;
    }

    public bool IsHasName(string _name)
    {
        for(int i=0; i<list.Count; ++i )
        {
            if (string.Compare(list[i].keyname, _name, true) == 0)
                return true;
        }

        return false;
    }

    public int GetEmptyId()
    {
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].id != i+1)
                return i;
        }

        return list.Count + 1;
    }

    public void Save()
    {
#if UNITY_EDITOR
        if (System.IO.Directory.Exists(directory) == false)
        {
            System.IO.Directory.CreateDirectory(directory);
        }

        if( ResUtil.IsHave(resPath) == false )
        {
            AssetDatabase.CreateAsset(this, path);
        }

        EditorUtility.SetDirty(this);
#endif
    }
}