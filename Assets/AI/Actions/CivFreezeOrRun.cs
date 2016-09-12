using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class CivFreezeOrRun : RAINDecision
{
    bool runAway = false;
    bool init = false;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.RUNNING;

        if (!init)
        {
            init = true;
            float rand = Random.value;

            if(rand < .5)
            {
                runAway = true;
            }
        }

        if (runAway)
        {
            tResult = _children[0].Run(ai);
        }
       
        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}