using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Guard_AI : Enemy
{


    //private instances
    AudioSource voice;


    float TimerOne = 0;


    public override void Guard_Start()
    {
        base.Guard_Start();
        voice = GetComponent<AudioSource>();
    }






    public override void Idle()
    {
        base.Idle();
        agent.SetDestination(transform.position);
    }
    public override void Guard()
    {
        base.Guard();
        agent.SetDestination(GuardPosition);
        if (Vector3.Distance(agent.destination, transform.position) <= .1f)
            State = NPCStates.Idle;
    }
    public override void Patroll()
    {
        base.Patroll();
    }
    public override void NPC_Event()
    {
        base.NPC_Event();
    }
    public override void Suspicious()
    {
        base.Suspicious();
    }
    public override void Alert()
    {
        base.Alert();
        if(TimerOne > 0)
        {
            TimerOne -= Time.deltaTime;
            agent.destination = playercharacter.transform.position;
        }
        else 
        {
            agent.destination = transform.position;

           
            Patroll();
        }
       
    }
    public override void Chasing()
    {
        agent.destination = playercharacter.transform.position;
    }




    public override void OnPlayerEnterView()
    {
        State = NPCStates.Chasing;
        if (!voice.isPlaying && TimerOne <=0)
            voice.Play();
    }
    public override void OnPlayerStayInView()
    {
        base.OnPlayerStayInView();
    }
    public override void OnPlayerExitView()
    {
        base.OnPlayerExitView();
        TimerOne = AttentionSpan;
        State = NPCStates.Alert;
    }



}
