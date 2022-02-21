using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class Guard_Base : Enemy
{

    [HorizontalLine(2f, EColor.Gray)]
    [Header("ENEMY OPTIONS")]
    public GameObject VisionCone;
    public GameObject PeripheralVisionCone;
    public float AttentionSpan;
    public Vector3 GuardPosition;
    public LayerMask Detect;

    [HorizontalLine(2f, EColor.Gray)]
    [Header("GUARD OPTIONS")]
    public float MeleeDistance;
    public int MeleeDamage;
    public bool hasGun;
    [ShowIf("hasGun")]
    public float RangedDistance;
    [ShowIf("hasGun")]
    public int RangedDamage;

    private bool foundplayer = false;
    private float atttention;

    public override void _Start()
    {
        base._Start();
        VisionCone.AddComponent<VisionCone>();
        PeripheralVisionCone.AddComponent<PeripheralVisionCone>();
    }

    public override void _Update()
    {
        base._Update();
    }



    public override void Idle()
    {
        base.Idle();
    }
    public override void Guard()
    {
        base.Guard();
    }
    public override void Patroll()
    {
        base.Patroll();
    }


    public override void Suspicious()
    {
        base.Suspicious();
    }
    public override void Alert()
    {
        base.Alert();
    }
    public override void Chasing()
    {
        base.Chasing();
        agent.SetDestination(player.transform.position);

        if (foundplayer)
        {
            atttention = AttentionSpan;
        }
        else
        {
            atttention -= Time.deltaTime;
            if(atttention <= 0)
            {
                State = EnemyState.Patroll;
            }
        }
    }


    public override void NPC_Event()
    {
        base.NPC_Event();
    }



    public override void OnPlayerEnterVision(GameObject g)
    {
        PlayerCharacter p = g.GetComponent<PlayerCharacter>();

        //Casts 2 rays to checks if players head and chest is in view
        if (!(Physics.Linecast(VisionCone.transform.position, p.PlayerHeadDetector.position, ~Detect) && Physics.Linecast(VisionCone.transform.position, p.PlayerChestDetector.position, ~Detect)))
        {
            if (!p.inShadow)
            {
                base.OnPlayerEnterVision(g);
                player = g;
                GetComponent<AudioSource>().Play();
                State = EnemyState.Chasing;
                Debug.Log("Player " + g.name + " in sight");
                foundplayer = true;
                State = EnemyState.Chasing;
            }
        }
    }
    public override void OnPlayerStayInVision(GameObject g)
    {
        base.OnPlayerStayInVision(g);
        PlayerCharacter p = g.GetComponent<PlayerCharacter>();

        //Casts 2 rays to checks if players head and chest is in view
        if (!foundplayer)
        {
            if (!(Physics.Linecast(VisionCone.transform.position, p.PlayerHeadDetector.position, ~Detect) && Physics.Linecast(VisionCone.transform.position, p.PlayerChestDetector.position, ~Detect)))
            {
                if (!p.inShadow)
                {
                    base.OnPlayerEnterVision(g);
                    player = g;
                    GetComponent<AudioSource>().Play();
                    State = EnemyState.Chasing;
                    Debug.Log("Player " + g.name + " in sight");
                    foundplayer = true;
                    State = EnemyState.Chasing;
                }
            }
        }
      
    }
    public override void OnPlayerExitVision(GameObject g)
    {
        base.OnPlayerExitVision(g);
        foundplayer = false;
    }

    public override void OnPlayerEnterPeripheralVision(Vector3 suspos)
    {
        base.OnPlayerEnterPeripheralVision(suspos);
    }
    public override void OnPlayerStayInPeripheralVision(Vector3 suspos)
    {
        base.OnPlayerStayInPeripheralVision(suspos);
    }
    public override void OnPlayerExitPeripheralVision(Vector3 suspos)
    {
        base.OnPlayerExitPeripheralVision(suspos);
    }


    public void SuspiciousPoint(Vector3 SusPoint)
    {

    }
    public void AlertPoint(Vector3 AlerPoint)
    {

    }
}
