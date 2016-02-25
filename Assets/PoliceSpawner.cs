using UnityEngine;
using System.Collections;

public class PoliceSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab;

    [SerializeField]
    [Range(1, 5)]
    private int numPolice;

    [SerializeField]
    private Transform[] patrolPoints;

	// Use this for initialization
	void Start () {
        int trans;
        GameObject temp;
        Police script;

        for (int i = 0; i < numPolice; i++) {
            trans = Random.Range(0, patrolPoints.Length);
            temp = GameObject.Instantiate(charPrefab);
            script = temp.AddComponent<Police>();
            script.Patrol = patrolPoints;
            script.destPoint = trans;
            temp.transform.position = patrolPoints[trans].position + Vector3.up;
            temp.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
            temp.name = "Police";
        }
	}
	
}
