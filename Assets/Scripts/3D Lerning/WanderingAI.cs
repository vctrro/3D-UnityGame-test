using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float obstacleRange = 7.0f;
    private bool _alive, _attack;
    private float _attackSpeed;
    private Vector3 _targetAttack;

    private void OnEnable()
    {
        _alive = true;        
    }
    private void Start()
    {
        _attackSpeed = speed * 2.5f;
        GetComponent<ReactiveTarget>().OnHit.AddListener(SetAlive);
        EnemyTrigger temp = GetComponentInChildren<EnemyTrigger>();
        Debug.Log(temp);
        temp.OnTargetDetected.AddListener((target)=>{_targetAttack = target; _attack = true;});
        temp.OnTargetLost.AddListener(SetAlive);
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
