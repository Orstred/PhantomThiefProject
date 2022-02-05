using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

[System.Serializable]
public enum NPCStates { Idle, Guard, Patroll, Suspicious, Alert, Chasing, NPC_Event }

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    
    
    

    
    //Stats
    [HorizontalLine(2f,EColor.Gray)]
    [Header("AI OPTIONS")]
    public NPCStates State;
    public WaypointManager PatrollPath;
    public float WalkSpeed = 5, RunSpeed = 15;
    public bool BackAndForth;
    public bool isGoingForward;


    //private variables
    protected NavMeshAgent agent;
    protected bool isfollowingpath;
    protected Waypoint TargetWaypoint;
    


    

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
            FollowPath();
        }
        else if (State == NPCStates.NPC_Event)
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


    public virtual void FollowPath()
    {
        //If the agent has no destination go to the closest point
        if(TargetWaypoint == null)
        {
            TargetWaypoint = ClosestPointInPath();
            agent.SetDestination(TargetWaypoint.GetPosition()); 
        }

        //If the agent has reached the way point
        else if(Vector3.Distance(agent.destination,transform.position) < 0.2f)
        {
            //Changes the target waypoint to be the next waypoint based on wether it's going forward or back on the path
            TargetWaypoint = (isGoingForward) ? TargetWaypoint.nextWaypoint : TargetWaypoint.previousWaypoint;
            agent.SetDestination(TargetWaypoint.GetPosition());

            //Changes target direction if is back and forward and not loop
            if (BackAndForth && !PatrollPath.Loop)
            {
                if(TargetWaypoint == PatrollPath.Pathway[0] || TargetWaypoint == PatrollPath.LastWayPoint())
                {
                    isGoingForward = !isGoingForward;
                }
            }
        }
    }


    public void BranchOf()
    {
       if(Vector3.Distance(transform.position, agent.destination) < 0.1f)
        {
            if (Random.Range(0, 99.9f) < ClosestPointInPath().BranchChance)
            {
                agent.SetDestination(ClosestPointInPath().Entrypoint.GetPosition());
                PatrollPath = ClosestPointInPath().Branch;
                isGoingForward = ClosestPointInPath().BranchForward;
            }
        }

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
