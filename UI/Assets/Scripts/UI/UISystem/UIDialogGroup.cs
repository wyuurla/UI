using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIDialogGroup
 * @detail 다이얼로그 유아이를 관리한다.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIDialogGroup : MonoBehaviour
{
    protected MemoryPooling<UIDialog> m_pool;

    public virtual void Init()
    {
        m_pool = new MemoryPooling<UIDialog>(transform, 100);
    }

    /**
     * 모든 Dialog를 Close함수를 실행시켜 비활성화 상태로 만든다.
     */
    public virtual void Close()
    {
        m_pool.Close();
    }

    /**
     * 모든 Dialog 게임 오브젝트를 삭제한다.
     */
    public virtual void Clear()
    {
        m_pool.Clear();
    }


    /**
     * UIPathTable_keyname 사용하여 유아이를 오픈시킨다.
     */
    public virtual UIDialog OpenDialog(int _id)
    {
        return OpenDialog(UIPathTable.Instance.GetPath(_id));
    }

    /**
     * UIPathTable_keyname 사용하여 유아이를 생성해서 리턴한다.
     */
    public virtual T GetDialog<T>(int _id) where T : UIDialog
    {
        return GetDialog<T>(UIPathTable.Instance.GetPath(_id));
    }

    /**
     * path 경로의 유아이를 오픈시킨다.
     */
    public virtual UIDialog OpenDialog(string _path)
    {
        UIDialog _dlg = m_pool.Get(_path);
        if (null == _dlg)
            return null;

        _dlg.Open();
        return _dlg;
    }

    /**
     * path 경로의 유아이를 생성해서 리턴한다.
     */
    public virtual T GetDialog<T>(string _path) where T : UIDialog
    {
        return m_pool.Get(_path) as T;
    }

    public virtual T FindDialog<T>(string _path) where T : UIDialog
    {
        return m_pool.Find(_path) as T;
    }

    public virtual List<UIDialog> FindAllDialog(string _path)
    {
        return m_pool.FindAll(_path);
    }

    public virtual void UpdateLogic()
    {
        m_pool.UpdateLogic();
    }
}
