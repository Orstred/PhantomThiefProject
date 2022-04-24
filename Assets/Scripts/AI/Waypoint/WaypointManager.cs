using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;
public class WaypointManager : MonoBehaviour
{


    public List<Waypoint> Pathway = new List<Waypoint>();
    public bool Loop;
    public bool isIntersected;



#if UNITY_EDITOR
    [Button]
    public void NewWaypoint()
    {
        //Adds an waypoint at point 0 and resets its transform
        if(Pathway.Count == 0)
        {
            Pathway.Add(new GameObject().AddComponent<Waypoint>());
            Pathway[0].transform.parent = transform;
            Pathway[0].Set();
        }
        //Adds an waypoint in front of the first waypoint and sets the way point 0 as its previouswaypoint
        else if (Pathway.Count == 1)
        {
             Pathway.Add(new GameObject().AddComponent<Waypoint>());
             Pathway[0].nextWaypoint = LastWayPoint();
             LastWayPoint().Set(Pathway[0]);
        }
        //Adds an waypoint in front of the previously last waypoint and adds itself as the next waypoint of the previous waypoint
        else
        {
            Pathway.Add(new GameObject().AddComponent<Waypoint>());
            LastWayPoint().Set(Pathway[Pathway.Count - 2]);
            LastWayPoint().previousWaypoint.nextWaypoint = LastWayPoint();
        }

        LastWayPoint().transform.parent = transform;
    }
    [Button]
    public void RemoveWaypoint()
    {
        if (Selection.activeGameObject.GetComponent<Waypoint>() && Selection.activeGameObject.GetComponent<Waypoint>() != Pathway[0])
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                //Saves the selected objects index removes the object from the list and destroys it
                int wayindex = Pathway.IndexOf(g.GetComponent<Waypoint>());
                Pathway.Remove(g.GetComponent<Waypoint>());
                Object.DestroyImmediate(g.gameObject);

                //uses the saved index to select the previous waypoint 
                Selection.activeGameObject = Pathway[wayindex - 1].gameObject;

                //If current point is not null then select it and assing it's previous waypoint
                if (Pathway[wayindex].gameObject != null)
                {
                    //and attribute the next and previous waypoint to it's neighbors
                    Selection.activeGameObject.GetComponent<Waypoint>().nextWaypoint = Pathway[wayindex];
                    Pathway[wayindex].previousWaypoint = Pathway[wayindex - 1];
                }
            }
       
        }
    }
    [Button]
    public void AddWaypointAfter()
    {
        //if there is nothing after current waypoint
        if (Selection.activeGameObject.GetComponent<Waypoint>() == LastWayPoint())
        {
            NewWaypoint();
        }
        else
        {
            //Gets the index of the next way point
            int id = Pathway.IndexOf(Selection.activeGameObject.GetComponent<Waypoint>()) + 1;

            Pathway.Insert(id,new GameObject().AddComponent<Waypoint>());

            //Sets the previous waypoint of the next waypoint and vice versa
            Pathway[id + 1].previousWaypoint = Pathway[id];
            Pathway[id - 1].nextWaypoint = Pathway[id];

            //Sets the previous and next waypoint of the new point
            Pathway[id].Set(Pathway[id - 1], Pathway[id + 1]);
            Pathway[id].transform.parent = transform;
        }
    }
    [Button]
    public void AddWaypointBefore()
    {
        //if there is nothing before current waypoint
        if (Selection.activeGameObject.GetComponent<Waypoint>() == Pathway[0])
        {


        }
        else
        {
            //Gets the index of the previous way point
            int id = Pathway.IndexOf(Selection.activeGameObject.GetComponent<Waypoint>());

            Pathway.Insert(id, new GameObject().AddComponent<Waypoint>());

            //Sets the previous waypoint of the next waypoint and vice versa
            Pathway[id + 1].previousWaypoint = Pathway[id];
            Pathway[id - 1].nextWaypoint = Pathway[id];

            //Sets the previous and next waypoint of the new point
            Pathway[id].Set(Pathway[id - 1], Pathway[id + 1]);
            Pathway[id].transform.parent = transform;
        }

    }
    [Button]
    public void AddBranchingPath()
    {
        if (Selection.activeGameObject.GetComponent<Waypoint>())
        {
            Waypoint point = Selection.activeGameObject.GetComponent<Waypoint>();

            //Instantiates a new path 
            point.isBranching = true;
            point.Branch = new GameObject().AddComponent<WaypointManager>();


            //Adds 2 points to path
            point.Branch.NewWaypoint(); point.Branch.NewWaypoint();

            //Sets the entry point
            point.Entrypoint = point.Branch.Pathway[1];

            //Sets the transform of the new waypoint manager
            point.Branch.transform.position = point.transform.position + point.transform.right;
            point.Branch.transform.Rotate(Vector3.up * 90);

            //Adds a branch on the opposite path for 2 way transport
            point.Branch.Pathway[0].isBranching = true;
            point.Branch.Pathway[0].Branch = this;
            point.Branch.Pathway[0].Entrypoint = Pathway[Pathway.IndexOf(point) - 1];

        }
    }
    [Button]
    public void RemoveBranchFromPoint() 
    {
        if (Selection.activeGameObject.GetComponent<Waypoint>())
        {
            Selection.activeGameObject.GetComponent<Waypoint>().isBranching = false;
            DestroyImmediate(Selection.activeGameObject.GetComponent<Waypoint>().Branch.gameObject);
            Selection.activeGameObject.GetComponent<Waypoint>().Branch = null;
            Selection.activeGameObject.GetComponent<Waypoint>().Entrypoint = null;
        }
    }
    [Button]
    public void AllPointsToGround()
    {
        foreach(Waypoint w in Pathway)
        {
            RaycastHit hit;
            if (Physics.Raycast(w.transform.position, -w.transform.up * 10, out hit))
                w.transform.position = hit.point;
        }
    }
#endif






    private void OnDrawGizmos()
    {
        //Draws the horizontal lines from the waypoints and connects the extremes if there is at least one waypoint
        if(Pathway.Count > 1)
        {
            foreach(Waypoint w in Pathway)
            {
                DrawHorizontalLine(w);
                ConnectWayPoints(w);
            }



            if (Loop)
            {
                LastWayPoint().nextWaypoint = Pathway[0];
                Pathway[0].previousWaypoint = LastWayPoint();
            }
            else
            {
                LastWayPoint().nextWaypoint = null;
                Pathway[0].previousWaypoint = null;
            }
        }
    }



    //Draws a line between the 2 extremmes of the waypoint
    void DrawHorizontalLine(Waypoint a)
    {
        Vector3 minbound = (a.transform.position + a.transform.right * a.transform.localScale.x);
        Vector3 maxbound = (a.transform.position - a.transform.right * a.transform.localScale.x);

        Gizmos.DrawLine(minbound, maxbound);
    }

    //Connects the two extremes of two waypoints
    void ConnectWayPoints(Waypoint a)
    {
        if(a.nextWaypoint != null)
        {
            Gizmos.DrawLine(a.transform.position - a.transform.right * a.transform.localScale.x, a.nextWaypoint.transform.position - a.nextWaypoint.transform.right * a.nextWaypoint.transform.localScale.x);
            Gizmos.DrawLine(a.transform.position + a.transform.right * a.transform.localScale.x, a.nextWaypoint.transform.position + a.nextWaypoint.transform.right * a.nextWaypoint.transform.localScale.x);
        }
    }

    //returns the final member of the pathway list
    public Waypoint LastWayPoint()
    {
        return Pathway[Pathway.Count - 1];
    }
}
