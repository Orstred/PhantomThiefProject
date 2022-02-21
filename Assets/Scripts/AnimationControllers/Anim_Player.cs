using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Anim_Player : MonoBehaviour
{


    Animator anim;
    PlayerCharacter parent;

    
    


    private void Start()
    {
        anim = GetComponent<Animator>();
        parent = GetComponentInParent<PlayerCharacter>();
    }


    private void LateUpdate()
    {
        anim.SetFloat("speed", parent.currentSpeed);

        anim.SetBool("Idle", parent.isIdle);
        anim.SetBool("Walking", parent.isWalking);
        anim.SetBool("Crouching", parent.isCrouching);
        anim.SetBool("Grappling", !parent.motor.isActiveAndEnabled);

        if(parent.motor.isActiveAndEnabled)
        anim.SetBool("isGrounded", parent.motor.isGrounded);
        if (!parent.motor.isActiveAndEnabled)
        anim.SetBool("isGrounded", false);
    }

    public void Jump()
    {
        GetComponentInParent<PlayerMotor>().Jump(5);
    }
    public void endjump()
    {
        anim.SetBool("Jump", false);
    }


}
