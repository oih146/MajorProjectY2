using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatChickenWalk : Cheat {

    public override void Execute()
    {
        base.Execute();
        PlayerMovement.Instance.SwitchWalk();
    }

}
