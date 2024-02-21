using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIManager
 * @detail 유아이를 관리하는 클래스.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIManager : MonoSingleton<UIManager>, IManager
{
    public UIDialogGroup dialog;
    public UIHudGroup hud;
    public UIDialogGroup tutorial;
    public UIFadeGroup fade;
    public UIDialogGroup popup;
    public Camera uiCamera;

    protected MemoryPooling<UIDialog> m_attachDialog;

    public void Init()
    {
        dialog.Init();
        hud.Init();
        tutorial.Init();
        fade.Init();
        popup.Init();

        m_attachDialog = new MemoryPooling<UIDialog>(transform, 100);
    }

    public void UpdateLogic()
    {
        dialog.UpdateLogic();
        hud.UpdateLogic();
        tutorial.UpdateLogic();
        fade.UpdateLogic();
        popup.UpdateLogic();

        m_attachDialog.UpdateLogic();
    }
  
    public void Clear()
    {
        dialog.Clear();
        hud.Clear();
        tutorial.Clear();
    }

    #region - attach dialog
    public virtual UIDialog OpenAttachDialog(string _path, Transform _attach)
    {
        UIDialog _dlg = m_attachDialog.Get(_path);
        if (null == _dlg)
            return null;

        _dlg.transform.SetParent(_attach);
        _dlg.transform.localScale = Vector3.one;
        _dlg.transform.localPosition = Vector3.zero;
        _dlg.transform.rotation = Quaternion.identity;
        _dlg.Open();
        return _dlg;
    }

    public virtual T GetAttachDialog<T>(string _path, Transform _attach) where T : UIDialog
    {
        T _dlg = m_attachDialog.Get(_path) as T;
        _dlg.transform.SetParent(_attach);
        return _dlg;
    }

    public virtual T FindAttachDialog<T>(string _path) where T : UIDialog
    {
        return m_attachDialog.Find(_path) as T;
    }
    #endregion
}
