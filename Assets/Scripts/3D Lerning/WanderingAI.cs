using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f, _attackSpeed = 7.0f;
    [SerializeField] private float _obstacleRange = 7.0f;
    private bool _alive, _attack;
    private Vector3 _targetAttack;

    private void OnEnable()
    {
        _alive = true;        
    }
    private void Start()
    {
        GetComponent<ReactiveTarget>().OnHit.AddListener(SetAlive);
        EnemyTrigger temp = GetComponentInChildren<EnemyTrigger>();
        temp.OnTargetLost.AddListener(()=>{_attack = false;});
        temp.OnTargetDetected.AddListener(target=>{_targetAttack = target; _attack = true;});
    }

    private void Update()
    {
        if (!_alive) return;
        if (_attack)
        {
            transform.forward = _targetAttack - transform.position;
            transform.Translate(0, 0, _attackSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, this.GetComponent<CapsuleCollider>().radius * this.transform.localScale.y, out hit, _obstacleRange))
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
    }
}
