using UnityEngine;
using System.Collections.Generic;

public class FallbackPoint : MonoBehaviour {

    [SerializeField]
    private bool formationPoint;

    [SerializeField]
    private GameObject[] barrierStorage;

    private List<BarrierPlacementSpot> barrierSpots = new List<BarrierPlacementSpot>();

    public List<BarrierPlacementSpot> BarrierSpots
    {
        get { return barrierSpots; }
    }

    public GameObject[] BarrierStorage
    {
        get { return barrierStorage; }
    }

    public bool FormationPoint
    {
        get { return formationPoint; }
    }

    Color c = Color.blue;
    public bool Compromised
    {
        get; private set;
    }

    void Start()
    {
        foreach(Transform t in transform)
        {
            BarrierPlacementSpot bps = t.gameObject.GetComponent<BarrierPlacementSpot>();
            if(t.gameObject.name.Contains("BarrierSpot") && bps != null)
            {
                barrierSpots.Add(bps);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rioter")
        {
            c = Color.red;
            Compromised = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = c;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
