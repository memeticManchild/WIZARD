using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixPattern : BulletPattern {

    public HelixBullet helixBullet;

    public override void Create()
    {
        HelixBullet h = Instantiate(helixBullet, transform.position, Quaternion.identity) as HelixBullet;
        h.GetShot(origin.transform.forward, origin.baseDamage, 0);
        Destroy(h, 3);
    }
}
