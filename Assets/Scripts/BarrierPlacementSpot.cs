using UnityEngine;
using System.Collections;

public class BarrierPlacementSpot : MonoBehaviour
{
    public bool Claimed
    {
        get; set;
    }

    public GameObject Barrier
    {
        get; private set;
    }

    public void Clear()
    {
        Barrier = null;
        Claimed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Police" && !other.isTrigger)
        {
            if (other.GetComponent<Police>().Barrier != null && Claimed)
            {
                GameObject barrier = other.GetComponent<Police>().Barrier;
                other.GetComponent<Police>().Barrier = null;
                other.GetComponent<Police>().PlacedBarriers++;
                Barrier = barrier;
                barrier.SetActive(true);
                barrier.transform.position = transform.position;
                barrier.transform.forward = transform.forward;
            }
        }

    }
}
