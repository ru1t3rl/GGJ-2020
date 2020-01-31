using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    CharacterController characterController;
    public new Camera camera;

    [HideInInspector] public float playerSpeedParallel, playerSpeedLateral, accelerationFactor = 10.0f, frictionFactor = 0.95f, speedNullifyThreshold = 0.001f;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public float rotationSpeed = 1;
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
            playerSpeedParallel = accelerationFactor * 20;
        else if (Input.GetKey(KeyCode.S))
            playerSpeedParallel = -accelerationFactor;
        else
            playerSpeedParallel = 0;

        if (Input.GetKey(KeyCode.A))
            playerSpeedLateral = accelerationFactor;
        else if (Input.GetKey(KeyCode.D))
            playerSpeedLateral = -accelerationFactor;
        else
            playerSpeedLateral = 0;

        moveDirection = new Vector3(playerSpeedLateral, 0.0f, playerSpeedParallel);        
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

        rotationDirection = new Vector3(0.0f, Input.GetAxis("Mouse X") * rotationSpeed, 0.0f);
        characterController.transform.Rotate(rotationDirection);

        #endregion
    }
}
