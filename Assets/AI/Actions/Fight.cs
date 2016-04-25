using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Fight : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        IAIGuy guy = ai.Body.GetComponent<IAIGuy>();
        IAIGuy otherGuy = ai.WorkingMemory.GetItem<IAIGuy>("Enemy");

        if(otherGuy != null && otherGuy.gameObject)
        {
            bool dead = otherGuy.TakeDamage(guy.Strength);
            if (dead)
            {
                ai.WorkingMemory.RemoveItem("Enemy");
                return ActionResult.SUCCESS;
            }
            return ActionResult.RUNNING;
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}