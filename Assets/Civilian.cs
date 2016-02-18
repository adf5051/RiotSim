using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Civilian : MonoBehaviour {

    private NavMeshAgent navAgent;

    private const float radius = 2;
    private const float distAhead = 2f;
    private const float timeBetweenPoints = .5f;
    private const int areaMask = 1 << 4;

	// Use this for initialization
	void Start () {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.areaMask = areaMask;
        StartCoroutine(FindNextPoint());
	}
	
    IEnumerator FindNextPoint() {
        float angle = Random.rotation.eulerAngles.y;
        Vector3 point = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        point = transform.position + (transform.forward * distAhead) + point;

        NavMeshHit hit;
        NavMesh.SamplePosition(point, out hit, 100, areaMask);
        navAgent.SetDestination(hit.position);

        yield return new WaitForSeconds(timeBetweenPoints);
        StartCoroutine(FindNextPoint());
    }

}
