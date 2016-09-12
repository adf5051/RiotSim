using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CivWander : RAINAction
{
    private float wanderTimer;

    private const float radius = .3f;
    private const float distAhead = 4f;
    private const float timeBetweenPoints = .5f;
    private const int areaMask = 1 << 4;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        
        if (wanderTimer > timeBetweenPoints)
        {
            NavMeshAgent navAgent = ai.Body.GetComponent<NavMeshAgent>();
            wanderTimer = 0;
            int sign = Random.value > .1f ? 1 : -1;

            float angle = Random.rotation.eulerAngles.y;
            Vector3 point = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            point = ai.Body.transform.position + (sign * ai.Body.transform.forward * distAhead) + point;

            NavMeshHit hit;
            NavMesh.SamplePosition(point, out hit, 100, areaMask);
            navAgent.SetDestination(hit.position);
        }
        else
        {
            wanderTimer += Time.deltaTime;
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}