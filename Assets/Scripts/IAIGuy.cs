using UnityEngine;
using System.Collections;

public interface IAIGuy
{
    BitArray Genes { get; set; }

    int fitness { get; set; }

    GameObject gameObject { get; }

    int Health { get; set; }

    int Strength { get; set; }

    int Speed { get; set; }

    bool TakeDamage(int damage);

    void RemoveDeadEnemy(IAIGuy enemy);

    void TranslateGenesToInts();
}
