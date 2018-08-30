using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixBullet : Projectile
{
    private static readonly float loopTime = 0.5f;
    private static readonly float multiplier = 2 * Mathf.PI / loopTime;
    private static readonly float width = 5;

    private float currentLoopTime = 0;
    private float currentSlope;

    protected override void UpdateMovement()
    {
        currentLoopTime += Time.deltaTime;
        currentLoopTime %= loopTime;

        currentSlope = width * multiplier * Mathf.Cos(multiplier * currentLoopTime);
        
        Vector2 slopeVector = new Vector2(currentLoopTime, currentSlope);
        
        float angleCos = Vector2.Dot(slopeVector, Vector2.up) / (slopeVector.magnitude * Vector2.up.magnitude);
        float angleSin = Mathf.Sin(Mathf.Acos(angleCos));

        Debug.Log("Angle: " + Mathf.Acos(angleCos));

        Vector2 directionVector = new Vector2(direction.x * angleCos + direction.y * angleSin,
                                              direction.y * angleCos - direction.x * angleSin).normalized;

        //Debug.Log(directionVector);

        rigidbody.velocity = directionVector * travelSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.LookRotation(directionVector, Vector3.forward);
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }
}
