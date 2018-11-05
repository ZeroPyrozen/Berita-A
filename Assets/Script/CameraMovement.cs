using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;

    public GameObject player;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera


    [SerializeField] private float offsetY = 0.75f;
    private float offsetZ = -10f;
    private float camHeight;
    private float camWidth;

    // Use this for initialization
    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
            
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(0, offsetY, offsetZ);

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        var clampedX = Mathf.Clamp(player.transform.position.x, -9.39f + camWidth, 1.17f - camWidth);
        var clampedY = Mathf.Clamp(player.transform.position.y + offset.y, 1.95f + camHeight, 7.55f - camHeight);
        transform.position = new Vector3(clampedX, clampedY, offset.z);
        
    }
}
