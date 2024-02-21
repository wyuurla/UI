using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @brief UIAction
 * @detail 유아이 연출 기본 클래스.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIAction
{
    public enum eACTION_TYPE
    {
        NONE = 0,
        SIZE_TWEEN,
    }

    public eACTION_TYPE actionType;
    public UIAction(eACTION_TYPE _type)
    {
        actionType = _type;
    }

    public virtual bool IsAction()
    {
        return false;
    }
   
    public virtual void PlayOpen(UnityEngine.Events.UnityAction _complete)
    {
        if (null != _complete)
            _complete();
    }
    

    public virtual void PlayClose(UnityEngine.Events.UnityAction _complete)
    {
        if (null != _complete)
            _complete();
    }

    public virtual void PlayClick(UnityEngine.Events.UnityAction _complete)
    {
        if (null != _complete)
            _complete();
    }

    public virtual void UpdateLogic()
    {

    }
}
