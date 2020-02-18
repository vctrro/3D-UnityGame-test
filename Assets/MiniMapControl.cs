using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapControl : MonoBehaviour
{
    private Transform _player;
    private void Start()
    {
        _player = GameObject.Find("Player").transform;
        transform.up = _player.forward;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(90, _player.position.y, 0);
    }
}
