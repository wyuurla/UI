using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * @brief UISlot
 * @detail 슬롯 리스트에서 사용하는 슬롯 기본클래스.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UISlot : UIBase
{
    public Image[] icon;
    public Button[] btnClick;
    public GameObject select;
    public RectTransform panel;

    public UnityEngine.Events.UnityAction<UISlot> clickAction;

    public bool isSelect { get { return UIUtil.IsShow(select); } }

    public void SetShow(bool _isShow)
    {
        UIUtil.SetShow(panel, _isShow);
    }

    public override void Init()
    {
        base.Init();
        UIUtil.AddClickEvent(btnClick, OnClick);
    }

    public override void Open()
    {
        base.Open();
        gameObject.SetActive(true);
        SetSelect(false);
        SetShow(true);
    }

    public virtual void SetSelect(bool _isShow)
    {
        UIUtil.SetShow(select, _isShow);
    }

    public virtual void OnClick()
    {
        if (null != clickAction)
            clickAction(this);
    }
}
