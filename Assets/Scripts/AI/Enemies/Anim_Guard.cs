using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Guard : MonoBehaviour
{

    Animator anim;
    RangedGuard en;

    private void Start()
    {
        anim = GetComponent<Animator>();
        en = GetComponentInParent<RangedGuard>();
    }

    private void Update()
    {
        resetanim();
        if (en.State == EnemyState.Chasing)
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

    public void resetanim()
    {
        anim.SetBool("Running", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Idle", false);
    }
}
