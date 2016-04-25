using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Police : MonoBehaviour, IAIGuy
{
    private int health = 100;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

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
    private int strength = 5;
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    [SerializeField]
    private int speed = 5;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public GameObject Barrier
    {
        get; set;
    }

    public int fitness
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    void Awake()
    {
        SpottedRioters = new List<Collider>();
        AllPolice.Add(this);
        communicate += HandleCommunication;
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
