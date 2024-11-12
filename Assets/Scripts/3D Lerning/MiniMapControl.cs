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
        transform.position = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        // transform.rotation = Quaternion.identity;
        // transform.rotation = Quaternion.Euler(90, 0, -_player.eulerAngles.y);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _player.eulerAngles.y, transform.eulerAngles.z);
    }
}
