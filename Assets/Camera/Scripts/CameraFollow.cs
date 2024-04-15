using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Camera follow variables
    public Transform attachObject;
    private float cameraSmoothingSpeed = 0.1f;

    // Camera Orthographic Size variables
    private float zoomSensitivity = 2.0f;

    private Camera mainCamera;


    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            // Adjust orthographic size based on scroll wheel input
            mainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 4.0f, 12.0f);
        }
    }


    // FixedUpdate() for consistent updates by updating on interval instead of every frame
    // Creates smooth camera movement that follows the player
    void FixedUpdate()
    {
        // Sets point between start and end point to create smooth movement
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, attachObject.position, cameraSmoothingSpeed);
        transform.position = smoothedPosition;

        transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }
}
