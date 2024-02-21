using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIDialogGroup
 * @detail ���̾�α� �����̸� �����Ѵ�.
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
     * ��� Dialog�� Close�Լ��� ������� ��Ȱ��ȭ ���·� �����.
     */
    public virtual void Close()
    {
        m_pool.Close();
    }

    /**
     * ��� Dialog ���� ������Ʈ�� �����Ѵ�.
     */
    public virtual void Clear()
    {
        m_pool.Clear();
    }


    /**
     * UIPathTable_keyname ����Ͽ� �����̸� ���½�Ų��.
     */
    public virtual UIDialog OpenDialog(int _id)
    {
        return OpenDialog(UIPathTable.Instance.GetPath(_id));
    }

    /**
     * UIPathTable_keyname ����Ͽ� �����̸� �����ؼ� �����Ѵ�.
     */
    public virtual T GetDialog<T>(int _id) where T : UIDialog
    {
        return GetDialog<T>(UIPathTable.Instance.GetPath(_id));
    }

    /**
     * path ����� �����̸� ���½�Ų��.
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
     * path ����� �����̸� �����ؼ� �����Ѵ�.
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
