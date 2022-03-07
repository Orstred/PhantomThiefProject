using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{


    Guard_Base parentAI;
    LayerMask IgnoreLayers;

    private void Start()
    {
        parentAI = GetComponentInParent<Guard_Base>();
        IgnoreLayers = parentAI.RaycastIgnore;
    }


    private void OnTriggerEnter(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
        if (other.tag == "Player")
        {
            if(IsPlayerVisible(other.GetComponent<PlayerCharacter>()))
            parentAI.OnPlayerEnterVision(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    { 
        if (other.tag == "Player")
        {
            if (IsPlayerVisible(other.GetComponent<PlayerCharacter>()))
            {
                parentAI.OnPlayerStayInVision(other.gameObject);
            }
            else
            {
                parentAI.OnPlayerExitVision(other.gameObject);
            }      
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        if (other.tag == "Player")
        {
            parentAI.OnPlayerExitVision(other.gameObject);
        }
    }

    
    public bool IsPlayerVisible(PlayerCharacter p)
    {
        if (!p.inShadow && !(Physics.Linecast(transform.position, p.PlayerHeadDetector.position, ~IgnoreLayers) && Physics.Linecast(transform.position, p.PlayerChestDetector.position, ~IgnoreLayers)))
        { 
        return true;   
        }
        else
        {
        return false;
        }
        
    }
}
