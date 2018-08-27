using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float speed;
    public Wand wand;

    private Rigidbody2D rigid2d;
    private Transform legs;
    private float maxSpeed;

    private void Awake()
    {
        legs = transform.Find("Legs");
        rigid2d = (Rigidbody2D)transform.GetComponent("Rigidbody2D");
        maxSpeed = speed / rigid2d.drag; //The maximum speed of walking without any modifiers
    }

    private void FixedUpdate()
    {
        rotate_body();

        //Movement
        float speedRatio = rigid2d.velocity.magnitude / maxSpeed; //The ratio between the current speed and maximum speed
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool isClick = Input.GetMouseButtonDown(0);

        //The more you look away from where you are headed to, the slower you move
        //The speed ratio with the angle of looking relative to leg movement considered
        float legsAndSightAngle = Mathf.Abs((legs.eulerAngles - transform.eulerAngles).z);
        if (legsAndSightAngle > 180) legsAndSightAngle = 360 - legsAndSightAngle; //Correct the axis to consider the smaller angle
        legsAndSightAngle = Mathf.Clamp(legsAndSightAngle, 0, 90);

        float sightVsWalkingDirectionWeight = (90 - legsAndSightAngle) / 90 * speedRatio; 

        Vector2 movementForce = new Vector2(horizontal, vertical);

        if (movementForce.magnitude > 1) //Correct it so running diagonally doesn't yield more speed
            movementForce *= 1 / movementForce.magnitude;

        movementForce *= speed * (sightVsWalkingDirectionWeight / 4f + 0.75f);

        rigid2d.AddForce(movementForce);

        //Wand manipulation
        if (isClick)
            wand.cast();

        //Animation
        transform.GetComponent<Animator>().SetFloat("Speed", speedRatio);
        transform.GetComponent<Animator>().SetFloat("ArmSpeed", sightVsWalkingDirectionWeight);
    }

    private void rotate_body()
    {
        //Makes the player face the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion toMouseRotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = toMouseRotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rigid2d.angularVelocity = 0;

        //Makes the legs of the player face wherever the player is moving
        Quaternion toVelocityRotation;
        if (rigid2d.velocity.magnitude > 0.01)
        {
            //find the angle of rotation with the inverse tangent, then adjust it so the forward angle (0) is the negative direction of the y axis
            float angle = Mathf.Atan2(rigid2d.velocity.y, rigid2d.velocity.x) * Mathf.Rad2Deg - 90;
            toVelocityRotation = Quaternion.Euler(0, 0, angle);
        }
        else
            toVelocityRotation = transform.rotation;

        legs.rotation = toVelocityRotation;
        legs.eulerAngles = new Vector3(0, 0, legs.eulerAngles.z);
    }


}
