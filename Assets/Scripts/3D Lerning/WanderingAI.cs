using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float obstacleRange = 7.0f;
    private bool _alive;
    private void Start()
    {
        gameObject.GetComponent<ReactiveTarget>().OnHit.AddListener(SetAlive);
        _alive = true;
    }

    private void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, this.GetComponent<CapsuleCollider>().radius * this.transform.localScale.y, out hit, obstacleRange))
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    private void SetAlive()
    {
        if (!_alive) return;
        _alive = false;
        Debug.Log("Enemy dead");
    }
}
