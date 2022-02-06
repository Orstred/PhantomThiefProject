using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{


    Enemy en;


    private void Start()
    {
        en = GetComponentInParent<Enemy>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            en.OnPlayerEnterVision(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            en.OnPlayerStayInVision(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            en.OnPlayerExitVision(other.gameObject);
        }
    }

}
