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
        navAgent.SetDestination(goal.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
