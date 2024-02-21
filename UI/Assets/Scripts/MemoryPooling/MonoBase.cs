using UnityEngine;

/**
 * @brief MonoBase
 * @detail  �޸� Ǯ������ ����ϴ� ���� Ŭ����. 
 *          �޸� Ǯ������ ����ҷ��� ��Ŭ������ ��ӹ޾ƾ��Ѵ�.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class MonoBase : MonoBehaviour
{
    public bool isOpen { get { return gameObject.activeSelf; } }

    public virtual void Init() { }
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void UpdateLogic()
    {
    }
}
