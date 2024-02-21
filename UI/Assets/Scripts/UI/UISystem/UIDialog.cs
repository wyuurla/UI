using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief UIDialog
 * @detail 창 유아이 기본 클래스.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIDialog : UIBase
{
    public Image backImage;
    public RectTransform panel;

    public Button[] btnCloses;
    public UIAction.eACTION_TYPE actionType = UIAction.eACTION_TYPE.SIZE_TWEEN;
    public UISound.eSOUND_ID sound_open = UISound.eSOUND_ID.dialog_open;
    public UISound.eSOUND_ID sound_close = UISound.eSOUND_ID.dialog_close;

    protected RectTransform m_rectTransform;
    protected UIAction m_action;

    public UIAction getAction
    {
        get
        {
            if (null == m_action || m_action.actionType != actionType)
                m_action = new UIAction(actionType);
            return m_action;
        }
    }

    public override void Init()
    {
        base.Init();

        m_rectTransform = transform as RectTransform;
        UIUtil.AddClickEvent(btnCloses, OnClick_Close);
    }

    public override void Open()
    {
        base.Open();

        ResetRectTransform();
        getAction.PlayOpen(CompleteAction_Open);
        UISound.PlaySound(sound_open);
    }

    public override void Close()
    {
        if (getAction.IsAction() == true)
            return;

        UISound.PlaySound(sound_close);
        getAction.PlayClose(CompleteAction_Close);
        base.Close();
    }

    public virtual void CompleteAction_Open()
    {
    }

    public virtual void CompleteAction_Close()
    {
        base.Close();
    }

    public virtual void ResetRectTransform()
    {
        m_rectTransform.offsetMin = Vector2.zero;
        m_rectTransform.offsetMax = Vector2.zero;
        m_rectTransform.SetAsLastSibling();
    }

    public virtual void OnClick_Close()
    {
        Close();
    }
}
