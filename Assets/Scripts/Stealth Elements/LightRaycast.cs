using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRaycast : MonoBehaviour
{

    
     public bool isSun;
     public bool isTrigger;


    PlayerCharacter playercharacter;
    Transform _transform;
    Transform player;
    Vector3 po;
    


    
    void Start()
    {
        player = GameManager.instance.playerCharacter;
        playercharacter = GameManager.instance.playerCharacter.GetComponent<PlayerCharacter>();
    }

    
    void Update()
    {
        if (!isTrigger)
        {
            if (isSun)
            {
                po = -(Quaternion.Euler(transform.eulerAngles) * Vector3.forward);
                if (!Physics.Linecast(player.position, po * 10000f, ~LayerMask.GetMask("Player")))
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
                if (!Physics.Linecast(transform.position, player.position, ~LayerMask.GetMask("Player")))
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


    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.tag == "Player")
            {
                player = other.transform;
                other.GetComponent<PlayerCharacter>().LightSources.Add(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isTrigger)
        {
            player = GameManager.instance.playerCharacter;
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerCharacter>().LightSources.Remove(gameObject);
            }
        }
    }

    private void OnDisable()
    {
        player.GetComponent<PlayerCharacter>().LightSources.Remove(gameObject); 
    }
    private void OnDestroy()
    {
        player.GetComponent<PlayerCharacter>().LightSources.Remove(gameObject);
    }


}
