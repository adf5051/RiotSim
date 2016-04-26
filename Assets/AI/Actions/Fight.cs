using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Fight : RAINAction
{
    float timer;

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
            timer += Time.deltaTime;

            if (timer > 1)
            {
                timer = 0;
                bool dead = otherGuy.TakeDamage(guy.Strength);
                if (dead)
                {
                    guy.RemoveDeadEnemy(otherGuy);
                    GameObject.Destroy(otherGuy.gameObject);
                    ai.WorkingMemory.SetItem<IAIGuy>("Enemy", null);
                    return ActionResult.SUCCESS;
                }
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