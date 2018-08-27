using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wand : MonoBehaviour
{
    public GameObject projectile; // what projectile will be generated
    public int damage; // the base damage the wand will add to the projectile it shoots
    public float castRate; // interval between each shot in seconds
    public PlayerBehaviour owner; // the player who is using it

    public void cast()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - owner.transform.position;
        GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
        p.GetComponent<Projectile>().SetDirection(direction);
    }
}
