using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] ComponentsToDisable;

    Camera Scenecamera;

    void Start()
    {
        if (!isLocalPlayer)
            for (int i = 0; i < ComponentsToDisable.Length; i++)
            {
                ComponentsToDisable[i].enabled = false;
            }
        else
        {
            Scenecamera = Camera.main;
            if (Scenecamera != null)
            {
                Scenecamera.gameObject.SetActive(false);
            }
        }
            
    }
}
	