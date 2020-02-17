using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 3;
    private int _nextPoint = 0;


    private void Update()
    {
        if (_nextPoint < _points.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_nextPoint].position, Time.deltaTime * _speed);
            if (transform.position == _points[_nextPoint].position)
                {
                    _nextPoint++;
                }
        }        
    }
}
