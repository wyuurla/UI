using System.Collections.Generic;
using UnityEngine;

/**
 * @brief MemoryPooling
 * @detail 메모리 풀링.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class MemoryPooling<T> where T : MonoBase
{
    public delegate T AddComponentDelegate(GameObject _object);
    protected List<MemoryPoolingItem<T>> m_activeList = new List<MemoryPoolingItem<T>>();
    protected List<MemoryPoolingItem<T>> m_hideList = new List<MemoryPoolingItem<T>>();
    protected Transform m_attach;
    protected int m_maxCount = 100;

    public List<MemoryPoolingItem<T>> activeList { get { return m_activeList; } }
    public List<MemoryPoolingItem<T>> hideList { get { return m_hideList; } }

    public MemoryPooling(Transform _attach, int _maxCount)
    {
        m_attach = _attach;
        m_maxCount = _maxCount;
    }

    /**
     * 모든 오브젝트를 삭제한다.
     */
    public void Clear()
    {
        DeleteList(m_activeList);
        DeleteList(m_hideList);
    }

    /**
     * 활성화 되어 있는 모든 오브젝트를 비활성화처리한다.
     */
    public void Close()
    {
        for( int i=0;i<m_activeList.Count; ++i )
        {
            if (null == m_activeList[i])
                continue;
            if (null == m_activeList[i].item)
                continue;
            m_activeList[i].item.Close();
            m_hideList.Add(m_activeList[i]);
        }
        m_activeList.Clear();
    }

    /**
     * 오브젝트를 리턴한다.
     */
    public T Get(string _path, AddComponentDelegate _addComponentDelegate = null)
    {
        MemoryPoolingItem<T> _getItem = GetItem(_path, _addComponentDelegate);
        if (null == _getItem)
            return null;

        return _getItem.item;
    }

    /**
     * 풀링 아이템을 리턴한다. 비활성화된 오브젝트가 없다면 생성해서 리턴한다. 
     * path : 파일 경로
     * addComponentDelegate : MonoBase 클래스가 프리팹에 없을때 사용한다. 게임오브젝트에 MonoBase를 추가하는 코드가 필요하다.
     */
    public MemoryPoolingItem<T> GetItem(string _path, AddComponentDelegate _addComponentDelegate)
    {
        if (string.IsNullOrWhiteSpace(_path) == true)
        {
            Debug.LogError("error null");
            return null;
        }

        int _resKey = _path.ToLower().GetHashCode();
        MemoryPoolingItem<T> _findItem = m_hideList.Find(item => item == null ? false : item.resKey == _resKey);

        if( null == _findItem )
        {
            GameObject _loadGameObject = ResUtil.Create(_path, m_attach);
            if (null == _loadGameObject)
                return null;

            T _component = _loadGameObject.GetComponent<T>();
            if( null == _component)
            {
                if( null != _addComponentDelegate)
                {
                    _component = _addComponentDelegate(_loadGameObject);
                }
            }

            if( null == _component )
            {
                Debug.LogError("MemoryPooling::GetItem()[ null == Component ] : " + typeof(T).Name);
                return null;
            }

            _component.Init();
            _findItem = new MemoryPoolingItem<T>(_resKey, _component);
            m_activeList.Add(_findItem);
        }
        else
        {
            m_hideList.Remove(_findItem);
            if(_findItem.item == null )
            {
                Debug.LogError("null == _findItem.item : " + _path);
            }
            else
            {
                m_activeList.Add(_findItem);
                _findItem.item.gameObject.SetActive(true);
            }           
        }

        return _findItem;
    }

    /**
     * 활성화되어 있는 오브젝트를 찾아 리턴한다.
     */
    public T Find(string _path)
    {
        if (string.IsNullOrWhiteSpace(_path) == true)
            return null;

        int _resKey = _path.ToLower().GetHashCode();

        MemoryPoolingItem<T> _item = m_activeList.Find(item => item.resKey == _resKey);
        if (null == _item)
            return null;

        return _item.item;
    }

    /**
     * 활성화되어 있는 오브젝트 모두를 찾아 리턴한다.
     */
    public List<T> FindAll(string _path)
    {
        if (string.IsNullOrWhiteSpace(_path) == true)
            return null;

        int _resKey = _path.ToLower().GetHashCode();

        List<MemoryPoolingItem<T>> _items = m_activeList.FindAll(item => item.resKey == _resKey);
        if (null == _items)
            return null;

        List<T> _resultList = new List<T>();
        for(int i=0;i< _items.Count; ++i )
        {
            _resultList.Add(_items[i].item);
        }

        return _resultList;
    }

   
    /**
     * 업데이트 함수.
     * 활성화 되어 있는 오브젝트가 null이면 리스트에서 제거하고 비활성화되어 있으면 비활성화 리스트로 이동시킨다.
     */
    public void UpdateLogic()
    {
        int _index = 0;
        while(m_activeList.Count > _index)
        {
            MemoryPoolingItem<T> _activeItem = m_activeList[_index];
            if( null == _activeItem.item )
            {
                m_activeList.RemoveAt(_index);
                continue;
            }

            if( _activeItem.item.isOpen == false )
            {
                m_activeList.RemoveAt(_index);
                if (m_maxCount <= m_hideList.Count)
                {
                    GameObject.Destroy(_activeItem.item.gameObject);
                }
                else
                {
                    m_hideList.Add(_activeItem);
                }
                continue;
            }

            _activeItem.item.UpdateLogic();
            ++_index;
        }
    }

    /**
     * 게임오브젝트르 삭제한다.
     */
    private void DeleteList(List<MemoryPoolingItem<T>> _list)
    {
        if (null == _list)
            return;
        for (int i = 0; i < _list.Count; ++i)
        {
            if (null == _list[i])
                continue;
            if (null == _list[i].item)
                continue;
            GameObject.Destroy(_list[i].item.gameObject);
        }

        _list.Clear();
    }
}
