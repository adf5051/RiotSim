using UnityEngine;
using RAIN.Navigation.Waypoints;
using System.Collections.Generic;

public class PoliceSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab = null;

    [SerializeField]
    [Range(1, 5)]
    private int numPolice = 2;

    [SerializeField]
    private Transform[] patrolPoints = null;

    public void NewRound(List<IAIGuy> pop)
    {
        int trans;
        GameObject temp;
        RAIN.Core.AI rig;
        Police p;
        NavMeshAgent agent;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            for(int i = 0; i < pop.Count; i++)
            {
                trans = Random.Range(0, patrolPoints.Length);
                temp = pop[i].gameObject;
                agent = temp.GetComponent<NavMeshAgent>();

                p = temp.GetComponent<Police>();
                p.Initialize();

                rig = temp.GetComponentInChildren<RAIN.Core.AIRig>().AI;
                rig.WorkingMemory.SetItem<Transform>("patrolRoute", patrolPoints[0].parent);
                rig.Mind.AIInit();

                p.KnowAboutRiot = false;
                rig.WorkingMemory.SetItem<bool>("RiotSpotted", false);

                temp.transform.position = patrolPoints[trans].position;
                temp.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
                temp.name = "Police";
                temp.SetActive(true);
                agent.ResetPath();
            }
        }
    }

    // Use this for initialization
    void Awake() {
        SimManager.Instance.PoliceSpawners.Add(this);

        GameObject temp;
        RAIN.Core.AI rig;
        Police p;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            for (int i = 0; i < numPolice; i++)
            {

                temp = GameObject.Instantiate(charPrefab);
                temp.SetActive(false);

                p = temp.GetComponent<Police>();
                p.Genes = new System.Collections.BitArray(9);

                for (int j = 0; j < 9; j++)
                {
                    int r = Random.Range(0, 1);
                    bool bit = r == 1;
                    p.Genes[j] = bit;
                }

                SimManager.Instance.PolicePop.Add(p);
            }
        }
	}
	
}
