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

    [SerializeField]
    private RAIN.BehaviorTrees.BTAsset riotBehavior;

    private Bounds bounds;

	// Use this for initialization
	void Start () {

        bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

        GameObject temp;
        RAIN.Core.AI rig;
        Vector3 pos;
	    for(int i = 0; i < numRioters; i++) {
            temp = GameObject.Instantiate(charPrefab);
            rig = temp.GetComponentInChildren<RAIN.Core.AIRig>().AI;
            RAIN.Minds.BasicMind mind = rig.Mind as RAIN.Minds.BasicMind;
            mind.SetTreeForBinding("Riot Tree", riotBehavior);
            mind.ReloadBinding("Riot Tree");

            float x = Random.Range(-bounds.extents.x * transform.localScale.x, bounds.extents.x * transform.localScale.x) + transform.position.x;
            float z = Random.Range(-bounds.extents.z * transform.localScale.z, bounds.extents.z * transform.localScale.z) + transform.position.z;
            pos = new Vector3(x, 2, z);
            temp.transform.position = pos;
            temp.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            temp.name = "Rioter";
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
