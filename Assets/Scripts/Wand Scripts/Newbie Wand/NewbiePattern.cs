using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbiePattern : BulletPattern
{
    public override void Create(WandHandler origin)
    {
        NewbieProjectile n = Instantiate(projectile, origin.transform.position, Quaternion.identity) as NewbieProjectile;
        n.GetShot(origin.transform.up, origin.wand.baseDamage, 0);
        Destroy(n, 3);
    }
}
