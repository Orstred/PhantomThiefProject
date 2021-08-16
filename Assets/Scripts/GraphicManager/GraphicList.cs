using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum profession { PhantomThief = 1, Police, Civilian, Miscellaneous }
[System.Serializable]
public class GraphicList 
{
    [HideInInspector]
    public profession Profession;
    public Costumes[] PhantomThief_Costume;
    public Costumes[] Police_Costume;
    public Costumes[] Civilian_Costume;
    public Costumes[] Miscellaneous;

   public Costumes[] CurrentProfession()
    {
        if(Profession == profession.PhantomThief)
        {
            return PhantomThief_Costume;
        }
        else if(Profession == profession.Police)
        {
            return Police_Costume;
        }
        else if(Profession == profession.Civilian)
        {
            return Civilian_Costume;
        }
        else
        return Miscellaneous;
    }
}
