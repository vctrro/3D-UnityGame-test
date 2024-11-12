using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnHit;
    private bool recivedHit = false;

    private void OnCollisionEnter(Collision collision)
    {

        recivedHit = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        OnHit.Invoke();
        StartCoroutine(RecivedHit());

    }

    private IEnumerator RecivedHit()
    {
        Debug.Log(recivedHit);
        yield return new WaitForSeconds(3f);
        //recivedHit = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
}
