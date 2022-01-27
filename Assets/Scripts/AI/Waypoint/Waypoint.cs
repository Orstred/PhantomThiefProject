using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;



public class Waypoint : MonoBehaviour
{


    //Instances
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;

    #region Advanced Options Branching
    //Branching path options
    [Foldout("Advanced Options")]
    public bool Branching;

    [Foldout("Advanced Options")]
    [ShowIf("Branching")]
    public List<WaypointManager> Branch;

    [Foldout("Advanced Options")]
    [ShowIf("Branching")]
    [Range (0,1)]
    public float BranchChance;

    [Foldout("Advanced Options")]
    [ShowIf("Branching")]
    public bool BranchIn;

    [Foldout("Advanced Options")]
    [ShowIf("Branching")]
    public int EntryPoint;
    #endregion

    #region Advanced Options Events
    //Pathway events options
    [Foldout("Advanced Options")]
    [HorizontalLine(1f, EColor.White)]
    public GameObject[] InterestPoints;
    #endregion

    [HideInInspector]
    public float widht = 1;
    [HideInInspector]
    public bool alwaysshow = true;


#if UNITY_EDITOR
    [Button]
    public void AddPointInFront()
    {
        if (Application.isEditor)
        {
            if (nextWaypoint != null)
            {
                GetComponentInParent<WaypointManager>().AddWaypointAfter();
            }
            else
            {
                GetComponentInParent<WaypointManager>().NewWaypoint();
            }
        }

    }
    [Button]
    public void AddPointAtBack()
    {
        if(Application.isEditor)
        GetComponentInParent<WaypointManager>().AddWaypointBefore();
    }
    [ShowIf("Branching")]
    [Button]
    public void AddBranch()
    {
        
        
            Branching = true;
            Branch.Add(new GameObject("BranchPath" + Branch.Count).AddComponent<WaypointManager>());
            Branch[Branch.Count - 1].transform.parent = transform;
            Selection.activeGameObject = Branch[Branch.Count - 1].gameObject;
            Selection.activeGameObject.transform.position = transform.position;
            Selection.activeGameObject.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);


            Selection.activeGameObject.GetComponent<WaypointManager>().NewWaypoint();
            Selection.activeGameObject.GetComponent<WaypointManager>().NewWaypoint();

    

    }
    [Button]
    public void Remove()
    {
     
            GetComponentInParent<WaypointManager>().RemoveWaypoint();
    }
    [ShowIf("Branching")]
    [Button]
    public void RemoveAllBranches()
    {

        
            foreach (WaypointManager m in Branch)
            {
                Branch.Remove(m);
                DestroyImmediate(m.gameObject);
            }
            Branching = false;

    }
#endif






    public Vector3 GetPosition()
    {  
        Vector3 minbound = (transform.position + transform.right * widht / 2f);
        Vector3 maxbound = (transform.position - transform.right * widht / 2f);

        return Vector3.Lerp(minbound, maxbound, Random.Range(0f,1f));
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

        Gizmos.color = Color.blue;
       // if(Branching)
       // Gizmos.DrawLine(transform.position, Branch[0].Pathway[EntryPoint].transform.position);
        
    }

}
