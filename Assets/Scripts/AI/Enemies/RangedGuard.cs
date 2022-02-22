using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGuard : Guard_Base
{



    public override void Patroll()
    {
        base.Patroll();
        agent.speed = WalkSpeed;
    }

    public override void Chasing()
    {
        base.Chasing();
        agent.speed = RunSpeed;
    }

}
