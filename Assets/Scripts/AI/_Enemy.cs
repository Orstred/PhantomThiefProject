using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class _Enemy : NPC
{

    [HorizontalLine(2f, EColor.Gray)]
    [Header("ENEMY OPTIONS")]
    public GameObject VisionCone;
    public float AttentionSpan;
    public Vector3 GuardPosition;





}
