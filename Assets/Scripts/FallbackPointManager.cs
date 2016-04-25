using UnityEngine;
using System.Collections;

public class FallbackPointManager : MonoBehaviour {

    public static FallbackPointManager Instance
    {
        get; private set;
    }

    [SerializeField]
    private FallbackPoint[] fallbackPoints;

    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public FallbackPoint FindNextUncompromised()
    {
        foreach(FallbackPoint fb in fallbackPoints)
        {
            if (fb.Compromised)
            {
                continue;
            }
            else
            {
                return fb;
            }
        }

        return null;
    }

    public FallbackPoint FindFormationPoint()
    {
        foreach (FallbackPoint fb in fallbackPoints)
        {
            if (fb.FormationPoint)
            {
                return fb;
            }
        }

        return null;
    }
}
