using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

[System.Serializable]
public enum NPCStates { Idle, Guard, Patroll, Suspicious, Alert, Chasing, NPC_EVENT }

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    
    
    

    
    //Stats
    [HorizontalLine(2f,EColor.Gray)]
    [Header("AI OPTIONS")]
    public NPCStates State;
    public WaypointManager PatrollPath;
    public float WalkSpeed = 5, RunSpeed = 15;
    public bool BackAndForth;
    public bool isGoingForward;
    public bool allowBranching;

    //private variables
    protected NavMeshAgent agent;
    protected bool isfollowingpath;
    protected Waypoint TargetWaypoint;
    protected bool hasReachedDestination;


    

    void Start()
    {
        _Start();
    }

    void Update()
    {
        _Update();

    }

    public virtual void _Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = WalkSpeed;
    }

    public virtual void _Update()
    {
        if (State == NPCStates.Idle)
        {
            Idle();
        }
        else if (State == NPCStates.Guard)
        {
            Guard();
        }
        else if (State == NPCStates.Patroll)
        {
            Patroll();
        }
        else if (State == NPCStates.NPC_EVENT)
        {
            NPC_Event();
        }
        else if (State == NPCStates.Suspicious)
        {
            Suspicious();
        }
        else if (State == NPCStates.Alert)
        {
            Alert();
        }
        else if (State == NPCStates.Chasing)
        {
            Chasing();
        }
    }



    public virtual void Idle()
    {

    }

    public virtual void Guard()
    {

    }

    public virtual void Patroll()
    {
        hasReachedDestination = (transform.position - agent.destination).magnitude < 0.2f;

        //Checks for branching paths and Events on the current waypoint
        if (hasReachedDestination && TargetWaypoint != null)
        {
            if (TargetWaypoint.hasEvent) {State = NPCStates.NPC_EVENT; }
            else if (allowBranching && TargetWaypoint.isBranching) {BranchOf();}
            if (!BackAndForth && !PatrollPath.Loop)
            {
                if (TargetWaypoint == PatrollPath.Pathway[0] || TargetWaypoint == PatrollPath.LastWayPoint())
                {
                    agent.stoppingDistance = .4f;
                    State = NPCStates.Idle;
                    agent.SetDestination(transform.position);
                    agent.stoppingDistance = 0.1f;
                }
            }
        }

        //Moves Entity along a waypoint manager
        FollowPath();
    }




    public virtual void Suspicious()
    {

    }

    public virtual void Alert()
    {

    }

    public virtual void Chasing()
    {

    }

    public virtual void NPC_Event()
    {

    }



    public void FollowPath()
    {

        //If the agent has no destination go to the closest point
        if (TargetWaypoint == null)
        {
            TargetWaypoint = ClosestPointInPath();
            agent.SetDestination(TargetWaypoint.GetPosition());
        }

        //If the agent has reached the way point
        else if (hasReachedDestination)
        {

            //Changes target direction if is back and forward and not loop else it stops the agent
            if (BackAndForth && !PatrollPath.Loop)
            {
                if (TargetWaypoint == PatrollPath.Pathway[0] || TargetWaypoint == PatrollPath.LastWayPoint())
                {
                    isGoingForward = !isGoingForward;
                }
            }

            //Changes the target waypoint to be the next waypoint based on wether it's going forward or back on the path
            TargetWaypoint = (isGoingForward) ? TargetWaypoint.nextWaypoint : TargetWaypoint.previousWaypoint;
            if (TargetWaypoint != null)
                agent.SetDestination(TargetWaypoint.GetPosition());
        }
    }

    public void BranchOf()
    {
        //Calculates the chance of branching
        if (Random.Range(0, 100f) <= TargetWaypoint.BranchChance)
        {
            agent.SetDestination(TargetWaypoint.Entrypoint.GetPosition());
            isGoingForward = TargetWaypoint.BranchForward;
            PatrollPath = TargetWaypoint.Branch;
            TargetWaypoint = TargetWaypoint.Entrypoint;
        }
    }
    private Waypoint ClosestPointInPath()
    {
        Waypoint p = null;
        foreach(Waypoint g in PatrollPath.Pathway)
        {
            if(p == null)
            {
                p = g;
            }
            else if(Vector3.Distance(g.transform.position, transform.position) < Vector3.Distance(transform.position, p.transform.position))
            {
                p = g;
            }
        }
        return p;
    }


    
}
