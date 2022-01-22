using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Destructable : Interactable
{
    [ShowIf("UseSeparateMesh")]
    public Transform DestroyedMesh;
    public bool UseSeparateMesh;



    public override void Interact()
    {
        base.Interact();
        Break();
    }



    private void Break()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (!UseSeparateMesh)
            {
                t.parent = null;
                t.gameObject.AddComponent<Rigidbody>();
            }
            else
            {
                Destroy(t.gameObject);
            }
        }
        if (UseSeparateMesh)
        {
           Instantiate(DestroyedMesh, transform.position, transform.rotation);
        }
    }
}
