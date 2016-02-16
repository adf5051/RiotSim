using UnityEngine;
using System.Collections;

public class RiotSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab;

    [SerializeField]
    [Range(2,20)]
    private int numRioters;

    [SerializeField]
    private Transform riotGoal;

    private Bounds bounds;

	// Use this for initialization
	void Start () {

        bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

        GameObject temp;
        Rioter script;
        Vector3 pos;
	    for(int i = 0; i < numRioters; i++) {
            temp = GameObject.Instantiate(charPrefab);
            script = temp.AddComponent<Rioter>();
            script.EndGoal = riotGoal;
            float x = Random.Range(-bounds.extents.x * transform.localScale.x, bounds.extents.x * transform.localScale.x) + transform.position.x;
            float z = Random.Range(-bounds.extents.z * transform.localScale.z, bounds.extents.z * transform.localScale.z) + transform.position.z;
            pos = new Vector3(x, 2, z);
            temp.transform.position = pos;
        }
        Debug.Break();
	}
}
