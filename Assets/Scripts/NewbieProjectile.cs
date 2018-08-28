using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieProjectile : Projectile {

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    protected override void UpdateMovement()
    {
        rigidbody.velocity = direction * travelSpeed * Time.deltaTime;
    }
}
