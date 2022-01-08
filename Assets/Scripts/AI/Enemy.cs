using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
[System.Serializable]
public enum StateMachine { Patrolling, Chasing, Guarding}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{


    #region Instantiations
    NavMeshAgent agent;
    Transform playercharacter;
    [HideInInspector]
    public AudioSource Voice;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playercharacter = GameManager.instance.playercharacter;
        Voice = GetComponent<AudioSource>();
    }

    #endregion

    #region Movement Stats
    [Foldout("Movement Stats")]
    public float WalkSpeed, RunSpeed;
    [Foldout("Movement Stats")]
    public float TurnSpeed;
    #endregion

    #region AI Stat
    [Foldout("AI options")]
    public GameObject VisonCone;
    [Foldout("AI options")]
    public WaypointManager PatrollPath;
    [Foldout("AI options")]
    public StateMachine State;
    #endregion

    #region Local Variables
    int currentdestination;
    #endregion


    private void Update()
    {


       






        if (State == StateMachine.Chasing)
        {
            ChasePlayer();
        }
        if(State == StateMachine.Patrolling)
        {
            
            FollowPath();
        }
    }
 
    public void ChasePlayer()
    {
        agent.destination = playercharacter.position;
    }

    public void FollowPath()
    {
        if (PatrollPath.Loop)
        {
            if (Vector3.Distance(transform.position, agent.destination) < .1f || agent.destination == null)
            {
                
                    if (PatrollPath.Pathway[currentdestination] == PatrollPath.Pathway[0])
                        currentdestination = 0;
                    agent.SetDestination(PatrollPath.Pathway[currentdestination].GetPosition());
                    currentdestination++;
                
            }
        }
        else 
        {
            if (Vector3.Distance(transform.position, agent.destination) < .1f || agent.destination == null)
            {
                if (currentdestination < PatrollPath.Pathway.Count)
                {
                    agent.SetDestination(PatrollPath.Pathway[currentdestination].GetPosition());
                    currentdestination++;
                }

            }
        }

    }   




  
}
