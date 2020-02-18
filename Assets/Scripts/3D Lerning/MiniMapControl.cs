using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapControl : MonoBehaviour
{
    private Transform _player;
    private void Start()
    {
        _player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        transform.rotation = _player.rotation;
    }
}
