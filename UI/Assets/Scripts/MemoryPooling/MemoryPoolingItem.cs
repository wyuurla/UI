
/**
 * @brief MemoryPoolingItem
 * @detail 메모리 풀링 아이템.
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
