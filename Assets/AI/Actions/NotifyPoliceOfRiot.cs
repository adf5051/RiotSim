using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class NotifyPoliceOfRiot : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (ai.WorkingMemory.GetItem<bool>("RiotSpotted"))
        {
            Police p = ai.Body.GetComponent<Police>();
            if (!p.KnowAboutRiot)
            {
                p.CommunicateRiotSpotted();
            }
        }

        return ActionResult.FAILURE;
    }


    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}