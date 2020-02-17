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
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        // GetComponent<Rigidbody>().freezeRotation = true;
        if (!controller.enemyPool.Push(this.gameObject))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
