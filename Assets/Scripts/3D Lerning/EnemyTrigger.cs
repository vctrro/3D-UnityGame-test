using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector3> OnTargetDetected;
    [HideInInspector] public UnityEvent OnTargetLost;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            RaycastHit hit;
            Physics.Linecast(transform.position, other.transform.position, out hit);
            if (hit.collider.gameObject.name == "Player")
            {
                OnTargetDetected.Invoke(other.transform.position);
            }
            else
            {
                OnTargetLost.Invoke();
            }
        }
    }
}
