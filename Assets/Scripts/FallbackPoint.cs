using UnityEngine;
using System.Collections.Generic;

public class FallbackPoint : MonoBehaviour
{

    [SerializeField]
    private bool formationPoint = false;

    [SerializeField]
    private GameObject[] barrierStorage = null;

    private List<BarrierPlacementSpot> barrierSpots = new List<BarrierPlacementSpot>();

    public List<BarrierPlacementSpot> BarrierSpots
    {
        get { return barrierSpots; }
    }

    public GameObject[] BarrierStorage
    {
        get { return barrierStorage; }
    }

    private List<GameObject> formationPoints = new List<GameObject>();
    private List<GameObject> freeFormationPoints = new List<GameObject>();

    public List<GameObject> FreeFormationPoints
    {
        get { return freeFormationPoints; }
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
        foreach (Transform t in transform)
        {
            BarrierPlacementSpot bps = t.gameObject.GetComponent<BarrierPlacementSpot>();
            if (t.gameObject.name.Contains("BarrierSpot") && bps != null)
            {
                barrierSpots.Add(bps);

            }
            else if (t.gameObject.name.Contains("FormationPoint"))
            {
                formationPoints.Add(t.gameObject);
            }
        }

        if (formationPoints.Count > 0)
        {
            freeFormationPoints = formationPoints;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rioter")
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

    public GameObject FindClosestFormationPoint(Vector3 position)
    {
        float dist = float.MaxValue;
        GameObject closest = null;
        Vector3 mag;

        foreach (GameObject go in freeFormationPoints)
        {
            mag = position - go.transform.position;
            if (mag.magnitude < dist)
            {
                dist = mag.magnitude;
                closest = go;
            }
        }

        if (closest)
        {
            freeFormationPoints.Remove(closest);
        }

        return closest;
    }

    public void Clear()
    {
        freeFormationPoints = formationPoints;

        foreach(BarrierPlacementSpot bps in barrierSpots)
        {
            bps.Clear();
        }
    }
}
