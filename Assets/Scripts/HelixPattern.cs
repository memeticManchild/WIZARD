using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixPattern : BulletPattern {

    public HelixBullet helixBullet;

    public override void Create(WandHandler origin)
    {
        HelixBullet h = Instantiate(helixBullet, origin.transform.position, Quaternion.identity) as HelixBullet;
        h.GetShot(origin.transform.up, origin.wand.baseDamage, 0);
        Destroy(h, 3);
    }
}
