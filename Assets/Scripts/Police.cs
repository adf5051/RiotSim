using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

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

    private Observation statsBeforeFight;

    bool fighting;
    public bool Fighting
    {
        get { return fighting; }
        set
        {
            if(value && fighting != value)
            {
                statsBeforeFight.carryingBarrier = Barrier != null;
                statsBeforeFight.health = health;
                statsBeforeFight.strength = strength;
            }
            else
            {
                statsBeforeFight = default(Observation);
            }

            fighting = value;
        }
    }

    public int PlacedBarriers { get; set; }

    public delegate void Communicate(CommunicationType type, object data);
    public static event Communicate communicate;
    private static List<Police> allPolice = new List<Police>();
    public static List<Police> AllPolice
    {
        get { return allPolice; }
        private set { allPolice = value; }
    }

    public List<Rioter> SpottedRioters
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

    public int fitness { get; set; }

    public BitArray Genes { get; set; }

    public bool Dead { get; set; }

    void Awake()
    {
        AllPolice.Add(this);
        communicate += HandleCommunication;
    }

    public void Initialize()
    {
        SpottedRioters = new List<Rioter>();
        PlacedBarriers = 0;
        fighting = false;
        statsBeforeFight = default(Observation);
        TranslateGenesToInts();
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //allPolice.Remove(this);
            Dead = true;

            if (Fighting &&
                !EqualityComparer<Observation>.Default.Equals(statsBeforeFight, default(Observation)))
            {
                SimManager.Instance.policeBayes.AddObs(statsBeforeFight.health, statsBeforeFight.strength, statsBeforeFight.carryingBarrier, false);
                fighting = false;
                statsBeforeFight = default(Observation);
            }

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
        if (other.isTrigger)
        {
            return;
        }

        if (other.tag == "Rioter" && !SpottedRioters.Contains(other.GetComponent<Rioter>()))
        {
            SpottedRioters.Add(other.GetComponent<Rioter>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.tag == "Rioter" && SpottedRioters.Contains(other.GetComponent<Rioter>()))
        {
            SpottedRioters.Remove(other.GetComponent<Rioter>());
        }
    }

    public void RemoveDeadEnemy(IAIGuy enemy)
    {
        if (enemy.GetType() == typeof(Rioter) && SpottedRioters.Contains(enemy as Rioter))
        {
            SpottedRioters.Remove(enemy as Rioter);
            if (Fighting)
            {
                if (!EqualityComparer<Observation>.Default.Equals(statsBeforeFight, default(Observation)))
                {
                    SimManager.Instance.policeBayes.AddObs(statsBeforeFight.health, statsBeforeFight.strength, statsBeforeFight.carryingBarrier, true);
                }

                Fighting = false;
                statsBeforeFight = default(Observation);
            }
        }
    }

    public void CalculateFitness()
    {
        fitness = 0;

        if (!Dead)
        {
            fitness += 10;
        }

        fitness += (10 * PlacedBarriers);
        fitness += health;
    }

    public void TranslateGenesToInts()
    {
        // hhh SpSpSp StStSt
        // H 5-15-30-50
        // Sp 1-1-2-2
        // St 1-1-2-2

        Health = 5;

        if (Genes[0])
            Health += 15;

        if (Genes[1])
            Health += 30;

        if (Genes[2])
            Health += 50;

        speed = 2;

        if (Genes[3])
            speed += 2;

        if (Genes[4])
            speed += 3;

        if (Genes[5])
            speed += 3;

        strength = 2;

        if (Genes[6])
            strength += 2;

        if (Genes[7])
            strength += 3;

        if (Genes[8])
            strength += 3;

    }
}
