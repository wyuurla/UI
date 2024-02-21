using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIHudGroup
 * @detail UIHud를 관리하는 클래스.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIHudGroup : MonoBehaviour
{
    MemoryPooling<UIHud> m_pool;

    public MemoryPooling<UIHud> pool { get { return m_pool; } }

    public void Init()
    {
        m_pool = new MemoryPooling<UIHud>(transform, 100);
    }
   
    public void Clear()
    {
        m_pool.Clear();
    }

    public void UpdateLogic()
    {
        if (null == m_pool)
            return;

        m_pool.UpdateLogic();
    }    

    /**
     * 유아이 위치를 월드위치로 변환하여 리턴한다.
     */
    static public Vector3 GetUI2World(Vector3 _uiPosition)
    {
        if (Camera.main == null)
            return Vector3.zero;

        if (UIManager.Instance == null || UIManager.Instance.uiCamera == null)
            return Vector3.zero;

        Vector3 _pos = Camera.main.ScreenToWorldPoint(UIManager.Instance.uiCamera.WorldToScreenPoint(_uiPosition));
        return _pos;
    }

    /**
     * 월드 위치를 유아이 위치로 변환하여 리턴하다.
     */
    static public Vector3 GetWorld2UI(Vector3 _worldPosition)
    {
        if (UIManager.Instance == null || UIManager.Instance.uiCamera == null)
            return Vector3.zero;

        Vector3 _pos = UIManager.Instance.uiCamera.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(_worldPosition));
        _pos.z = 0f;

        return _pos;
    }
}
