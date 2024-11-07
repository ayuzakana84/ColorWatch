using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    public GameObject[] lifeArray = new GameObject[3];
    public int life;

    private Rigidbody rb;

    private float nowStamina;

    private bool moving;
    private bool looking;
    private bool running;
    private bool dameged = false;

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

        life = 3;
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

                //��������������
                if (moveVector.x < 0 && playermove.x > 0 || moveVector.x > 0 && playermove.x < 0)
                {
                    rb.velocity = Vector3.zero;
                }
                if (moveVector.y < 0 && playermove.z > 0 || moveVector.y > 0 && playermove.z < 0)
                {
                    rb.velocity = Vector3.zero;
                }

                rb.AddRelativeForce(playermove);

                nowStamina -= 0.1f;
                staminaSlider.value = nowStamina;

                if (rb.velocity.magnitude > limitDash)
                {
                    rb.velocity = rb.velocity.normalized * limitDash;
                }

                //�X�^�~�i���؂ꂽ��
                if (nowStamina <= 0)
                {
                    running = false;
                }
            }
            else //�ʏ펞
            {
                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed;

                //��������������
                if (moveVector.x < 0 && playermove.x > 0 || moveVector.x > 0 && playermove.x < 0)
                {
                    rb.velocity = Vector3.zero;
                }
                if (moveVector.y < 0 && playermove.y > 0 || moveVector.y > 0 && playermove.y < 0)
                {
                    rb.velocity = Vector3.zero;
                }

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

            Camera.main.transform.localRotation = Quaternion.Euler(playerlook.x, 0, 0);
            transform.rotation = Quaternion.Euler(0,playerlook.y,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !dameged)
        {
            playerspeed = 10;
            dameged = true;
            Invoke(nameof(Invincible), 3.0f); //3�b��ɖ��G��Ԃ�߂�
            Invoke(nameof(SpeedUp), 3.0f); //3�b���playerspeed��5�ɖ߂�
            life--;
            lifeArray[life].SetActive(false);
            if (life <= 0)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    void SpeedUp()
    {
        playerspeed = 5;
    }

    void Invincible()
    {
        dameged = false;
        Debug.Log("���G���ԏI��");
    }
}
