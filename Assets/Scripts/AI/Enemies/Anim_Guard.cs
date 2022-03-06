using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Guard : MonoBehaviour
{

    Animator anim;
    Guard_Base en;
    public Transform R_handpos;
    public Transform L_handpos;
    public Transform HandsParent;



    private void Start()
    {
        anim = GetComponent<Animator>();
        en = GetComponentInParent<Guard_Base>();
    }

    public void Update()
    {
        resetanim();
        if (en.State == EnemyState.Chasing && en.agent.speed != 0)
        {
            anim.SetBool("Running", true);
        }
        else if (en.State == EnemyState.Patroll)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
           anim.SetBool("Idle", true);
        }
    }



    public void OnAnimatorIK(int layerIndex)
    {
        //Inverse kinematics
        if (R_handpos != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, L_handpos.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            anim.SetIKPosition(AvatarIKGoal.RightHand, R_handpos.position);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

            anim.SetIKRotation(AvatarIKGoal.LeftHand, L_handpos.rotation);
            anim.SetIKRotation(AvatarIKGoal.RightHand, R_handpos.rotation);
        }
    }

    public void resetanim()
    {
        anim.SetBool("Running", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Idle", false);
    }
}
