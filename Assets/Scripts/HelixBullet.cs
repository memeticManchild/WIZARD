using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixBullet : Projectile
{
    private static readonly float loopTime = 1f;
    private static readonly float multiplier = 2 * Mathf.PI / loopTime;
    private static readonly float width = 5f;

    private float currentLoopTime = 0;
    private float currentSlope;

    protected override void UpdateMovement()
    {
        currentLoopTime += Time.deltaTime;
        currentLoopTime %= loopTime;

        currentSlope = multiplier * Mathf.Cos(multiplier * currentLoopTime);
        
        Vector2 slopeVector = new Vector2(currentLoopTime, currentSlope);

        float angleCos = Vector2.Dot(slopeVector, Vector2.up) / (slopeVector.magnitude * Vector2.up.magnitude);
        float angleSin = Mathf.Sin(Mathf.Acos(angleCos));

        Vector2 currentDirection = new Vector2(-direction.y, direction.x);

        currentDirection = new Vector2(currentDirection.x * angleCos + currentDirection.y * angleSin,
                                       currentDirection.y * angleCos - currentDirection.x * angleSin).normalized;

        currentDirection *= width;

        rigidbody.velocity = (currentDirection + direction) * travelSpeed * Time.deltaTime;
        float rotationAngle = Mathf.Atan2(currentDirection.x, -currentDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

    public void SetCurrentLoopTime(float timeIndex) // From 0 to 1
    {
        currentLoopTime = timeIndex *= loopTime;
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }
}
