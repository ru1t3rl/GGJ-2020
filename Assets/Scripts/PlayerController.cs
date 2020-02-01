using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    CharacterController characterController;
    public new Camera camera;

    const float hoverHeight = 1;

    public float rotationSpeed = 1, accelerationFactor = 10.0f, speedNullifyThreshold = 0.001f;
    [HideInInspector] public float playerSpeedParallel, playerSpeedHeight, playerSpeedLateral, accelerationX, accelerationY, accelerationZ;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public Vector3 rotationDirection = Vector3.zero;

    public float jumpingPower = 20;
    private bool isJumping = false, isFalling = false;

    public float maxSpeed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {
        HoverMovement();
    }

    void HoverMovement()
    {
        // Parallel Movement / Straight
        if (Input.GetKey(KeyCode.W))
            accelerationX = accelerationFactor;
        else if (Input.GetKey(KeyCode.S))
            accelerationX = -accelerationFactor;
        else
        {
            accelerationX = 0;
            playerSpeedParallel *= 0.95f; // Dirty
        }

        // Lateral Movement / Strafing
        if (Input.GetKey(KeyCode.D))
            accelerationZ = accelerationFactor;
        else if (Input.GetKey(KeyCode.A))
            accelerationZ = -accelerationFactor;
        else
        {
            accelerationZ = 0;
            playerSpeedLateral *= 0.95f; // Dirty
        }

        // Jump Mechanic
        if (!isJumping && Input.GetKeyDown(KeyCode.F))
        {
            playerSpeedHeight = jumpingPower;
            isJumping = true;
        }

        if (playerSpeedHeight < 0)
            isFalling = true;
        else
            isFalling = false;

        if (isJumping)
        {
            playerSpeedHeight -= 0.5f;
            
            if (characterController.transform.position.y <= hoverHeight && isFalling)
            {
                playerSpeedHeight = 0;
                accelerationY = 0;
                isJumping = false;
            }            
        }        

        playerSpeedParallel += accelerationX;
        playerSpeedHeight += accelerationY;
        playerSpeedLateral += accelerationZ;

        if (playerSpeedParallel < speedNullifyThreshold && playerSpeedParallel > -speedNullifyThreshold)
            playerSpeedParallel = 0;
        if (playerSpeedLateral < speedNullifyThreshold && playerSpeedLateral > -speedNullifyThreshold)
            playerSpeedLateral = 0;


        moveDirection = new Vector3(playerSpeedLateral, playerSpeedHeight, playerSpeedParallel);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

        rotationDirection = new Vector3(0.0f , Input.GetAxis("Mouse X") * rotationSpeed, 0.0f);
        characterController.transform.Rotate(rotationDirection);
    }
}