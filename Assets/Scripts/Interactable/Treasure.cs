using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Treasure : Interactable
{


    public int CashValue;

    public override void Interact()
    {
        base.Interact();
        manager.Cash += CashValue;
        manager.PlaySFX("Caching");
        Destroy(gameObject);
    }


}
