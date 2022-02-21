using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeripheralVisionCone : MonoBehaviour
{



    Enemy en;


    private void Start()
    {
        en = GetComponentInParent<Enemy>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            en.OnPlayerEnterPeripheralVision(other.transform.position);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            en.OnPlayerStayInPeripheralVision(other.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            en.OnPlayerExitPeripheralVision(other.transform.position);
        }
    }





}
