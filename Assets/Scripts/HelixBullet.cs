using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixBullet : Projectile
{
    public float loopTime = 3f;
    public float width = 2f;

    private float multiplier;

    private void Awake()
    {
        multiplier = 2 * Mathf.PI / loopTime;
    }

    private float currentLoopTime = 0;
    private float currentSlope;

    protected override void UpdateMovement()
    {
        currentLoopTime += Time.fixedDeltaTime;

        currentSlope = multiplier * Mathf.Cos(multiplier * currentLoopTime);
        
        Vector2 slopeVector = new Vector2(travelSpeed/width, currentSlope * width);

        float angleCos = Vector2.Dot(slopeVector, Vector2.up) / (slopeVector.magnitude * Vector2.up.magnitude);
        float angleSin = Mathf.Sin(Mathf.Acos(angleCos));

        Vector2 currentDirection = new Vector2(-direction.y, direction.x);

        currentDirection = new Vector2(currentDirection.x * angleCos + currentDirection.y * angleSin,
                                       currentDirection.y * angleCos - currentDirection.x * angleSin).normalized;

        rigidbody.velocity = currentDirection * travelSpeed * Time.fixedDeltaTime;
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
