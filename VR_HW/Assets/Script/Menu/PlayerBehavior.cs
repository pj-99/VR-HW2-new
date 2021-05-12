using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    //button values are 0 for left button, 1 for right button, 2 for the middle button

    void Update()
    {
        if (Mouse.current.rightButton.isPressed && this.name == "Camera")
        {
            currentRotation.x += Mouse.current.delta.x.ReadValue() * sensitivity;
            currentRotation.y -= Mouse.current.delta.y.ReadValue() * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            if (Mouse.current.leftButton.isPressed)
                Cursor.lockState = CursorLockMode.Locked;
        }
        else if(Keyboard.current[Key.LeftShift].isPressed && this.name == "LeftHand")
        {
            if (Mouse.current.delta.x.ReadValue() > 0 && !Mouse.current.middleButton.isPressed)
            {

                transform.position += new Vector3(Mouse.current.delta.x.ReadUnprocessedValue() * Time.deltaTime * sensitivity,
                                           Mouse.current.delta.y.ReadUnprocessedValue() * Time.deltaTime * sensitivity, 0.0f);
            }

            else if (Mouse.current.delta.x.ReadValue() < 0 && !Mouse.current.middleButton.isPressed)
            {
                transform.position += new Vector3(Mouse.current.delta.x.ReadUnprocessedValue() * Time.deltaTime * sensitivity,
                                           Mouse.current.delta.y.ReadUnprocessedValue() * Time.deltaTime * sensitivity, 0.0f);
            }


            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                transform.position += new Vector3(0.0f, 0.0f, 0.5f);
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                transform.position += new Vector3(0.0f, 0.0f, -0.5f);
            }

            else if(Input.GetMouseButton(2))
            {
                currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
                currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
                currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
                transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
                if (Input.GetMouseButtonDown(0))
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (Input.GetKey(KeyCode.Space) && this.name == "RightHand")
        {
            if (Input.GetAxis("Mouse X") > 0 && !Input.GetMouseButton(2))
            {
                transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity,
                                           Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity, 0.0f);
            }

            else if (Input.GetAxis("Mouse X") < 0 && !Input.GetMouseButton(2))
            {
                transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity,
                                           Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity, 0.0f);
            }

            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                transform.position += new Vector3(0.0f, 0.0f, 0.5f);
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                transform.position += new Vector3(0.0f, 0.0f, -0.5f);
            }

            else if (Input.GetMouseButton(2))
            {
                currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
                currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
                currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
                transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
                if (Input.GetMouseButtonDown(0))
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
