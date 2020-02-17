using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    readonly int _poolCapacity;
    private Transform _parent, _prefab;    
    private Queue<GameObject> _pool;
    private GameObject _temp;

    public Pool(GameObject prefab, int capacity, Transform parent = null)
    {
        _poolCapacity = capacity;
        _prefab = prefab.transform;
        _pool = new Queue<GameObject>(_poolCapacity);
        if (parent != null)
        {
            _parent = parent;
        }
        else
        {
            _parent = new GameObject(prefab.name + "sPool").transform;
            _parent.Translate(Vector3.zero);
        }
        for (int i = 0; i < capacity; i++)
        {
            _pool.Enqueue(GameObject.Instantiate(prefab, _parent));
        }

    }

    public bool Push(GameObject item)
    {
        if (_pool.Count <= _poolCapacity)
        {
            item.transform.rotation = _prefab.rotation;
            item.transform.position = _prefab.position;
            item.transform.parent = _parent;
            item.SetActive(false);
            _pool.Enqueue(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject Pop()
    {
        if (_pool.Count != 0)
        {
            _temp = _pool.Dequeue();
            _temp.SetActive(true);
            return _temp;
        }
        else
        {
            return null;
        }
    }
}
