using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class DetectRioters : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (ai.WorkingMemory.GetItem<bool>("RiotSpotted"))
        {
            return ActionResult.FAILURE;
        }

        bool riotFound = false;

        Police p = ai.Body.transform.GetComponent<Police>();
        if(p.SpottedRioters.Count > 0)
        {
            riotFound = DetectRioter(ai, p.SpottedRioters);
        }     

        return (riotFound) ? ActionResult.FAILURE : ActionResult.SUCCESS;
    }

    private bool DetectRioter(AI ai, List<Collider> rioters, float hfov = 90)
    {

        Vector3 rpos;
        Vector3 ptor;
        Vector3 cnoy;
        Vector3 pnoy = ai.Body.transform.forward;
        pnoy.y = 0;
        pnoy.Normalize();

        float fdot;
        float hangle;

        foreach(Collider c in rioters)
        {
            rpos = c.transform.position;
            ptor = rpos - ai.Body.transform.position;
            fdot = Vector3.Dot(ptor, ai.Body.transform.forward);

            if(fdot < 0)
            {
                continue;
            }

            cnoy = ptor;
            cnoy.y = 0;
            cnoy.Normalize();

            hangle = Vector3.Angle(pnoy, cnoy);
            if(hangle > hfov || hangle < -hfov)
            {
                continue;
            }

            ai.WorkingMemory.SetItem<bool>("RiotSpotted", true);
            return true;
            
        }

        return false;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}