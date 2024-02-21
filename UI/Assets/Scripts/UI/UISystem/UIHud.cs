using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIHud
 * @detail 캐릭터에 머리에 처리하는 유아이 기본클래스, 재화 획득 연출등에서 사용한다.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UIHud : UIBase
{
    public RectTransform panel;
  
    public virtual void SetPosition(Vector3 _position)
    {
        transform.position = _position;
    }
}
