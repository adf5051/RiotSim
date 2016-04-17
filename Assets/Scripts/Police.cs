using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Police : MonoBehaviour
{
    public enum CommunicationType
    {
        riotSptted,
        claimPlacement,
    }

    public delegate void Communicate(CommunicationType type, object data);
    public static event Communicate communicate;
    private static List<Police> allPolice = new List<Police>();
    public static List<Police> AllPolice
    {
        get { return allPolice; }
        private set { allPolice = value; }
    }

    public List<Collider> SpottedRioters
    {
        get; private set;
    }

    public bool KnowAboutRiot { get; set; }
    public float SightDist { get; set; }
    public float HorizontalFOV { get; set; }

    [SerializeField]
    private float speed = 5;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Awake()
    {
        SpottedRioters = new List<Collider>();
        AllPolice.Add(this);
        communicate += HandleCommunication;
    }

    private void HandleCommunication(CommunicationType type, object data)
    {
        if (type == CommunicationType.riotSptted && !KnowAboutRiot)
        {
            KnowAboutRiot = true;
            GetComponentInChildren<RAIN.Core.AIRig>().AI.WorkingMemory.SetItem<bool>("RiotSpotted", true);
            GetComponent<NavMeshAgent>().Stop();
            Debug.Log("Riot Spotted Recieved");
        }
    }

    public void CommunicateRiotSpotted()
    {
        if (communicate != null)
        {
            KnowAboutRiot = true;
            GetComponent<NavMeshAgent>().Stop();
            communicate(CommunicationType.riotSptted, null);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rioter" && !SpottedRioters.Contains(other))
        {
            SpottedRioters.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rioter" && SpottedRioters.Contains(other))
        {
            SpottedRioters.Remove(other);
        }
    }
}
