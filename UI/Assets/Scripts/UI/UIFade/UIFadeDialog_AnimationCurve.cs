using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief UIFadeDialog_AnimationCurve
 * @detail AnimationCurve를 사용해서 이미지의 알파값으로 페이드 인아웃한다.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIFadeDialog_AnimationCurve : UIFadeDialog
{
    public Image image;
    public AnimationCurve curve;
    protected float m_time;

    public override void Open(eFADE_STATE _state, float _duration, UnityEngine.Events.UnityAction _complete)
    {
        base.Open(_state, _duration, _complete);
        UIUtil.SetAlpha(image, m_state == eFADE_STATE.FADE_IN ? 0f : 1f);
        m_time = 0f;
        m_duration = _duration;
        if(m_duration <= 0f )
        {
            Debug.LogError("_duration <= 0f");
            m_duration = 1f;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (m_state == eFADE_STATE.NONE)
            return;

        if (m_duration <= 0f)
        {
            m_time = 1f;
        }
        else
        {
            m_time += Time.deltaTime / m_duration;
        }

        if (m_state == eFADE_STATE.FADE_IN)
        {
            UIUtil.SetAlpha(image, curve.Evaluate(m_time));
        }
        else
        {
            UIUtil.SetAlpha(image, 1f - curve.Evaluate(m_time));
        }
        
        if( m_time >= 1f )
        {
            if (m_state == eFADE_STATE.FADE_OUT)
                Close();

            m_state = eFADE_STATE.NONE;
            if (null != m_complete)
                m_complete();
        }
    }
}
