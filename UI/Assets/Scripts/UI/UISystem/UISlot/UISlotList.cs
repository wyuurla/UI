using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief UISlotList
 * @detail UISlot 리스트를 관리하는 클래스.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UISlotList : UIBase
{
    public Transform attach;
    public ScrollRect scrollRect;
    public string slotPath;

    protected List<UISlot> m_slotList = new List<UISlot>();
    protected List<UISlot> m_activeList = new List<UISlot>();
    protected UnityEngine.Events.UnityAction<UISlot> m_clickAction;
    protected UISlot m_select;

    public List<UISlot> activeList { get { return m_activeList; } }
    public UISlot getSelect { get { return m_select; } }

    public override void Open()
    {
        base.Open();
        CloseSlotList();
    }

    /**
     * 기본 슬롯의 버튼을 클릭할때 호출할 함수를 연결한다.
     */
    public void SetClickAction(UnityEngine.Events.UnityAction<UISlot> _clickAction)
    {
        m_clickAction = _clickAction;
    }

    /**
     * 모든 슬롯들을 비활성화한다.
     */
    public virtual void CloseSlotList()
    {
        SetSelect(null);        
        m_activeList.ForEach(item => item.Close());
        m_slotList.AddRange(m_activeList);
        m_activeList.Clear();
    }
    /**
     * 활성화되어 있는 슬롯들의 ResetUI() 함수를 호출한다.
     */
    public override void ResetUI()
    {
        base.ResetUI();
  
        for( int i=0; i< m_activeList.Count; ++i )
        {
            m_activeList[i].ResetUI();
        }        
    }

    /**
    * 슬롯을 선택한다.
    */
    public virtual void SetSelect(int _idx)
    {
        if (m_activeList.Count <= _idx)
        {
            Debug.LogError("over : " + m_activeList.Count.ToString() + " : " + _idx.ToString());
            return;
        }

        if( _idx < 0 )
        {
            Debug.LogError("over : " + m_activeList.Count.ToString() + " : " + _idx.ToString());
            return;
        }

        SetSelect(m_activeList[_idx]);
    }

    /**
     * 슬롯을 선택한다.
     */
    public virtual void SetSelect(UISlot _slot)
    {
        m_activeList.ForEach(item => item.SetSelect(false));
        m_select = _slot;
        if (null != m_select)
            m_select.SetSelect(true);
    }

    /**
     * 슬롯을 생성해서 리턴한다. 비활성화된 슬롯이 없으면 생성해서 리턴한다.
     */
    public virtual UISlot GetSlot()
    {
        UISlot _slot = null;
        if (m_slotList.Count <= 0)
        {
            _slot = ResUtil.Create<UISlot>(slotPath, attach);
            if (null == _slot)
                return null;

            _slot.gameObject.name = $"slot_{(m_slotList.Count+m_activeList.Count)}";
            _slot.Init();
            _slot.clickAction = m_clickAction;
            _slot.Open();
            m_activeList.Add(_slot);
            return _slot;
        }

        _slot = m_slotList[0];
        _slot.clickAction = m_clickAction;
        _slot.Open();
        _slot.transform.SetAsLastSibling();
        m_activeList.Add(_slot);
        m_slotList.RemoveAt(0);
        return _slot;
    }

    /**
     * 템플릿타입으로 캐스팅해서 리턴한다.
     */
    public virtual T GetSlot<T>() where T : UISlot
    {
       return GetSlot() as T;
    }

    /**
     * 스크롤한다.
     */
    public virtual void SetScroll(UISlot _slot, bool _horizontal = false)
    {
        if (null == _slot)
            return;

        if (null == scrollRect.content)
            return;

        RectTransform targetRectTransform = _slot.transform.GetComponent<RectTransform>();
        if (null == targetRectTransform)
            return;

        Vector3 targetPosition = -targetRectTransform.anchoredPosition;
        if(_horizontal == true )
        {
            scrollRect.content.anchoredPosition = new Vector2(targetPosition.x, scrollRect.content.anchoredPosition.y);
        }
        else
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.y, targetPosition.x);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    
        for( int i=0; i<m_activeList.Count; ++i )
        {
            m_activeList[i].UpdateLogic();
        }
    }
}
