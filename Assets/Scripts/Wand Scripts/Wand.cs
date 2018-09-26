using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wand", menuName = "Wand")]
public class Wand : ScriptableObject
{
    public int baseDamage; // the base damage the wand will add to the projectile it shoots
    public float castCooldown; // interval between each shot in seconds
    public BulletPattern pattern;
    public Sprite sprite;
}
