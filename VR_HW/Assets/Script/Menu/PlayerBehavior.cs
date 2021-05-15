using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private Transform LeftHand = null;
    [SerializeField] private Transform RightHand = null;

    //button values are 0 for left button, 1 for right button, 2 for the middle button

    private Vector2 Look_axis, mouse_scroll;


    private Controls controls;

    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    void Start() {
        Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        Controls.Player.Hand_forward.performed += ctx => Scrolling(ctx.ReadValue<Vector2>());
        Controls.Player.Hand_forward.canceled += ctx => N_Scrolling();
    }

    private void Look(Vector2 lookAxis) => Look_axis = lookAxis;

    private void Scrolling(Vector2 scroll) => mouse_scroll = scroll;

    private void N_Scrolling() => mouse_scroll = Vector2.zero;

    void Update()
    {
        float deltaTime = Time.deltaTime;
        if (Mouse.current.rightButton.isPressed)
        {
            currentRotation.x += Mouse.current.delta.ReadValue().x * sensitivity;
            currentRotation.y -= Mouse.current.delta.ReadValue().y * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            cameraTransform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            if (Mouse.current.middleButton.isPressed)
            {
                currentRotation.x += Mouse.current.delta.ReadValue().x * sensitivity;
                currentRotation.y -= Mouse.current.delta.ReadValue().y * sensitivity;
                currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
                LeftHand.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            }
            else if (mouse_scroll != Vector2.zero && mouse_scroll.y > 0)
                LeftHand.position += transform.forward * deltaTime * 0.5f;
            else if (mouse_scroll != Vector2.zero && mouse_scroll.y < 0)
                LeftHand.position -= transform.forward * deltaTime * 0.5f;
            else if (!Mouse.current.rightButton.isPressed)
            {
                LeftHand.position += new Vector3(0.0f, Mouse.current.delta.ReadValue().y * deltaTime * sensitivity, 0.0f);
                LeftHand.position += transform.right * Mouse.current.delta.ReadValue().x * deltaTime * sensitivity;
            }
        }

        if (Keyboard.current.spaceKey.isPressed)
        {
            if (Mouse.current.middleButton.isPressed)
            {
                currentRotation.x += Mouse.current.delta.ReadValue().x * sensitivity;
                currentRotation.y -= Mouse.current.delta.ReadValue().y * sensitivity;
                currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
                RightHand.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            }
            else if (mouse_scroll != Vector2.zero && mouse_scroll.y > 0)
                RightHand.position += transform.forward * deltaTime * 0.5f;
            else if (mouse_scroll != Vector2.zero && mouse_scroll.y < 0)
                RightHand.position -= transform.forward * deltaTime * 0.5f;
            else if (!Mouse.current.rightButton.isPressed)
            {
                RightHand.position += new Vector3(0.0f, Mouse.current.delta.ReadValue().y * deltaTime * sensitivity, 0.0f);
                RightHand.position += transform.right * Mouse.current.delta.ReadValue().x * deltaTime * sensitivity;
            }
        }
    }
}
