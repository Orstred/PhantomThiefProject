using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]

public class BodyGraphic 
{
    [AllowNesting]
    [Foldout("Instances")]
    public List<GameObject> Heads;
    [Foldout("Instances")]
    public List<GameObject> Body;
    [Foldout("Instances")]
    public List<GameObject> Legs;
    public int Head_id;
    public int Body_id;
    public int Legs_id;
}
