using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIFadeDialog
 * @detail 페이드 인아웃에 사용하는 기본클래스
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIFadeDialog : UIDialog
{
    public enum eFADE_STATE
    {
        NONE,
        FADE_IN,
        FADE_OUT,
    }

    protected eFADE_STATE m_state;
    protected float m_duration;
    protected UnityEngine.Events.UnityAction m_complete;
    
    public virtual void Open(eFADE_STATE _state, float _duration, UnityEngine.Events.UnityAction _complete)
    {
        base.Open();
        m_state = _state;
        m_duration = _duration;
        m_complete = _complete;
    }
}