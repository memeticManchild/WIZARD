using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWandHandler : WandHandler {

    public override bool Cast(bool useButtonDown, bool useButtonHold, bool useButtonUp)
    {
        if (useButtonDown && timeSincePrevUse > wand.castCooldown)
        {
            Vector2 direction = transform.up;
            wand.pattern.Create(this);
            
            timeSincePrevUse = 0;
            return true;
        }
        return false;
    }
}
