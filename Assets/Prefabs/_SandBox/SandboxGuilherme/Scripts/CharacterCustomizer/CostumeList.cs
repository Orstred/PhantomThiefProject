using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[CreateAssetMenu(fileName = "costumes", menuName = "Character/CostumeList", order = 2)]
public class CostumeList : ScriptableObject
{
    public Costume[] costumes;

    [Button]
    public void UpdateID()
    {
         for (int x = 0; x < costumes.Length; x++)
        {
            costumes[x]._id = x;
        }
    }
}
