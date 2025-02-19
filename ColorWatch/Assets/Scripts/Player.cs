using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerspeed = 5;
    [SerializeField] private float dashspeed = 1.3f;
    [SerializeField] private float lookspeed = 0.8f;
    [SerializeField] private float maxAngleX = 80; //下を向く限界の角度
    [SerializeField] private float minAngleX = -90; //上を向く限界の角度
    [SerializeField] private float maxStamina = 100f; //スタミナの最大値
    [SerializeField] private float limitMove = 10; //歩く速さの上限
    [SerializeField] private float limitDash = 15; //走る速さの上限

    public GameObject[] lifeArray = new GameObject[3];
    public GameObject[] damagedlifeArray = new GameObject[3];
    public int life;

    private Rigidbody rb;

    private float nowStamina;

    public bool moving;
    public bool running;
    private bool looking;
    private bool dameged = false;
    private bool runSE = false;
    private bool walkSE = false;

    private Vector2 moveVector;
    private Vector2 lookVector;

    private Vector3 playermove;
    private Vector3 playerlook;

    //Sound
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource walkSESource;
    [SerializeField] AudioSource runSESource;

    [SerializeField] AudioClip soundHit;
    [SerializeField] AudioClip soundDamege;

    //UI
    public GameObject staminaGauge;
    private Slider staminaSlider;
    public GameObject runOnIcon;
    public GameObject runOffIcon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        staminaSlider = staminaGauge.GetComponent<Slider>();
        staminaSlider.maxValue = maxStamina;
        nowStamina = maxStamina;

        runOnIcon.SetActive(false);

        life = 3;
        for(int i=0; i<life; i++)
        {
            damagedlifeArray[i].SetActive(false);
        }
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
            runOnIcon.SetActive(true);
            runOffIcon.SetActive(false);
            running = true;
        }
        else if (context.canceled)
        {
            runOnIcon.SetActive(false);
            runOffIcon.SetActive(true);
            running = false;
        }

    }

    void Update()
    {
        //プレイヤーの移動
        if (moving)
        {
            if (running) //ダッシュ時
            {
                if (!runSE) //効果音を鳴らす
                {
                    if (walkSE)
                    {
                        walkSESource.Stop();
                        walkSE = false;
                    }

                    runSESource.Play();
                    runSE = true;
                }

                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed * dashspeed;

                //慣性を消したい
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

                //スタミナが切れたら
                if (nowStamina <= 0)
                {
                    running = false;
                }
            }
            else //通常時
            {
                if (!walkSE)
                {
                    if (runSE)
                    {
                        runSESource.Stop();
                        runSE = false;
                    }

                    walkSESource.Play();
                    walkSE = true;
                }

                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed;

                //慣性を消したい
                if (moveVector.x < 0 && playermove.x > 0 || moveVector.x > 0 && playermove.x < 0)
                {
                    rb.velocity = Vector3.zero;
                }
                if (moveVector.y < 0 && playermove.y > 0 || moveVector.y > 0 && playermove.y < 0)
                {
                    rb.velocity = Vector3.zero;
                }

                rb.AddRelativeForce(playermove);

                //加速しすぎないように制限
                if (rb.velocity.magnitude > limitMove)
                {
                    rb.velocity = rb.velocity.normalized * limitMove;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;

            runSESource.Stop();
            walkSESource.Stop();
            runSE = false;
            walkSE = false;
        }

        //スタミナの回復
        if (nowStamina < staminaSlider.maxValue && !running)
        {
            nowStamina += 0.15f;
            staminaSlider.value = nowStamina;
        }

        //プレイヤーの視点
        if (looking)
        {
            //上下の視点移動
            if (playerlook.x > maxAngleX)
            {
                if (lookVector.y > 0)
                {
                    playerlook.x -= lookVector.y * lookspeed * Time.deltaTime;
                }
            }
            else if (playerlook.x < minAngleX)
            {
                if (lookVector.y < 0)
                {
                    playerlook.x -= lookVector.y * lookspeed * Time.deltaTime;
                }
            }
            else
            {
                playerlook.x -= lookVector.y * lookspeed * Time.deltaTime;
            }

            //Camera.main.transform.localRotation = Quaternion.Euler(playerlook.x, 0, 0);

            //左右の視点移動
            if (playerlook.y >= 360)
            {
                playerlook.y = playerlook.y - 360;
            }
            else if (playerlook.y < 0)
            {
                playerlook.y = 360 - playerlook.y;
            }

            playerlook.y += lookVector.x * lookspeed * Time.deltaTime;

            Camera.main.transform.localRotation = Quaternion.Euler(playerlook.x, 0, 0);
            transform.rotation = Quaternion.Euler(0,playerlook.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //敵にぶつかった時
        if (other.CompareTag("Enemy") && !dameged)
        {
            audioSource.PlayOneShot(soundDamege); //効果音を鳴らす

            playerspeed = 10;
            dameged = true;
            Invoke(nameof(Invincible), 3.0f); //3秒後に無敵状態を戻す
            Invoke(nameof(SpeedUp), 3.0f); //3秒後にplayerspeedを5に戻す
            life--;
            lifeArray[life].SetActive(false);
            damagedlifeArray[life].SetActive(true);
            if (life <= 0)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    //物にぶつかったら効果音を鳴らす
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Floor")
        {
            audioSource.PlayOneShot(soundHit);
        }
    }

    void SpeedUp()
    {
        playerspeed = 5;
    }

    void Invincible()
    {
        dameged = false;
    }
}
