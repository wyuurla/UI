using UnityEngine;

/**
 * @brief MonoBase
 * @detail  메모리 풀링에서 사용하는 상위 클래스. 
 *          메모리 풀링에서 사용할려면 이클래스를 상속받아야한다.
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
