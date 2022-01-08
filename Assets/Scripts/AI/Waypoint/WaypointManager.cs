using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;
public class WaypointManager : MonoBehaviour
{

    public List<Waypoint> Pathway;
    public bool AlwaysShow = false;
    public bool Loop;


#if UNITY_EDITOR
    [Button]
    public void NewWaypoint()
    {
        if (!Application.isPlaying)
        {
            if (!Loop)
            {
                if (Pathway.Count > 0)
                {
                    Pathway.Add(new GameObject("Waypoint" + Pathway.Count).AddComponent<Waypoint>());
                    Pathway[Pathway.Count - 1].transform.parent = transform;
                    Pathway[Pathway.Count - 1].transform.position = Pathway[Pathway.Count - 2].transform.position + Pathway[Pathway.Count - 2].transform.forward;
                    Pathway[Pathway.Count - 1].transform.rotation = Pathway[Pathway.Count - 2].transform.rotation;
                    Pathway[Pathway.Count - 1].transform.localScale = Pathway[Pathway.Count - 2].transform.localScale;
                }
                else
                {
                    Pathway.Add(new GameObject("Waypoint" + Pathway.Count).AddComponent<Waypoint>());
                    Pathway[0].transform.parent = transform;
                    Pathway[0].transform.position = transform.position;
                    Pathway[0].transform.rotation = transform.rotation;
                    Pathway[0].transform.localScale = Vector3.one;
                }



                if (!Application.isPlaying)
                {
                    Selection.activeGameObject = Pathway[Pathway.Count - 1].gameObject;
                }

            }
        }
    }
    [Button]
    public void AddBranchingPath()
    {
        if (!Application.isPlaying)
        {
            Selection.activeGameObject.GetComponent<Waypoint>().AddBranch();
        }
          
    }
    [Button]
    public void RemoveAllBranchingPaths()
    {
        if(!Application.isPlaying)
        {
            Selection.activeGameObject.GetComponent<Waypoint>().RemoveAllBranches();
        }
    }
    [Button]
    public void AddWaypointBefore()
    {
        if (!Application.isPlaying)
        {
            if(Selection.activeGameObject.GetComponent<Waypoint>() != Pathway[0])
            {
                int sit = Pathway.IndexOf(Selection.activeGameObject.GetComponent<Waypoint>());
                Pathway.Insert(Pathway.IndexOf(Selection.activeGameObject.GetComponent<Waypoint>()), new GameObject("Waypoint" + sit).AddComponent<Waypoint>());
                Pathway[sit].transform.parent = transform;
                Vector3 midpoint = (Pathway[sit - 1].transform.position - Pathway[sit + 1].transform.position);
                Quaternion midrotation = Quaternion.Euler(Pathway[sit - 1].transform.eulerAngles - Pathway[sit + 1].transform.eulerAngles);

                Pathway[sit].transform.position = Pathway[sit + 1].transform.position + midpoint * 0.5f;
                Pathway[sit].transform.rotation = Quaternion.Euler(Pathway[sit + 1].transform.eulerAngles - (midrotation.eulerAngles).normalized * 0.5f);
            }
        }
    }
    [Button]
    public void AddWaypointAfter()
    {
        if (!Application.isPlaying)
        {
            int sit = Pathway.IndexOf(Selection.activeGameObject.GetComponent<Waypoint>());
            Pathway.Insert(Pathway.IndexOf(Selection.activeGameObject.GetComponent<Waypoint>()) + 1, new GameObject("Waypoint" + sit).AddComponent<Waypoint>());

            sit++;
            Pathway[sit].transform.parent = transform;
            Selection.activeGameObject = Pathway[sit].gameObject;
            Vector3 midpoint = (Pathway[sit - 1].transform.position - Pathway[sit + 1].transform.position);
            Quaternion midrotation = Quaternion.Euler(Pathway[sit - 1].transform.eulerAngles - Pathway[sit + 1].transform.eulerAngles);

            Pathway[sit].transform.position = Pathway[sit + 1].transform.position + midpoint * 0.5f;
            Pathway[sit].transform.rotation = Quaternion.Euler(Pathway[sit + 1].transform.eulerAngles - (midrotation.eulerAngles).normalized * 0.5f);
        }
    }
    [Button]
    public void RemoveWaypoint()
    {
        if (!Application.isPlaying)
        {
            if (Selection.activeGameObject.GetComponent<Waypoint>())
            {
                foreach (GameObject g in Selection.gameObjects)
                {
                    Pathway.Remove(g.GetComponent<Waypoint>());
                    Object.DestroyImmediate(g.gameObject);
                }
            }
        }

    }
#endif



