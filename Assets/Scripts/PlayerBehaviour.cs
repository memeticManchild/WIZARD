using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float speed;
    public WandHandler wandHandler;
    public Transform legs;
    public Rigidbody2D rigid2d;
    public Animator animator;

    private float maxSpeed;
    private bool isShooting;
    private bool isReadyToShoot;

    private void Awake()
    {
        maxSpeed = speed / rigid2d.drag; //The maximum speed of walking without any modifiers
        isShooting = false;
        isReadyToShoot = false;
    }

    private void FixedUpdate()
    {
        // Basic Inputs 
        float speedRatio = rigid2d.velocity.magnitude / maxSpeed; //The ratio between the current speed and maximum speed
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool isMouse1Down = Input.GetMouseButtonDown(0);
        bool isMouse1Held = Input.GetMouseButton(0);
        bool isMouse1Up = Input.GetMouseButtonUp(0);

        RotateBody();

        MoveBody(speedRatio, vertical, horizontal);

        if (wandHandler.wand != null)
            HandleWand(isMouse1Down, isMouse1Held, isMouse1Up);
    }

    private void RotateBody()
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

    private void MoveBody(float speedRatio, float verticalInput, float horizontalInput)
    {
        //The more you look away from where you are headed to, the slower you move
        //The speed ratio with the angle of looking relative to leg movement considered
        float legsAndSightAngle = Mathf.Abs((legs.eulerAngles - transform.eulerAngles).z);
        if (legsAndSightAngle > 180) legsAndSightAngle = 360 - legsAndSightAngle; //Correct the axis to consider the smaller angle
        legsAndSightAngle = Mathf.Clamp(legsAndSightAngle, 0, 90);

        float sightVsWalkingDirectionWeight = (90 - legsAndSightAngle) / 90 * speedRatio;

        Vector2 movementForce = new Vector2(horizontalInput, verticalInput);

        if (movementForce.magnitude > 1) //Correct it so running diagonally doesn't yield more speed
            movementForce *= 1 / movementForce.magnitude;

        movementForce *= speed * (sightVsWalkingDirectionWeight / 4f + 0.75f);

        if (isShooting)
            movementForce *= 0.75f;

        rigid2d.AddForce(movementForce);

        //Update movement animation parameters
        animator.SetFloat("ArmSpeed", sightVsWalkingDirectionWeight);
        animator.SetFloat("Speed", speedRatio);
    }

    private void HandleWand(bool isMouseDown, bool isMouseHeld, bool isMouseUp)
    {
        // Wand inputs
        if (Input.GetKeyDown("q"))
            isShooting = !isShooting;
        if (!isShooting)
            isReadyToShoot = false;

        bool didCast = false;

        if (isShooting && isReadyToShoot)
            didCast = wandHandler.Cast(isMouseDown, isMouseHeld, isMouseUp);

        // Wand animations
        animator.SetBool("IsShooting", isShooting);
        animator.SetBool("DidSingleShot", didCast);
    }

    public void ReadyToShoot() // used in animation event
    {
        isReadyToShoot = true;
    }
}
