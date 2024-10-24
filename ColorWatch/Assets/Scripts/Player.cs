using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerspeed = 3;
    [SerializeField] private float dashspeed = 1.3f;
    [SerializeField] private float lookspeed = 0.8f;
    [SerializeField] private float maxAngleX = 80; //�����������E�̊p�x
    [SerializeField] private float minAngleX = -90; //����������E�̊p�x
    [SerializeField] private float maxStamina = 100f; //�X�^�~�i�̍ő�l
    [SerializeField] private float limitMove = 10; //���������̏��
    [SerializeField] private float limitDash = 15; //���鑬���̏��

    private Rigidbody rb;

    private float nowStamina;

    private bool moving;
    private bool looking;
    private bool running;

    private Vector2 moveVector;
    private Vector2 lookVector;

    private Vector3 playermove;
    private Vector3 playerlook;

    public GameObject staminaGauge;
    private Slider staminaSlider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        staminaSlider = staminaGauge.GetComponent<Slider>();
        staminaSlider.maxValue = maxStamina;
        nowStamina = maxStamina;
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

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            running = true;
        }
        else if (context.canceled)
        {
            running = false;
        }

    }

    void Update()
    {
        //�v���C���[�̈ړ�
        if (moving)
        {
            if (running) //�_�b�V����
            {
                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed * dashspeed;
                rb.AddRelativeForce(playermove);

                nowStamina -= 0.1f;
                staminaSlider.value = nowStamina;

                if (rb.velocity.magnitude > limitDash)
                {
                    rb.velocity = rb.velocity.normalized * limitDash;
                }
            }
            else //�ʏ펞
            {
                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed;
                rb.AddRelativeForce(playermove);

                //�����������Ȃ��悤�ɐ���
                if (rb.velocity.magnitude > limitMove)
                {
                    rb.velocity = rb.velocity.normalized * limitMove;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //�X�^�~�i�̉�
        if (nowStamina < staminaSlider.maxValue && !running)
        {
            nowStamina += 0.15f;
            staminaSlider.value = nowStamina;
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
    }
}
