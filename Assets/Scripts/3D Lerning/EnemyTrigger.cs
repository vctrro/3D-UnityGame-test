using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    [HideInInspector] public OnTriggerDetected OnTargetDetected;
    [HideInInspector] public UnityEvent OnTargetLost;

    private void OnTriggerStay(Collider other)
    {
        if (Physics.Linecast(transform.position, other.transform.position, LayerMask.GetMask("Default")))  //если что-то есть между ним и целью
        {
            OnTargetLost.Invoke();
        }
        else
        {
            OnTargetDetected.Invoke(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnTargetLost.Invoke();
    }

    [System.Serializable]
    public class OnTriggerDetected : UnityEvent<Vector3>
    {

    }
}
