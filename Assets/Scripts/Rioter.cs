using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Rioter : MonoBehaviour {

    private NavMeshAgent navAgent;

    private Transform goal;

    public Transform EndGoal {
        get { return goal; }
        set { goal = value; }
    }

	// Use this for initialization
	void Start () {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.areaMask = 1 << 3;
        navAgent.SetDestination(goal.position);   
	}
}
