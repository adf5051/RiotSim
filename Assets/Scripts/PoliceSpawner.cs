using UnityEngine;
using RAIN.Navigation.Waypoints;
using System.Collections.Generic;

public class PoliceSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab;

    [SerializeField]
    [Range(1, 5)]
    private int numPolice;

    [SerializeField]
    private Transform[] patrolPoints;

    [SerializeField]
    private WaypointRig patrol;

    [SerializeField]
    private bool left;

    [SerializeField]
    private RAIN.BehaviorTrees.BTAsset policeBehavior;

    [SerializeField]
    public RAIN.BehaviorTrees.BTNode headNode;

    // Use this for initialization
    void Start () {
        int trans;
        GameObject temp;
        RAIN.Core.AI rig;
        IList<Waypoint> waypoints = patrol.WaypointSet.Waypoints;

        for (int i = 0; i < numPolice; i++) {
            trans = Random.Range(0, waypoints.Count);

            temp = GameObject.Instantiate(charPrefab);
            rig = temp.GetComponentInChildren<RAIN.Core.AIRig>().AI;
            RAIN.Minds.BasicMind mind = rig.Mind as RAIN.Minds.BasicMind;
            mind.SetTreeForBinding("Police Tree", policeBehavior);
            //mind.BehaviorRoot = policeBehavior.;
            mind.ReloadBinding("Police Tree");
            rig.WorkingMemory.SetItem<bool>("left", left);

            temp.transform.position = waypoints[trans].Position + Vector3.up;
            temp.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
            temp.name = "Police";
        }
	}
	
}
