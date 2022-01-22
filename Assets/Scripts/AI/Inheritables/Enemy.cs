using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;


public class Enemy : NPC
{

    [HorizontalLine(2f, EColor.Gray)]
    [Header("ENEMY OPTIONS")]
    public GameObject VisionCone;
    public float AttentionSpan;
    public Vector3 GuardPosition;
    [HorizontalLine(2f, EColor.Gray)]
    [Header("COMBAT OPTIONS")]
    public float MeleeDistance = 2.5f;
    public float MeleeDamage;
    [ShowIf("hasGun")]
    public float ShootDistance = 35f;
    [ShowIf("hasGun")]
    public float BulletDamage;
    public bool hasGun;
    [HideInInspector]
    public PlayerCharacter playercharacter;



    //void start
    public override void _Start()
    {
        base._Start();
        Guard_Start();
    }


    public virtual void Guard_Start()
    {
        VisionCone.AddComponent<VisionCone>();
        playercharacter = GameManager.instance.Playercharacter.GetComponent<PlayerCharacter>();
    }









    public virtual void OnPlayerEnterView()
    {

       

    }

    public virtual void OnPlayerStayInView()
    {
        transform.LookAt(playercharacter.transform);
    }

    public virtual void OnPlayerExitView()
    {
        State = NPCStates.Alert;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MeleeDistance);
        if (hasGun)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ShootDistance);
        }
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(GuardPosition, .4f);

        if(GuardPosition == Vector3.zero)
        {
            GuardPosition = transform.position;
        }
    }

}
