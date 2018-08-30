using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixPattern : BulletPattern {

    public HelixBullet helixBullet;

    public override void Create(WandHandler origin)
    {
        HelixBullet h1 = Instantiate(helixBullet, origin.transform.position, Quaternion.identity) as HelixBullet;
        HelixBullet h2 = Instantiate(helixBullet, origin.transform.position, Quaternion.identity) as HelixBullet;
        h1.GetShot(origin.transform.up, origin.wand.baseDamage, 0);
        h2.GetShot(origin.transform.up, origin.wand.baseDamage, 0);
        h2.SetCurrentLoopTime(0.5f);
        Destroy(h1.gameObject, 10);
        Destroy(h2.gameObject, 10);
    }
}
