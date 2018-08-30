using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbiePattern : BulletPattern
{
    public NewbieProjectile newbieProjectile;

    public override void Create(WandHandler origin)
    {
        NewbieProjectile n = Instantiate(newbieProjectile, origin.transform.position, Quaternion.identity) as NewbieProjectile;
        n.GetShot(origin.transform.up, origin.wand.baseDamage, 0);
        Destroy(n, 3);
    }
}
