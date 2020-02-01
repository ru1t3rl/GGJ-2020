using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool freeze_x = false, freeze_y = false;
    public Vector2 sensetivity = new Vector2(5, 5);
    private Cursor mouse;

    private void Start()
    {
        mouse = new Cursor();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 rotation = Vector2.zero;

        if (!freeze_y)
        {
            //rotation.x = (Screen.height/2 - mousePosition.y) * sensetivity.x * Mathf.Deg2Rad;
            rotation.x = Input.GetAxis("Mouse X") * sensetivity.x * Mathf.Deg2Rad;
        }
        if (!freeze_x)
        {
            //rotation.y = (Screen.width/2 - mousePosition.x) * sensetivity.y * Mathf.Deg2Rad;
            rotation.y = Input.GetAxis("Mouse Y") * sensetivity.y * Mathf.Deg2Rad;
        }

        transform.Rotate(Vector3.up, rotation.x*10f);
        transform.Rotate(Vector3.right, -rotation.y * 10f);

        mousePosition = new Vector2(Screen.width/2, Screen.height/2);
    }
}
