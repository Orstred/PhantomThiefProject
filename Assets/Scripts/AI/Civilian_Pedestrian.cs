using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CivilianStates {Idle = 0, Patroll =1 , NPC_EVENT = 2}
public class Civilian_Pedestrian : MonoBehaviour
{

    public CivilianStates State = CivilianStates.Patroll;
    public WaypointManager PatrollPath;
    public float WalkSpeed = 5;
    public bool isGoingForward;
    public bool allowBranching = true;
    public bool BackAndForth;
    


    private Vector3 destination;
    private Waypoint TargetWaypoint;
    private bool hasReachedDestination;


    private void Update()
    {
        if (State == CivilianStates.Idle)
        {

        }
        else if (State == CivilianStates.Patroll)
        {
            //Checks for branching paths and Events on the current waypoint
            if ( TargetWaypoint != null &&hasReachedDestination)
            {
                if (TargetWaypoint.hasEvent) { State = CivilianStates.NPC_EVENT; }
                else if (allowBranching && TargetWaypoint.isBranching) { BranchOf(); }
                if(!BackAndForth && !PatrollPath.Loop)
                {
                    if(TargetWaypoint == PatrollPath.Pathway[0] || TargetWaypoint == PatrollPath.LastWayPoint())
                    {
                        destination = transform.position;
                        State = CivilianStates.Idle;
                    }
                }
            }

            //Moves Entity along a waypoint manager
            FollowPath();

            //Lerps the player to destination
            float dist = (transform.position - destination).magnitude;
            transform.position = Vector3.Lerp(transform.position, destination, (Time.deltaTime * WalkSpeed) / dist);
            hasReachedDestination = dist < 0.2f;

            //Rotates the player to look at destination
            Vector3 direction = destination - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 4);
        }
        else if (State == CivilianStates.NPC_EVENT)
        { 
        
        }
    }


    public virtual void FollowPath()
    {
        //If the agent has no destination go to the closest point
        if (TargetWaypoint == null)
        {
            TargetWaypoint = ClosestPointInPath();
            destination = TargetWaypoint.GetPosition();
        }

        //If the agent has reached the way point
        else if (hasReachedDestination)
        {
            //Changes target direction if is back and forward and not loop
            if (BackAndForth && !PatrollPath.Loop)
            {
                if (TargetWaypoint == PatrollPath.Pathway[0] || TargetWaypoint == PatrollPath.LastWayPoint())
                {
                    isGoingForward = !isGoingForward;
                }
            }


            //Changes the target waypoint to be the next waypoint based on wether it's going forward or back on the path
            TargetWaypoint = (isGoingForward) ? TargetWaypoint.nextWaypoint : TargetWaypoint.previousWaypoint;
            if(TargetWaypoint != null)
            destination = TargetWaypoint.GetPosition();
        }
    }

    public void BranchOf()
    { 
            //Calculates the chance of branching
            if (Random.Range(0, 100f) <= TargetWaypoint.BranchChance)
            {
                destination = TargetWaypoint.Entrypoint.GetPosition();
                isGoingForward = TargetWaypoint.BranchForward;
                PatrollPath = TargetWaypoint.Branch;
                TargetWaypoint = TargetWaypoint.Entrypoint;
            }
    }

    private Waypoint ClosestPointInPath()
    {
        Waypoint p = null;
        foreach (Waypoint g in PatrollPath.Pathway)
        {
            if (p == null)
            {
                p = g;
            }
            else if (Vector3.Distance(g.transform.position, transform.position) < Vector3.Distance(transform.position, p.transform.position))
            {
                p = g;
            }
        }
        return p;
    }
}
