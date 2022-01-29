using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

[System.Serializable]
public enum NPCStates {Idle, Guard, Patroll, Suspicious, Alert, Chasing, NPC_Event}

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

    public virtual void Patroll()
    {
        //Checks to see if there are branches attached to a waypoint and changes current path based on odds
        if(Vector3.Distance(transform.position, agent.destination) < 0.1f)
        {
            if (ClosestPointInPath().Branching)
            {
                if (Random.Range(0f, 0.99f) < ClosestPointInPath().BranchChance)
                {
                    BranchOff(ClosestPointInPath().Branch[Random.Range(0,ClosestPointInPath().Branch.Count -1)]);
                }
            }
        }

        //NPC loops around pathway until told otherwise
        if (PatrollPath.Loop)
        {
            //Moves the player forward on the array
            if (isGoingForward)
            {
                //Sets the first point to be the closest to player
                if (!isfollowingpath)
                {
                    agent.destination = ClosestPointInPath().GetPosition();
                    isfollowingpath = true;
                }

                else if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                {
                    if (ClosestPointInPath().nextWaypoint != null)
                        agent.destination = ClosestPointInPath().nextWaypoint.GetPosition();
                }
            }

            //Moves the player backwards on the array
            else if(!isGoingForward)
            {
                //Sets the first point to be the closest to player
                if (!isfollowingpath)
                {
                    agent.destination = ClosestPointInPath().GetPosition();
                    isfollowingpath = true;
                }

                else if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                {
                    if (ClosestPointInPath().previousWaypoint != null)
                        agent.destination = ClosestPointInPath().previousWaypoint.GetPosition();
                }
            }

        }

        //Switches NPC direction when last position is reached
        else if (BackAndForth)
        {
            //Moves the player forward on the array
            if (isGoingForward)
            {
                    if (ClosestPointInPath() != PatrollPath.Pathway[PatrollPath.Pathway.Count - 1])
                    {
                        //Sets the first point to be the closest to player
                        if (!isfollowingpath)
                        {
                            agent.destination = ClosestPointInPath().GetPosition();
                            isfollowingpath = true;
                        }

                        else if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                        {
                            if (ClosestPointInPath().nextWaypoint != null)
                                agent.destination = ClosestPointInPath().nextWaypoint.GetPosition();
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                            isGoingForward = false;
                    }
            }

            //Moves the player backwards on the array
            else if(!isGoingForward)
            {
                if (ClosestPointInPath() != PatrollPath.Pathway[0])
                {
                    //Sets the first point to be the closest to player
                    if (!isfollowingpath)
                    {
                        agent.destination = ClosestPointInPath().GetPosition();
                        isfollowingpath = true;
                    }

                    else if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                    {
                        if (ClosestPointInPath().previousWaypoint != null)
                            agent.destination = ClosestPointInPath().previousWaypoint.GetPosition();
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                        isGoingForward = true;
                }
            }
         
        }

        //Stops NPC at last point in pathway
        else
        {

            //Moves the player forward on the array
            if (isGoingForward)
            {
                if (ClosestPointInPath() != PatrollPath.Pathway[PatrollPath.Pathway.Count - 1])
                {
                    //Sets the first point to be the closest to player
                    if (!isfollowingpath)
                    {
                        agent.destination = ClosestPointInPath().GetPosition();
                        isfollowingpath = true;
                    }

                    else if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                    {
                        if (ClosestPointInPath().nextWaypoint != null)
                            agent.destination = ClosestPointInPath().nextWaypoint.GetPosition();
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                        agent.destination = transform.position;
                }
            }

            //Moves the player backwards on the array
            else if (!isGoingForward)
            {
                if (ClosestPointInPath() != PatrollPath.Pathway[0])
                {
                    //Sets the first point to be the closest to player
                    if (!isfollowingpath)
                    {
                        agent.destination = ClosestPointInPath().GetPosition();
                        isfollowingpath = true;
                    }

                    else if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                    {
                        if (ClosestPointInPath().previousWaypoint != null)
                            agent.destination = ClosestPointInPath().previousWaypoint.GetPosition();
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
                        agent.destination = transform.position;
                }
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


    public void BranchOff(WaypointManager branch)
    {
        agent.destination = branch.Pathway[ClosestPointInPath().EntryPoint].GetPosition();
        isGoingForward = ClosestPointInPath().BranchIn;
        PatrollPath = branch;  
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
