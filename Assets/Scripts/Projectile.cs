using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    public new Rigidbody2D rigidbody;
    public new Collider2D collider;
    public float travelSpeed;
    public int damage;
    protected Vector2 direction;

    public virtual void GetShot(Vector2 direction, int wandDamage, float wandTravelSpeed)
    {
        this.direction = direction.normalized;
        damage += wandDamage;
        travelSpeed += wandTravelSpeed;
    }

    protected abstract void UpdateMovement();

}
