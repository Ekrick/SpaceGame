using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    [SerializeField] [Range (1.0f, 100.0f)] private float fMaxSpeed;
    [SerializeField] [Range (1.0f, 100.0f)] private float fAccelerationAmount;
    [SerializeField] [Range (0.0f, 0.01f)] private float fDragAmount;

    [SerializeField] private LaserBeam laserPrefab;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;
    private bool bAccelerating = false;
    Vector3 velocityVector = Vector3.zero;
    
    
    void Start()
    {
        
    }

    void Update()
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
            Instantiate(laserPrefab, transform.position, transform.rotation);
        }
    }

    void LookAtMousePointer()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));

        transform.LookAt(mousePosition);
    }


}