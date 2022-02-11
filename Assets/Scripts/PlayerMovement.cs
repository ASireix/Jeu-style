using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    Camera cam;

    public float speed;
    Vector2 directionVector;

    public float turnSpeed;
    float turnSmoothVelocity;

    private void Start()
    {
        cam = Camera.main;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionVector = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(directionVector.x, 0, directionVector.y).normalized;

        movement = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * movement;
        //transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
        characterController.Move(movement * speed * Time.deltaTime);
        if (movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    
}
