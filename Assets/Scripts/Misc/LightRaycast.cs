using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRaycast : MonoBehaviour
{

    
     public bool isSun;
     public bool isTrigger;

    public LayerMask Detect;
    PlayerCharacter playercharacter;
    Transform _transform;
    Transform player;
    Vector3 po;
    
   

    
    void Start()
    {
        _transform = transform;
        player = GameManager.instance.playerCharacter;
        playercharacter = GameManager.instance.playerCharacter.GetComponent<PlayerCharacter>();
    }

    
    void Update()
    {
        if (!isTrigger)
        {
            if (isSun)
            {
                po = -(Quaternion.Euler(_transform.eulerAngles) * Vector3.forward);
                if (!Physics.Linecast(player.position, po * 10000f, ~Detect))
                {
                    if (!playercharacter.LightSources.Contains(gameObject))
                    {
                        playercharacter.LightSources.Add(gameObject);
                    }
                }
                else if (playercharacter.LightSources.Contains(gameObject))
                {
                    playercharacter.LightSources.Remove(gameObject);
                }
            }
            else if (!isSun)
            {
                if (!Physics.Linecast(_transform.position, player.position, ~Detect))
                {
                    if (!playercharacter.LightSources.Contains(gameObject))
                    {
                        playercharacter.LightSources.Add(gameObject);
                    }
                }
                else if (playercharacter.LightSources.Contains(gameObject))
                {
                    playercharacter.LightSources.Remove(gameObject);
                }
            }
        }
     }


    public void OnTriggerEnter(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
        if (other.tag == "Player")
        {
            isTrigger = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        if (other.tag == "Player")
        {
            isTrigger = true;
            playercharacter.LightSources.Remove(gameObject);
        }
    }

    private void OnDisable()
    {
        if(player != null)
        player.GetComponent<PlayerCharacter>().LightSources.Remove(gameObject); 
    }
    private void OnDestroy()
    {
        if(player != null)
        player.GetComponent<PlayerCharacter>().LightSources.Remove(gameObject);
    }


}
