using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    readonly int _poolCapacity;
    public int PoolCount {get; private set;}
    private Transform _parent, _prefab;    
    private Queue<GameObject> _pool;

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
            var temp = GameObject.Instantiate(prefab, _parent);
            temp.SetActive(false);
            _pool.Enqueue(temp);
            PoolCount++;
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
            // for (int i = 0; i < item.transform.childCount; i++)
            // {
            //     item.transform.GetChild(i).gameObject.SetActive(false);
            // }
            _pool.Enqueue(item);
            PoolCount++;
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
            var temp = _pool.Dequeue();
            temp.SetActive(true);
            // for (int i = 0; i < _temp.transform.childCount; i++)
            // {
            //     _temp.transform.GetChild(i).gameObject.SetActive(true);
            // }
            PoolCount--;
            return temp;
        }
        else
        {
            return null;
        }
    }
}
