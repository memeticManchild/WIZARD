using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieWand : Wand {

    public NewbieProjectile projectile;

    public void FixedUpdate()
    {
        timeSincePrevUse += Time.deltaTime;
    }

    public override bool Cast(bool useButtonDown, bool useButtonHold, bool useButtonUp) 
    {
        if (useButtonDown && timeSincePrevUse > castCooldown)
        {
            Vector2 direction = transform.up;
            NewbieProjectile p = Instantiate(projectile, transform.position, Quaternion.identity) as NewbieProjectile;
            p.GetShot(direction, baseDamage, 0);
            Destroy(p, 3f);
            timeSincePrevUse = 0;
            return true;
        }
        return false;
    }
}
