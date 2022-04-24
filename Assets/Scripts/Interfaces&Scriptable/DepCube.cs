using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepCube : IItem, IDeployable
{


   
    public void Deploy()
    {
    
    }
    public void Remove()
    {
        
    }


    new public void Use()
    {
        Deploy();
    }
}
