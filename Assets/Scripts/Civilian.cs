using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Civilian : MonoBehaviour, IAIGuy {

    private NavMeshAgent navAgent;
    private int health = 50;

    private const int areaMask = 1 << 4;

    public BitArray Genes
    {
        get; set;
    }

    public bool Dead
    {
        get; set;
    }

    public int fitness
    {
        get; set;
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int Strength
    {
        get; set;
    }

    public int Speed
    {
        get; set;
    }

    public void SimStateChange(State state)
    {
        RAIN.Core.AIRig ai = GetComponentInChildren<RAIN.Core.AIRig>();
        ai.AI.WorkingMemory.SetItem<State>("SimState", state);
        NavMeshAgent navAgent = gameObject.GetComponent<NavMeshAgent>();
        ai.AI.Mind.AIInit();

        if (gameObject.activeInHierarchy)
        {
            switch (state)
            {
                case State.running:
                    navAgent.Resume();
                    break;
                case State.stopped:
                    navAgent.Stop();
                    navAgent.ResetPath();
                    break;
            }
        }
    }

    // Use this for initialization
    void Start () {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.areaMask = areaMask;
        
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rioter" && !other.isTrigger)
        {
            RAIN.Core.AIRig ai = GetComponentInChildren<RAIN.Core.AIRig>();
            ai.AI.WorkingMemory.SetItem<bool>("riotSpotted", true);
        }
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveDeadEnemy(IAIGuy enemy)
    {
        throw new NotImplementedException();
    }

    public void TranslateGenesToInts()
    {
        throw new NotImplementedException();
    }

    public void Initialize()
    {
        health = 50;
    }

    public void CalculateFitness()
    {
        throw new NotImplementedException();
    }
}
