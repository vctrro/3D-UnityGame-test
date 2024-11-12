using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAB : MonoBehaviour
{
    [SerializeField] private Transform _pointA, _pointB;
    [SerializeField] private float _speed = 3;
    private bool _toA = true;


    private void Update()
    {
        if (_toA)
        {
            MoveTo(_pointA.position);
        }
        else
        {
            MoveTo(_pointB.position);
        }
    }

    private void MoveTo(Vector3 point)
    {
            transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * _speed);
            if (transform.position == point)
                {
                    _toA = !_toA;
                }
    }
}
