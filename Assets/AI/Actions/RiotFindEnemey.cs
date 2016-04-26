using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class RiotFindEnemey : RAINDecision
{

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Rioter r = ai.Body.GetComponent<Rioter>();
        IAIGuy otherGuy = ai.WorkingMemory.GetItem<IAIGuy>("Enemy");

        if (r.CloseEnemies.Count <= 0)
        {
            return ActionResult.SUCCESS;
        }

        if (r.CloseEnemies.Count > 0 && otherGuy == null)
        {

            float dist = float.MaxValue;
            IAIGuy closest = null;
            Vector3 mag;

            for (int i = 0; i < r.CloseEnemies.Count; i++)
            {
                IAIGuy guy = r.CloseEnemies[i];

                if(guy.Health <= 0 || guy.gameObject == null)
                {
                    i--;
                    r.RemoveDeadEnemy(guy);
                    continue;
                }

                mag = guy.gameObject.transform.position - r.gameObject.transform.position;

                if (mag.magnitude < dist)
                {
                    dist = mag.magnitude;
                    closest = guy;
                }
            }

            ai.WorkingMemory.SetItem<IAIGuy>("Enemy", closest);
            otherGuy = closest;
        }

        ActionResult move = _children[0].Run(ai);
        if (move == ActionResult.RUNNING)
        {
            return move;
        }
        else if (move == ActionResult.SUCCESS)
        {
            return _children[1].Run(ai);
        }

        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}