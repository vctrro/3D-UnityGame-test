using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ReactiveTarget : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnHit;
    [HideInInspector] public UnityEvent OnDead;
    private SceneController controller;

    private void OnEnable()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;        
    }

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<SceneController>();
    }
    public void ReactToHit()
    {
        OnHit.Invoke();
        GetComponent<AudioSource>().Stop();
        GetComponent<Rigidbody>().freezeRotation = false;
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);
        if (!controller.enemyPool.Push(this.gameObject))
        {
            GameObject.Destroy(this.gameObject);
        }
        OnDead.Invoke();
    }
}
