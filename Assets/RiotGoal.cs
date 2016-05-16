using UnityEngine;
using System.Collections;

public class RiotGoal : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.tag != "Rioter")
        {
            return;
        }

        SimManager.Instance.RiotReachedGoal = true;
    }
}
