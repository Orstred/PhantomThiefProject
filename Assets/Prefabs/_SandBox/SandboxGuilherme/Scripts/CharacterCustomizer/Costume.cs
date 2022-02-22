using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _costume
{
    public GameObject Hat;
    public GameObject Body;
    public GameObject Leg;
}

[CreateAssetMenu(fileName = "Costume", menuName = "Character/Costume", order = 1)]
public class Costume : ScriptableObject
{


    public int _id;
    public _costume Female;
    public _costume Male;

}
