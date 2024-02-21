using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/**
 * @brief UIButton
 * @detail 버튼을 상속받아 기능을 추가.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIButton : Button
{ 
    public UISound.eSOUND_ID sound_click = UISound.eSOUND_ID.button_click;
    public UIAction.eACTION_TYPE actionType = UIAction.eACTION_TYPE.SIZE_TWEEN;

    protected UIAction m_action;

    public UIAction getAction
    {
        get
        {
            if (null == m_action || m_action.actionType != actionType )
                m_action = new UIAction(actionType);
            return m_action;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        UISound.PlaySound(sound_click);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (interactable == false)
            return;

        getAction.PlayClick(null);
    }
}
