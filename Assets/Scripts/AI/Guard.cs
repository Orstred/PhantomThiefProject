using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class Guard : Enemy
{

    [HorizontalLine(2f, EColor.Gray)]
    [Header("ENEMY OPTIONS")]
    public GameObject VisionCone;
    public float AttentionSpan;
    public Vector3 GuardPosition;

    [HorizontalLine(2f, EColor.Gray)]
    [Header("ENEMY OPTIONS")]
    public float MeleeDistance;
    public int MeleeDamage;
    public bool hasGun;
    [ShowIf("hasGun")]
    public float RangedDistance;
    [ShowIf("hasGun")]
    public int RangedDamage;





}
