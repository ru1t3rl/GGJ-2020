using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    CharacterController characterController;
    public new Camera camera;

    [HideInInspector] public float playerSpeed, accelerationFactor = 0.3f, frictionFactor = 0.1f, speedcap = 10.0f, lowSpeedThreshold = 0.05f;
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
        {

        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

        rotationDirection = new Vector3(0.0f, Input.GetAxis("Mouse X") * rotationSpeed, 0.0f);
        characterController.transform.Rotate(rotationDirection);

        #endregion
    }
}
