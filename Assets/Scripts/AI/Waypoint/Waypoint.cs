using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;
public class Waypoint : MonoBehaviour
{



    [Button]
    public void AddPointInFront()
    {
        if(nextWaypoint != null)
        {
            GetComponentInParent<WaypointManager>().AddWaypointAfter();
        }
        else
        {
            GetComponentInParent<WaypointManager>().NewWaypoint();
        }
    }
    [Button]
    public void AddPointAtBack()
    {
        GetComponentInParent<WaypointManager>().AddWaypointBefore();
    }
    [Button]
    public void AddBranch()
    {
        Branch.Add(new GameObject("BranchPath" + Branch.Count).AddComponent<WaypointManager>());
        Selection.activeGameObject = Branch[Branch.Count - 1].gameObject;
        Selection.activeGameObject.transform.position = transform.position;
    }
    [Button]
    public void Remove()
    {
        GetComponentInParent<WaypointManager>().RemoveWaypoint();
    }
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;
    public List <WaypointManager> Branch;
    public float BranchChance;
    public float BranchIn;
    [HideInInspector]
    public float widht = 1;


    [HideInInspector]
    public bool alwaysshow = false;

    public Vector3 GetPosition()
    {
       // Gizmos.DrawLine(i.transform.position - i.transform.right * i.widht / 2f, i.transform.position + i.transform.right * i.widht / 2f);
        Vector3 minbound = (transform.position + transform.right * widht / 2f);
        Vector3 maxbound = (transform.position - transform.right * widht / 2f);

        return Vector3.Lerp(minbound, maxbound, Random.Range(0f,1f));
    }



    public void RemoveAllBranches()
    {
        foreach (WaypointManager m in Branch)
        {
            Branch.Remove(m);
            DestroyImmediate(m.gameObject);
        }
        
    }

    private void OnDrawGizmos()
    {
        if(alwaysshow)
        Gizmos.DrawSphere(transform.position, .1f);
    }
    private void OnDrawGizmosSelected()
    {
        widht = transform.localScale.x;
        alwaysshow = transform.parent.GetComponent<WaypointManager>().AlwaysShow;
        if (!alwaysshow)
                Gizmos.DrawSphere(transform.position, .1f);
        
    }

}
