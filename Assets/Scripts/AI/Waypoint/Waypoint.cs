using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Waypoint : MonoBehaviour
{

        
    //Instances
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;


    #region Branching Options
    public bool isBranching;
    [ShowIf("isBranching")]
    public WaypointManager Branch;
    [ShowIf("isBranching")]
    public Waypoint Entrypoint;
    [ShowIf("isBranching")]
    [Range(0, 100)]
    public float BranchChance;
    [ShowIf("isBranching")]
    public bool BranchRandomWay;
    [ShowIf("isBranching")]
    public bool BranchForward;
    public bool hasEvent = false;

    #endregion

#if UNITY_EDITOR
    [Button]
    public void NewWaypoint()
    {
        GetComponentInParent<WaypointManager>().NewWaypoint();
    }
    [Button]
    public void RemoveWaypoint()
    {
        GetComponentInParent<WaypointManager>().RemoveWaypoint();
    }
    [Button]
    public void AddWayPointAfter()
    {
        GetComponentInParent<WaypointManager>().AddWaypointAfter();
    }
    [Button]
    public void AddWaypointBefore()
    {
        GetComponentInParent<WaypointManager>().AddWaypointBefore();
    }
    [Button]
    public void AddBranch()
    {
        GetComponentInParent<WaypointManager>().AddBranchingPath();
    }
    [Button]
    public void RemoveBranch()
    {
        GetComponentInParent<WaypointManager>().RemoveBranchFromPoint();
    }
#endif




    public void Set(Waypoint previous = null, Waypoint next = null)
    {
        previousWaypoint = previous;
        nextWaypoint = next;

        if(next != null && previous != null)
        {
            transform.position = Vector3.Lerp(previous.transform.position, next.transform.position, .5f);
            transform.localScale = Vector3.Lerp(previous.transform.localScale, next.transform.localScale, .5f);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(previous.transform.eulerAngles, next.transform.eulerAngles, .5f));
        }
        else if(previous != null)
        {
            transform.position =previous.transform.position + previous.transform.forward;
            transform.rotation = previous.transform.rotation;
            transform.localScale = previous.transform.localScale;
        }
        else if(previous == null && next == null)
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
            Gizmos.DrawSphere(transform.position, .2f);
        }  
    }

}
