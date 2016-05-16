using UnityEngine;
using System.Collections.Generic;

public class CivilianSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab = null;

    [SerializeField]
    [Range(2, 20)]
    private int numCivies = 10;

    private Bounds bounds;

    public void NewRound(List<IAIGuy> pop)
    {
        GameObject temp;
        //Civilian script;
        Vector3 pos;
        RAIN.Core.AI rig;

        for (int i = 0; i < pop.Count; i++)
        {
            temp = pop[i].gameObject;
            temp.SetActive(false);

            rig = temp.GetComponentInChildren<RAIN.Core.AIRig>().AI;
            rig.Mind.AIInit();

            //script = temp.AddComponent<Civilian>();
            float x = Random.Range(-bounds.extents.x * transform.localScale.x, bounds.extents.x * transform.localScale.x) + transform.position.x;
            float z = Random.Range(-bounds.extents.z * transform.localScale.z, bounds.extents.z * transform.localScale.z) + transform.position.z;
            pos = new Vector3(x, 2, z);
            temp.transform.position = pos;
            temp.GetComponentInChildren<MeshRenderer>().material.color = Color.green;

            temp.name = "Civilian";
            temp.SetActive(true);
        }
    }

    // Use this for initialization
    void Awake () {
        SimManager.Instance.CivSpawners.Add(this);
        bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;
        GameObject temp;
        Civilian civ;

        for(int i = 0; i < numCivies; i++)
        {
            temp = GameObject.Instantiate(charPrefab);
            temp.SetActive(false);

            civ = temp.GetComponent<Civilian>();

            SimManager.Instance.CivPop.Add(civ);
            SimManager.Instance.onStateChanged += civ.SimStateChange;
        }

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
