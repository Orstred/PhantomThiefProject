using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum PhantomItems {Graplehook, Claw}
public class UIItem : MonoBehaviour
{

    public PhantomItems Item;
   
    public void OnInteract()
    {
        if(Item == PhantomItems.Graplehook)
        {
            GameManager.instance.playerCharacter.GetComponent<GrappleShot>().enabled = true;
        }
    }


}
