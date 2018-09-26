using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public new Camera camera;
    public PlayerBehaviour player;
    public float viewReachScale; //the multiplier for how far the player can see
    public float zAxis; //how far away the camera is from the screen

	void LateUpdate () {
        //move the camera towards the mouse (without breaching the screen)

        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = player.transform.position;

        //translate the position from a [0;0] to [1;1] vector, to a [-1;-1] to [1;1] for negative values
        mousePosition = camera.ScreenToViewportPoint(mousePosition) * 2;
        mousePosition[0] -= 1; mousePosition[1] -= 1;

        //set the position so it extends from the player
        transform.position = playerPosition + (new Vector3(mousePosition[0], mousePosition[1], zAxis) * viewReachScale);



	}
}