    private void OnDrawGizmos()
    {
        if (AlwaysShow && Pathway.Count > 1)
        {
            foreach (Waypoint i in Pathway)
            {
                //Sets the previous waypoint if it's not out of range
                if (Pathway.IndexOf(i) != 0) { i.previousWaypoint = Pathway[Pathway.IndexOf(i) - 1]; }

                //Sets the next waypoint if it's not out of range
                if (i != LastWayPoint()) { i.nextWaypoint = Pathway[Pathway.IndexOf(i) + 1]; }



                //Draws 2 parallel lines that go from one waypoint to another
                if (Pathway.IndexOf(i) + 1 < Pathway.Count)
                {
                    Gizmos.DrawLine(i.transform.position - i.transform.right * i.widht / 2, i.nextWaypoint.transform.position - i.nextWaypoint.transform.right * i.nextWaypoint.widht / 2);
                    Gizmos.DrawLine(i.transform.position + i.transform.right * i.widht / 2, i.nextWaypoint.transform.position + i.nextWaypoint.transform.right * i.nextWaypoint.widht / 2);
                }

                //draws an horizontal line it's diameter being the local x scale of the waypoint
                DrawHorizontalLine(i);

                //Draws an horizontal line from the two ends of the final waypoint
                DrawHorizontalLine(LastWayPoint());
            }


            //adds the first waypoint to the last index as well
            if (Loop)
            {
                if (Pathway[0] != LastWayPoint())
                    Pathway.Add(Pathway[0]);
            }
            //Removes the first waypoint from last index so you can loop
            else if (LastWayPoint() == Pathway[0])
            {
                Pathway.RemoveAt(Pathway.Count - 1);
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (!AlwaysShow && Pathway.Count > 1)
        {
            foreach (Waypoint i in Pathway)
            {
                //Sets the previous waypoint if it's not out of range
                if (Pathway.IndexOf(i) != 0) { i.previousWaypoint = Pathway[Pathway.IndexOf(i) - 1]; }

                //Sets the next waypoint if it's not out of range
                if (i != LastWayPoint()) { i.nextWaypoint = Pathway[Pathway.IndexOf(i) + 1]; }



                //Draws 2 parallel lines that go from one waypoint to another
                if (Pathway.IndexOf(i) + 1 < Pathway.Count)
                {
                    Gizmos.DrawLine(i.transform.position - i.transform.right * i.widht / 2, i.nextWaypoint.transform.position - i.nextWaypoint.transform.right * i.nextWaypoint.widht / 2);
                    Gizmos.DrawLine(i.transform.position + i.transform.right * i.widht / 2, i.nextWaypoint.transform.position + i.nextWaypoint.transform.right * i.nextWaypoint.widht / 2);
                }

                //draws an horizontal line it's diameter being the local x scale of the waypoint
                DrawHorizontalLine(i);

                //Draws an horizontal line from the two ends of the final waypoint
                DrawHorizontalLine(LastWayPoint());
            }


            //adds the first waypoint to the last index as well
            if (Loop)
            {
                if (Pathway[0] != LastWayPoint())
                    Pathway.Add(Pathway[0]);
            }
            //Removes the first waypoint from last index so you can loop
            else if (LastWayPoint() == Pathway[0])
            {
                Pathway.RemoveAt(Pathway.Count - 1);
            }
        }

    }




    //Draws a line between the 2 extremmes of the waypoint
    public void DrawHorizontalLine(Waypoint a)
    {
        Vector3 minbound = (a.transform.position + a.transform.right * a.widht / 2f);
        Vector3 maxbound = (a.transform.position - a.transform.right * a.widht / 2f);

        Gizmos.DrawLine(minbound, maxbound);
    }

    //returns the final member of the pathway list
    public Waypoint LastWayPoint()
    {
        return Pathway[Pathway.Count - 1];
    }

}
