using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UIHud
 * @detail ĳ���Ϳ� �Ӹ��� ó���ϴ� ������ �⺻Ŭ����, ��ȭ ȹ�� ������ ����Ѵ�.
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
