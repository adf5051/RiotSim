using UnityEngine;
using System.Collections;

public class BarrierStorage : MonoBehaviour {

    public GameObject obstaclePrefab;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Police")
        {

        }
    }
}
