using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public new Rigidbody2D rigidbody;
    public new CircleCollider2D collider;
    public float travelSpeed;
    public int damage;
    private Vector2 direction;

    public void getShot(Vector2 direction, int wandDamage, float wandTravelSpeed)
    {
        this.direction = direction.normalized;
        damage += wandDamage;
        travelSpeed += wandTravelSpeed;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = direction * travelSpeed * Time.deltaTime;
    }
}
