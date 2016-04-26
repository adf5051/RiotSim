using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Rioter : MonoBehaviour, IAIGuy
{

    private int health = 100;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

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

    List<IAIGuy> closeEnemies;
    public List<IAIGuy> CloseEnemies
    {
        get
        {
            if (closeEnemies == null)
            {
                closeEnemies = new List<IAIGuy>();
            }

            return closeEnemies;
        }
        set { closeEnemies = value; }
    }

    public int fitness
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public BitArray Genes { get; set; }

    int IAIGuy.fitness
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    // Use this for initialization
    void Start()
    {
        CloseEnemies = new List<IAIGuy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        Rioter rioter = other.GetComponent<Rioter>();
        if (rioter != null)
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

    public void RemoveDeadEnemy(IAIGuy enemy)
    {
        if (closeEnemies.Contains(enemy))
        {
            closeEnemies.Remove(enemy);
        }
    }

    public void TranslateGenesToInts()
    {
        // For the rioter, speed is not relevant when rioting, only when chasing down a cop;

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

        speed = 1;

        if (Genes[3])
            speed += 1;

        if (Genes[4])
            speed += 2;

        if (Genes[5])
            speed += 2;

        strength = 1;

        if (Genes[6])
            strength += 1;

        if (Genes[7])
            strength += 2;

        if (Genes[8])
            strength += 2;

    }
}
