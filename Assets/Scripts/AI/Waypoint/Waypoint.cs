using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Waypoint : MonoBehaviour
{

        
    //Instances
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;

    public bool isBranching;
    [ShowIf("isBranching")]
    public WaypointManager Branch;
    [ShowIf("isBranching")]
    public Waypoint Entrypoint;
    [ShowIf("isBranching")]
    [Range(0,100)]
    public float BranchChance;
    [ShowIf("isBranching")]
    public bool BranchRandomWay;
    [ShowIf("isBranching")]
    public bool BranchForward;
    public bool hasEvent = false;

    [Button]
    public void RemoveBranch()
    {
        isBranching = false;
        DestroyImmediate(Branch.gameObject);
        Branch = null;
        Entrypoint = null;
    }


    public void Set(Waypoint prev = null, Waypoint nex = null)
    {
        previousWaypoint = prev;
        nextWaypoint = nex;

        if(nex != null && prev != null)
        {
            transform.position = Vector3.Lerp(prev.transform.position, nex.transform.position, .5f);
            transform.localScale = Vector3.Lerp(prev.transform.localScale, nex.transform.localScale, .5f);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(prev.transform.eulerAngles, nex.transform.eulerAngles, .5f));
        }
        else if(prev != null)
        {
            transform.position =prev.transform.position + prev.transform.forward;
            transform.rotation = prev.transform.rotation;
            transform.localScale = prev.transform.localScale;
        }
        else if(prev == null && nex == null)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }


    }








    public Vector3 GetPosition()
    {
        Vector3 minbound = (transform.position + transform.right * transform.localScale.x);
        Vector3 maxbound = (transform.position - transform.right * transform.localScale.x);

        return Vector3.Lerp(minbound, maxbound, Random.Range(0f, 1f));
    }

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (isBranching)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, .2f);
            Gizmos.DrawCube(transform.position + transform.up, new Vector3(.2f, 1.3f, .2f));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + transform.up * 1.7f, Entrypoint.transform.position);
        }
        else
        {
            Gizmos.DrawSphere(transform.position, .1f);
        }

        
    }

}
