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
        if (!recivedHit)
        {
            recivedHit = true;
            OnHit.Invoke();
            StartCoroutine(RecivedHit());
        }
     }

    private IEnumerator RecivedHit()
    {
        yield return new WaitForSeconds(1f);
        recivedHit = false;
    }
}
