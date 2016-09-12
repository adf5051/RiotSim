using UnityEngine;
using System.Collections.Generic;

public class RiotSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab = null;

    [SerializeField]
    [Range(2,20)]
    private int numRioters = 10;

    private Bounds bounds;

    public void NewRound(List<IAIGuy> pop)
    {
        GameObject temp;
        Vector3 pos;
        RAIN.Core.AI rig;

        for (int i = 0; i < pop.Count; i++)
        {
            temp = pop[i].gameObject;

            rig = temp.GetComponentInChildren<RAIN.Core.AIRig>().AI;
            rig.Mind.AIInit();

            float x = Random.Range(-bounds.extents.x * transform.localScale.x, bounds.extents.x * transform.localScale.x) + transform.position.x;
            float z = Random.Range(-bounds.extents.z * transform.localScale.z, bounds.extents.z * transform.localScale.z) + transform.position.z;
            pos = new Vector3(x, 2, z);
            temp.transform.position = pos;
            temp.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            temp.name = "Rioter";
            temp.SetActive(true);
        }
    }

	// Use this for initialization
	void Awake () {
        SimManager.Instance.RiotSpawners.Add(this);

        bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

        GameObject temp;
        Rioter rioter;

	    for(int i = 0; i < numRioters; i++) {
            temp = GameObject.Instantiate(charPrefab);
            //temp.SetActive(false);

            rioter = temp.GetComponent<Rioter>();
            rioter.Genes = new System.Collections.BitArray(9);

            for (int j = 0; j < 9; j++)
            {
                int r = Random.Range(0, 1);
                bool bit = r == 1;
                rioter.Genes[j] = bit;
            }

            SimManager.Instance.RiotPop.Add(rioter);
            SimManager.Instance.onStateChanged += rioter.SimStateChange;
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
