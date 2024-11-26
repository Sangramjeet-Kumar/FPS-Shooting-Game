using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    //track which waypoint we are currently targeting.
    public int waypointIndex;

    public override void Enter()
    {

    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy1.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        //implement our patrol logic.
        if (enemy1.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 1)
            {
                if (waypointIndex < enemy1.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;

                enemy1.Agent.SetDestination(enemy1.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }
}