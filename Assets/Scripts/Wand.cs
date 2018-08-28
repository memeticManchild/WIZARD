using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wand : MonoBehaviour
{
    public enum CastType { SingleShot, Beam, Charged };

    public CastType castType;
    public int baseDamage; // the base damage the wand will add to the projectile it shoots
    public float castCooldown; // interval between each shot in seconds
    public PlayerBehaviour owner; // the entity that is using it

    protected float timeSincePrevUse = 0;

    // returns true if managed to cast, false if failed
    public abstract bool Cast(bool useButtonDown, bool useButtonHold, bool useButtonUp);
}
