using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wand : MonoBehaviour
{

    public bool autoCast; // if holding automaticly casts
    public float damage; //how much damage it deals
    public LayerMask dontHit; // what it doesnt hit
    public float castRate; // how fast is the gun shooting
    public float range; //how far the projectile goes
    public float speed; //how fast is the bullet
    private Rigidbody2D proj1;

    Transform castPoint;

    void Awake()
    {
        castPoint = transform.Find("castPoint");
        proj1 = (Rigidbody2D)transform.GetComponent("proj1");
    }


    void cast()
    {
        Rigidbody2D projectile;
        projectile = Instantiate(proj1, castPoint.position, castPoint.rotation);
        projectile = (Rigidbody2D)transform.GetComponent("Rigidbody2D");
        projectile.AddForce(projectile.transform.forward * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update ()
    {


        if (autoCast == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                cast();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > castRate)
            {
                castRate = Time.time + 1 / castRate;
                cast();
            }
        }
        
    }


}
