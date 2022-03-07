using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class RangedGuard : Guard_Base
{


    [HorizontalLine(2f, EColor.Gray)]
    [Header("RANGED OPTIONS")]
    public float RangeDistance;
    public int RangedDamage;
    public float AttackRate;
    public GameObject Bullet;
    public Transform Shooter;
    

    public override void Chasing()
    {
        if(Vector3.Distance(playercharacter.transform.position,transform.position) < RangeDistance - 1f)
        {
            Shooter.LookAt(playercharacter.transform.position + Vector3.up);
            Attack();
        }
        else
        base.Chasing();     
    }

    
    public void Attack()
    { 
            AtttentionSpan_counter = 1;
            agent.SetDestination(transform.position);
            agent.speed = 0;
            Shoot();    
    }

    float Rate;
    public void Shoot()
    {
        Rate -= Time.deltaTime;
        if (Rate <= 0)
        {
            Debug.Log("Shot at player");
            Rate = AttackRate;
            GameManager.instance.PlaySFX(1);
            Instantiate(Bullet, Shooter.position, Shooter.rotation);
        }

    }




    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeDistance);
    }

}
