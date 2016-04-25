using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Rioter : MonoBehaviour, IAIGuy
{

    private int health = 100;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    private NavMeshAgent navAgent;

    private Transform goal;

    public Transform EndGoal
    {
        get { return goal; }
        set { goal = value; }
    }

    [SerializeField]
    private int strength = 5;

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    [SerializeField]
    private int speed = 1;
    public int Speed
    {
        get { return speed; }

        set { speed = value; }
    }

    public List<IAIGuy> CloseEnemies
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

    // Use this for initialization
    void Start()
    {
        CloseEnemies = new List<IAIGuy>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.areaMask = (1 << 3) | (1 << 4);
        navAgent.SetDestination(goal.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        IAIGuy aiguy = other.GetComponent<IAIGuy>();

        if (aiguy != null)
        {
            CloseEnemies.Add(aiguy);   
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        IAIGuy aiguy = other.GetComponent<IAIGuy>();

        if (aiguy != null && CloseEnemies.Contains(aiguy))
        {
            CloseEnemies.Remove(aiguy);
        }
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
