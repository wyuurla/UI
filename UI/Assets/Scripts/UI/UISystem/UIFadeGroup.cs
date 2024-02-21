using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIFadeGroup
 * @detail 페이드인 유아이를 관리한다.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIFadeGroup : UIDialogGroup
{
    UIFadeDialog m_fadeDialog;

    public void FadeIn_AnimationCurve(UnityEngine.Events.UnityAction _complete, float _duration)
    {
        if (UIUtil.IsOpen(m_fadeDialog) == false)
        {
            m_fadeDialog = GetDialog<UIFadeDialog_AnimationCurve>("UI/UIFade/UIFadeDialog_AniCurve");
        }

        m_fadeDialog.Open( UIFadeDialog.eFADE_STATE.FADE_IN, _duration, _complete);
    }

    public void FadeOut_AnimationCurve(UnityEngine.Events.UnityAction _complete, float _duration)
    {
        if (UIUtil.IsOpen(m_fadeDialog) == false)
        {
            m_fadeDialog = GetDialog<UIFadeDialog_AnimationCurve>("UI/UIFade/UIFadeDialog_AniCurve");
        }

        m_fadeDialog.Open(UIFadeDialog.eFADE_STATE.FADE_OUT, _duration, _complete );
    }

    public bool IsFade()
    {
        if (UIUtil.IsOpen(m_fadeDialog) == true)
            return true;

        return false;
    }
}
