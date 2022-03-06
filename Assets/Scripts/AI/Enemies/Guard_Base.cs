using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Guard_Base : MonoBehaviour, IEnemy, IDamage
{




    [HorizontalLine(2f, EColor.Gray)]
    [Header("AI OPTIONS")]
    public EnemyState State;
    public WaypointManager PatrollPath;
    public float WalkSpeed = 5, RunSpeed = 15;
    public bool BackAndForth;
    public bool isGoingForward;
    public bool allowBranching;



    [HorizontalLine(2f, EColor.Gray)]
    [Header("ENEMY OPTIONS")]
    public GameObject VisionCone;
    public GameObject PeripheralVisionCone;
    public float AttentionSpan;
    public Vector3 GuardPosition;
    public LayerMask RaycastIgnore;
    public int Health = 1;


    
    protected EnemyState startingstate;
    protected bool foundplayercharacter = false;
    protected float AtttentionSpan_counter;
    [HideInInspector] public NavMeshAgent agent;
    protected bool isfollowingpath;
    protected Waypoint targetwaypoint;
    protected bool hasreacheddestination;
    protected GameObject playercharacter;






    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingstate = State;
        VisionCone.AddComponent<VisionCone>();
        PeripheralVisionCone.AddComponent<PeripheralVisionCone>();
        playercharacter = GameManager.instance.playerCharacter.gameObject;
    }

    public void Update()
    {
        #region StateBehaviours
        if (State == EnemyState.Idle)
        {
            Idle();
        }
        else if (State == EnemyState.Guard)
        {
            Guard();
        }
        else if (State == EnemyState.Patroll)
        {
            Patroll();
        }
        else if (State == EnemyState.NPC_EVENT)
        {
            NPC_Event();
        }
        else if (State == EnemyState.Suspicious)
        {
            Suspicious();
        }
        else if (State == EnemyState.Alert)
        {
            Alert();
        }
        else if (State == EnemyState.Chasing)
        {
            Chasing();
        }
        #endregion
    }





    #region IEnemy
    public virtual void Idle()
    {
        agent.speed = 0;
    }

    public virtual void Guard()
    {
        
    }

    public virtual void Patroll()
    {
        agent.speed = WalkSpeed;
        hasreacheddestination = (transform.position - agent.destination).magnitude < 0.2f;

        //Checks for branching paths and Events on the current waypoint
        if (hasreacheddestination && targetwaypoint != null)
        {
            if (targetwaypoint.hasEvent) { State = EnemyState.NPC_EVENT; }
            else if (allowBranching && targetwaypoint.isBranching) { BranchOf(); }
            if (!BackAndForth && !PatrollPath.Loop)
            {
                if (targetwaypoint == PatrollPath.Pathway[0] || targetwaypoint == PatrollPath.LastWayPoint())
                {
                    agent.stoppingDistance = .4f;
                    State = EnemyState.Idle;
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
        agent.speed = RunSpeed;
        agent.SetDestination(playercharacter.transform.position);
        if (foundplayercharacter)
        {
            AtttentionSpan_counter = AttentionSpan;
        }
        else
        {
            AtttentionSpan_counter -= Time.deltaTime;
            if(AtttentionSpan_counter <= 0)
            {
                State = startingstate;
            }
        }
    }

    public virtual void NPC_Event()
    {
       
    }



    public void OnPlayerEnterVision(GameObject g)
    {
        playercharacter = g;
        GetComponent<AudioSource>().volume = GameManager.instance.musicVolume;
        GetComponent<AudioSource>().Play();
        State = EnemyState.Chasing;
        Debug.Log("Player " + g.name + " in sight");
        foundplayercharacter = true;
    }
    public void OnPlayerStayInVision(GameObject g) 
    {
        if (!foundplayercharacter)
        { 
         playercharacter = g;
         if (!GetComponent<AudioSource>().isPlaying)
         GetComponent<AudioSource>().Play();
         State = EnemyState.Chasing;
         Debug.Log("Player " + g.name + " in sight");
         foundplayercharacter = true;
         State = EnemyState.Chasing;
        } 
    }
    public void OnPlayerExitVision(GameObject g)
    {
        foundplayercharacter = false;
    }

    public void OnPlayerEnterPeripheralVision(Vector3 suspos)
    {
        
    }
    public void OnPlayerStayInPeripheralVision(Vector3 suspos)
    {
        
    }
    public void OnPlayerExitPeripheralVision(Vector3 suspos)
    {
        
    }

    public void SuspiciousPoint(Vector3 point) { }
    public void AlertPoint(Vector3 AlerPoint) { }
    #endregion
   
    #region Idamage
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
    }
    public void DealDamage(GameObject Character, int Damage)
    {
        Character.GetComponent<IDamage>().TakeDamage(Damage);
    }
    #endregion



    //Waypoint manager following logic
    protected Waypoint GetClosestPointInCurrentPath()
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
    public void FollowPath()
    {
        //If the agent has no destination go to the closest point
        if (targetwaypoint == null)
        {
            targetwaypoint = GetClosestPointInCurrentPath();
            agent.SetDestination(targetwaypoint.GetPosition());
        }

        //If the agent has reached the way point
        else if (hasreacheddestination)
        {

            //Changes target direction if is back and forward and not loop else it stops the agent
            if (BackAndForth && !PatrollPath.Loop)
            {
                if (targetwaypoint == PatrollPath.Pathway[0] || targetwaypoint == PatrollPath.LastWayPoint())
                {
                    isGoingForward = !isGoingForward;
                }
            }

            //Changes the target waypoint to be the next waypoint based on wether it's going forward or back on the path
            targetwaypoint = (isGoingForward) ? targetwaypoint.nextWaypoint : targetwaypoint.previousWaypoint;
            if (targetwaypoint != null)
                agent.SetDestination(targetwaypoint.GetPosition());
        }
    }
    public void BranchOf()
    {
        //Calculates the chance of branching
        if (Random.Range(0, 100f) <= targetwaypoint.BranchChance)
        {
            agent.SetDestination(targetwaypoint.Entrypoint.GetPosition());
            isGoingForward = targetwaypoint.BranchForward;
            PatrollPath = targetwaypoint.Branch;
            targetwaypoint = targetwaypoint.Entrypoint;
        }
    }
}
