using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ReactiveTarget : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnHit;
    private SceneController controller;

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<SceneController>();
    }
    public void ReactToHit()
    {
        OnHit.Invoke();
        GetComponent<Rigidbody>().freezeRotation = false;
        StartCoroutine(Die());
        controller.OnDead();
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        if (!controller.enemyPool.Push(this.gameObject))
        {
            GameObject.Destroy(this.gameObject);
        }
        Debug.Log($"Pool count = {controller.enemyPool._pool.Count}");
    }
}
