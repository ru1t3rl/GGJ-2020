﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    CharacterController characterController;
    public new Camera camera;

    const float hoverHeight = 1;

    public float rotationSpeed = 1, accelerationFactor = 10.0f, speedNullifyThreshold = 0.001f;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public Vector3 rotationDirection = Vector3.zero;
    [HideInInspector] public Vector3 playerSpeed = Vector3.zero;
    [HideInInspector] public Vector3 acceleration = Vector3.zero;

    public float jumpingPower = 20;
    private bool isJumping = false, isFalling = false;

    public float maxSpeed = 20;
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
            acceleration.z = accelerationFactor;
        else if (Input.GetKey(KeyCode.S))
            acceleration.z = -accelerationFactor;
        else
        {
            acceleration.z = 0;
            playerSpeed.z *= 0.95f; // Dirty
        }

        // Lateral Movement / Strafing
        if (Input.GetKey(KeyCode.D))
            acceleration.x = accelerationFactor;
        else if (Input.GetKey(KeyCode.A))
            acceleration.x = -accelerationFactor;
        else
        {
            acceleration.x = 0;
            playerSpeed.x *= 0.95f; // Dirty
        }

        // Jump Mechanic
        if (!isJumping && Input.GetKeyDown(KeyCode.F))
        {
            playerSpeed.y = jumpingPower;
            isJumping = true;
        }

        if (playerSpeed.y < 0)
            isFalling = true;
        else
            isFalling = false;

        if (isJumping)
        {
            playerSpeed.y -= 0.5f;
            
            if (characterController.transform.position.y <= hoverHeight && isFalling)
            {
                playerSpeed.y = 0;
                acceleration.y = 0;
                isJumping = false;
            }            
        }

        playerSpeed += acceleration;

        if (playerSpeed.magnitude > maxSpeed)
            playerSpeed = playerSpeed.normalized * maxSpeed;

        if (playerSpeed.x < speedNullifyThreshold && playerSpeed.x > -speedNullifyThreshold)
            playerSpeed.x = 0;
        if (playerSpeed.z < speedNullifyThreshold && playerSpeed.z > -speedNullifyThreshold)
            playerSpeed.z = 0;


        moveDirection = playerSpeed;
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

        rotationDirection = new Vector3(0.0f , Input.GetAxis("Mouse X") * rotationSpeed, 0.0f);
        characterController.transform.Rotate(rotationDirection);
    }
}