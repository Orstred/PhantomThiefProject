using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Destructable : Interactable
{
    [ShowIf("useSeparateMesh")]
    public Transform DestroyedMesh;
    public bool useSeparateMesh;



    public override void Interact()
    {
        base.Interact();
        Break();
    }



    private void Break()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (!useSeparateMesh)
            {
                t.parent = null;
                t.gameObject.AddComponent<Rigidbody>();
            }
            else
            {
                Destroy(t.gameObject);
            }
        }
        if (useSeparateMesh)
        {
           Instantiate(DestroyedMesh, transform.position, transform.rotation);
        }
    }
}
