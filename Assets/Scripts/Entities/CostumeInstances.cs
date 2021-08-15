using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SlimBody
{
    public List<GameObject> Heads;
    public List<GameObject> Bodies;
    public List<GameObject> Legs;
}
[System.Serializable]
public struct BuiltBody
{
    public List<GameObject> Heads;
    public List<GameObject> Bodies;
    public List<GameObject> Legs;
}
[System.Serializable]
public struct ChubbyBody
{
    public List<GameObject> Heads;
    public List<GameObject> Bodies;
    public List<GameObject> Legs;
}

[System.Serializable]
public struct PhantomCostumes
{
    public SlimBody SlimCostumes;
    public BuiltBody BuiltCostumes;
    public ChubbyBody ChubbyCostumes;
}
[System.Serializable]
public struct PoliceCostumes
{
    public SlimBody SlimCostumes;
    public BuiltBody BuiltCostumes;
    public ChubbyBody ChubbyCostumes;
}
[System.Serializable]
public struct CivilianCostumes
{
    public SlimBody SlimCostumes;
    public BuiltBody BuiltCostumes;
    public ChubbyBody ChubbyCostumes;
}
[System.Serializable]
public struct FireFighterCostumes
{
    public SlimBody SlimCostumes;
    public BuiltBody BuiltCostumes;
    public ChubbyBody ChubbyCostumes;
}
[System.Serializable]
public struct MedicCostumes
{
    public SlimBody SlimCostumes;
    public BuiltBody BuiltCostumes;
    public ChubbyBody ChubbyCostumes;
}

[System.Serializable]
public class CostumeInstances 
{

    public PhantomCostumes _PhantomThiefInstances;
    public PoliceCostumes _PoliceCostumes;
    public CivilianCostumes _CivilianCostumes;
    public FireFighterCostumes _FireFighterCostumes;
    public MedicCostumes _MedicCostumes;
}
