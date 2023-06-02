using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
public class PlayerMovement : MonoBehaviour
{
    PlayerController player;
    public CharacterController characterController;

    Camera cam;

    [HideInInspector]
    public float speed;
    Vector2 directionVector;
    float gravity = -9.81f;
    public float gravityMultiplier = 3f;

    public float turnSpeed;
    float turnSmoothVelocity;

    Vector3 lastPos;

    bool canMove;
    bool canRotate;

    /*
    public override void OnStartAuthority()
    {
        enabled = true;
    }
    */

    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        cam = Camera.main;
        characterController.Move(new Vector3(0,0,0) * speed * Time.deltaTime);
        lastPos = transform.position;
        canMove = true;
        canRotate = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        directionVector = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    //[ClientCallback]
    void Update()
    {
        Vector3 movement = new Vector3(directionVector.x, 0, directionVector.y).normalized;

        if (transform.position != lastPos && movement != Vector3.zero)
        {
            player.anim.SetBool("isWalking", true);
        }
        else
        {
            player.anim.SetBool("isWalking", false);
        }

        lastPos = transform.position;

        if (!canMove && !canRotate)
        {
            return;
        }

        movement = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * movement;
        
        ApplyRotation(movement);
        ApplyMovement(movement);



    }

    void ApplyMovement(Vector3 movement)
    {
        if (canMove)
        {
            if (characterController.isGrounded)
            {
                movement.y = -1f;
            }
            else
            {
                movement.y = gravity * gravityMultiplier * Time.deltaTime;
            }

            //transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
            characterController.Move(movement * speed * Time.deltaTime);
        }
    }

    void ApplyRotation(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            if (canRotate)
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    public void LockAll()
    {
        canMove = false;
        canRotate = false;
    }

    public void UnlockAll()
    {
        canMove = true;
        canRotate = true;
    }

    public void LockMov()
    {
        canMove = false;
    }

    public void UnlockMov()
    {
        canMove = true;
    }
    
    public void LockRot()
    {
        canRotate = false;
    }

    public void UnlockRot()
    {
        canRotate = true;
    }
}
