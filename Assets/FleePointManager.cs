using UnityEngine;
using System.Collections;

public class FleePointManager : MonoBehaviour {

    private static FleePointManager instance;
    public static FleePointManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private FleePoints leftFlee;

    [SerializeField]
    private FleePoints rightFlee;

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public Vector3 NearestFleePoint(Vector3 pos)
    {
        Vector3 left = leftFlee.FindClosest(pos);
        Vector3 right = rightFlee.FindClosest(pos);

        float ld = (left - pos).magnitude;
        float rd = (right - pos).magnitude;

        return (ld < rd) ? left : right;
    }
}
