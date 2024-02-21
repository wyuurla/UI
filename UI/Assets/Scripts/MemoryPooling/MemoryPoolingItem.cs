
/**
 * @brief MemoryPoolingItem
 * @detail �޸� Ǯ�� ������.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class MemoryPoolingItem<T> where T : MonoBase
{
    public int resKey;
    public T item;
    
    public MemoryPoolingItem(int _resKey, T _item)
    {
        resKey = _resKey;
        item = _item;
    }
}
