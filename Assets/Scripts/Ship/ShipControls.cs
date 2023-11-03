using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControls : MonoBehaviour
{
    [SerializeField] [Range (1.0f, 100.0f)] private float fMaxSpeed;
    [SerializeField] [Range (1.0f, 100.0f)] private float fAccelerationAmount;
    [SerializeField] [Range (0.0f, 0.01f)] private float fDragAmount;
    private bool bAccelerating = false;

    [SerializeField] private LaserPool laserPool;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;
    Vector3 velocityVector = Vector3.zero;
    

    void Update()
    {
        MoveShip();
        LookAtMousePointer();
    }

    public void MoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            bAccelerating = true;
        }

        if (context.canceled)
        {
            bAccelerating = false;
        }
    }

    public void LookAction(InputAction.CallbackContext context)
    {
        Vector2 lookVector = context.ReadValue<Vector2>();
        transform.forward = lookVector;

    }

    public void FireAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            laserPool.FireLaser(transform.position, transform.rotation);
        }
    }

    void LookAtMousePointer()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));

        transform.LookAt(mousePosition);
    }

    void MoveShip()
    {
        if (velocityVector.magnitude > fMaxSpeed)
        {
            velocityVector = velocityVector.normalized * fMaxSpeed;
        }
        
        transform.position += velocityVector * Time.deltaTime;
        
        if (bAccelerating)
        {
            velocityVector += transform.forward * fAccelerationAmount * Time.deltaTime;
        }
        else
        {
            velocityVector -= velocityVector * fDragAmount;
        }
    }
}
