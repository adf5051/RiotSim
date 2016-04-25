using UnityEngine;
using System.Collections;

public interface IAIGuy
{
    int fitness { get; }

    GameObject gameObject { get; }

    int Health { get; set; }

    int Strength { get; set; }

    int Speed { get; set; }

    bool TakeDamage(int damage);

}
