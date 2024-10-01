using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerspeed = 3;
    [SerializeField] private float lookspeed = 0.8f;
    [SerializeField] private float maxAngleX = 80; //�����������E�̊p�x
    [SerializeField] private float minAngleX = -90; //����������E�̊p�x

    private Rigidbody rb;

    private bool moving;
    private bool looking;

    private Vector2 moveVector;
    private Vector2 lookVector;

    private Vector3 playermove;
    private Vector3 playerlook;

    public Vector3 playerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        if (context.started)
        {
            moving = true;
        }
        else if(context.canceled)
        {
            moving = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();

        if (context.started)
        {
            looking = true;
        }
        else if (context.canceled)
        {
            looking = false;
        }
    }

    void Update()
    {
        //�v���C���[�̈ړ�
        if (moving)
        {
            playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed;
            rb.AddRelativeForce(playermove);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //�v���C���[�̎��_
        if (looking)
        {
            //�㉺�̎��_�ړ�
            if (playerlook.x > maxAngleX)
            {
                if (lookVector.y > 0)
                {
                    playerlook.x -= lookVector.y * lookspeed;
                }
            }
            else if (playerlook.x < minAngleX)
            {
                if (lookVector.y < 0)
                {
                    playerlook.x -= lookVector.y * lookspeed;
                }
            }
            else
            {
                playerlook.x -= lookVector.y * lookspeed;
            }

            //Camera.main.transform.localRotation = Quaternion.Euler(playerlook.x, 0, 0);

            //���E�̎��_�ړ�
            if (playerlook.y >= 360)
            {
                playerlook.y = playerlook.y - 360;
            }
            else if (playerlook.y < 0)
            {
                playerlook.y = 360 - playerlook.y;
            }

            playerlook.y += lookVector.x * lookspeed;

            //Camera.main.transform.localRotation = Quaternion.Euler(playerlook);
            transform.rotation = Quaternion.Euler(playerlook);
            

        }

        playerPosition = Vector3.forward;
        //Debug.Log(playerPosition);
    }
}
