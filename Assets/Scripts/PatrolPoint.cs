using UnityEngine;
using System.Collections;

public class PatrolPoint : MonoBehaviour {

    public Transform next;

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1);

        if(next)
            Gizmos.DrawLine(transform.position, next.position);
    }
}
