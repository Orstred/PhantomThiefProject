using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponentInParent<Enemy>().State = StateMachine.Chasing;
            GetComponentInParent<Enemy>().Voice.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
   if(other.tag == "Player")
        {
            if (other.tag == "Player")
            {
                GetComponentInParent<Enemy>().State = StateMachine.Patrolling;
            }
        }
    }


}
