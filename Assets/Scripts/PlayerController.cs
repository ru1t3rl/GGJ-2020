using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    CharacterController characterController;
    public new Camera camera;

    [HideInInspector] public float playerSpeedParallel, playerSpeedLateral, accelerationX, accelerationY;
    public float rotationSpeed = 1, accelerationFactor = 10.0f, speedNullifyThreshold = 0.001f;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public Vector3 rotationDirection = Vector3.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {
        #region Key-Based Controlls

        if (Input.GetKey(KeyCode.W))
            accelerationX = accelerationFactor;
        else if (Input.GetKey(KeyCode.S))
            accelerationX = -accelerationFactor;
        else
        {
            accelerationX = 0;
            playerSpeedParallel *= 0.95f; // Dirty
        }

        if (Input.GetKey(KeyCode.D))
            accelerationY = accelerationFactor;
        else if (Input.GetKey(KeyCode.A))
            accelerationY = -accelerationFactor;
        else
        {
            accelerationY = 0;
            playerSpeedLateral *= 0.95f; // Dirty
        }

        playerSpeedParallel += accelerationX;
        playerSpeedLateral += accelerationY;

        if (playerSpeedParallel < speedNullifyThreshold && playerSpeedParallel > -speedNullifyThreshold)
            playerSpeedParallel = 0;
        if (playerSpeedLateral < speedNullifyThreshold && playerSpeedLateral > -speedNullifyThreshold)
            playerSpeedLateral = 0;

        Debug.Log(playerSpeedParallel + " " + playerSpeedLateral);

        moveDirection = new Vector3(playerSpeedLateral, 0.0f, playerSpeedParallel);        
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

        rotationDirection = new Vector3(0.0f, Input.GetAxis("Mouse X") * rotationSpeed, 0.0f);
        characterController.transform.Rotate(rotationDirection);

        #endregion
    }
}
