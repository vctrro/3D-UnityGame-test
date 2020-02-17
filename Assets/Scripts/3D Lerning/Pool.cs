using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 30;
    public int Capacity { get{return _poolCapacity;} }
    private Transform _parent;
    private Queue<GameObject> _pool;

    private void Init(Transform parent, int capacity)
    {
        _poolCapacity = capacity;
        _pool = new Queue<GameObject>(_poolCapacity);
        _parent = parent;
    }

    public bool Push(GameObject item)
    {
        if (_pool.Count <= _poolCapacity)
        {
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
            GameObject temp = _pool.Dequeue();
            temp.SetActive(true);
            return temp;
        }
        else
        {
            return null;
        }
    }
}
