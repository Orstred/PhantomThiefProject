using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Logic_Anim_Pedestrian : MonoBehaviour
{

    Animator anim;
    Civilian_Pedestrian parent;


    private void Start()
    {
        anim = GetComponent<Animator>();
        parent = GetComponentInParent<Civilian_Pedestrian>();
    }

    private void LateUpdate()
    {
        ResetAnima();
        switch ((int)parent.State)
        {
            case (0):
                anim.SetBool("Idle", true);
                break;
            case(1):
                anim.SetBool("Walking", true);
                break;
            case (2):
                anim.SetBool("Idle", true);
                break;
        }
          
    }

    void ResetAnima()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Walking", false);
    }

}
