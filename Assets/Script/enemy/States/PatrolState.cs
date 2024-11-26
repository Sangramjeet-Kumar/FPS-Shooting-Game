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
    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        //implement our patrol logic.
        if (enemy1.Agent.remainingDistance < 0.2f)
        {
            if (waypointIndex < enemy1.path.waypoints.Count - 1)
            {
                waypointIndex++;
            }
            else
            {
                waypointIndex = 0;
            }
            enemy1.Agent.SetDestination(enemy1.path.waypoints[waypointIndex].position);
        }
    }
}