using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WandHandler : MonoBehaviour {

    public Wand wand;
    public PlayerBehaviour owner;

    protected float timeSincePrevUse = 0;

    public abstract bool Cast(bool useButtonDown, bool useButtonHold, bool useButtonUp);

    public virtual void FixedUpdate()
    {
        timeSincePrevUse += Time.deltaTime;
    }

}
