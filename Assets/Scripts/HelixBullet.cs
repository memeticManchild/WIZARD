using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixBullet : Projectile
{
    private static readonly float loopTime = 0.5f;
    private static readonly float multiplier = 2 * Mathf.PI / loopTime;

    private float currentLoopTime = 0;
    private float currentSlope;

    protected override void UpdateMovement()
    {
        currentLoopTime += Time.deltaTime;
        currentLoopTime %= loopTime;

        currentSlope = multiplier * Mathf.Cos(multiplier * currentLoopTime);

        Vector2 slopeVector = new Vector2(currentLoopTime, currentSlope);

        float angleCos = Vector2.Dot(direction, slopeVector);
        float angleSin = Mathf.Sin(Mathf.Acos(angleCos));

        Vector2 directionVector = new Vector2(slopeVector.x * angleCos + slopeVector.y * angleSin,
                                              slopeVector.y * angleCos - slopeVector.x * angleSin);

        rigidbody.velocity = directionVector * travelSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(directionVector, Vector3.forward);
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }
}
