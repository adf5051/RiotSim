using UnityEngine;
using System.Collections;

public class FallbackPoint : MonoBehaviour {

    [SerializeField]
    private bool formationPoint;

    [SerializeField]
    private GameObject[] barrierStorage;

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
