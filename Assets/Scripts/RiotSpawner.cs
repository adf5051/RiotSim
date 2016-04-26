using UnityEngine;
using System.Collections;

public class RiotSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab = null;

    [SerializeField]
    [Range(2,20)]
    private int numRioters = 10;

    private Bounds bounds;



	// Use this for initialization
	void Start () {

        bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

        GameObject temp;
        //RAIN.Core.AI rig;
        Vector3 pos;
        Rioter rioter;

	    for(int i = 0; i < numRioters; i++) {
            temp = GameObject.Instantiate(charPrefab);

            rioter = temp.GetComponent<Rioter>();
            rioter.Genes = new System.Collections.BitArray(9);

            for (int j = 0; j < 9; j++)
            {
                int r = Random.Range(0, 1);
                bool bit = r == 1;
                rioter.Genes[j] = bit;
            }

            //rig = temp.GetComponentInChildren<RAIN.Core.AIRig>().AI;

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
