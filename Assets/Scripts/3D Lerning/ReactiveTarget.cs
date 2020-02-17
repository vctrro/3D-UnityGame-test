using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ReactiveTarget : MonoBehaviour
{
    public UnityEvent OnHit;
    public void ReactToHit()
    {
        OnHit.Invoke();
        GetComponent<Rigidbody>().freezeRotation = false;
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
}
