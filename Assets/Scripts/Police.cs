using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Police : MonoBehaviour {

    public Transform[] Patrol {
        get; set;
    }

    private NavMeshAgent navAgent;
    public int destPoint = 0;

    private int areaMask = (1 << 5) | (1 << 4);

	// Use this for initialization
	void Start () {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.areaMask = areaMask;
        navAgent.autoBraking = false;

        GoToNextPoint();
    }

	void GoToNextPoint() {
        if (Patrol.Length == 0)
            return;

        navAgent.destination = Patrol[destPoint].position;

        destPoint = (destPoint + 1) % Patrol.Length;
    }

	// Update is called once per frame
	void Update () {
        if (navAgent.remainingDistance < 0.5f)
            GoToNextPoint();
    }
}
