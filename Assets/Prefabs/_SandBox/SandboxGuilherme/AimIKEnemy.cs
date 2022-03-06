using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIKEnemy : MonoBehaviour
{

    Transform player;
    Guard_Base en;



    private void Start()
    {
        player = GameManager.instance.playerCharacter.GetComponent<PlayerCharacter>().PlayerChestDetector;
        en = GetComponentInParent<Guard_Base>();
     
    }

    private void Update()
    {
        if(en.State == EnemyState.Chasing)
        transform.LookAt(player);
        else
        {
            transform.rotation = transform.root.rotation;
        }
    }
}
