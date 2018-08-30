using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletPattern : MonoBehaviour {

    public Wand origin;
    protected List<Projectile> currentShot;
    
    public abstract void Create();
}
