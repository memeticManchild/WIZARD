using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletPattern : MonoBehaviour {

    public Projectile projectile;

    public abstract void Create(WandHandler origin);

}
