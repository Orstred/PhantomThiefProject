using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    Enemy _controller;
    PlayerCharacter playercharacter;

    private void Start()
    {
        _controller = GetComponentInParent<Enemy>();
        playercharacter = GameManager.instance.playerCharacter.GetComponent<PlayerCharacter>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!Physics.Linecast(transform.position, playercharacter.PlayerHeadDetector.transform.position, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) || !Physics.Linecast(transform.position, playercharacter.PlayerChestDetector.transform.position, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
                _controller.OnPlayerEnterView();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!Physics.Linecast(transform.position, playercharacter.PlayerHeadDetector.transform.position, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) || !Physics.Linecast(transform.position, playercharacter.PlayerChestDetector.transform.position, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
                _controller.OnPlayerStayInView();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

                _controller.OnPlayerExitView();
        }
    }

}
