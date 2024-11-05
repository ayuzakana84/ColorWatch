using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BlackLightScript : MonoBehaviour
{
    public Transform judge;
    public Transform player;
    public float maxAngle = 25f;
    public bool ShyStop = false;

    private bool powerOn = false;

    [SerializeField] private float maxBattery = 100f; //�o�b�e���[�̍ő�l
    private float nowbattery;

    public GameObject batteryGauge;
    private Slider batterySlider;

    void Start()
    {
        batterySlider = batteryGauge.GetComponent<Slider>();
        batterySlider.maxValue = maxBattery;
        nowbattery = maxBattery;
    }

    public void OnLight(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            if (!powerOn)
            {
                powerOn = true;
            }
            else
            {
                powerOn = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyShy"))
        {
            ShyStop = true;
            Debug.Log("������");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyShy"))
        {
            ShyStop = false;
            Debug.Log("�o��");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(judge!=null)
        {
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                foreach(Material material in renderer.materials)
                {
                    if(material.shader.name=="Unlit/BlackLight")
                    {
                        if (powerOn) //���C�gON
                        {
                            material.SetVector("_Judge", new Vector4(judge.position.x, judge.position.y, judge.position.z, 0));

                            material.SetVector("_Position", new Vector4(player.position.x, player.position.y, player.position.z, 0));

                            material.SetFloat("_MaxAngle", maxAngle);

                            nowbattery -= 0.0001f;
                            batterySlider.value = nowbattery;
                        }
                        else //���C�gOFF
                        {
                            material.SetVector("_Judge", new Vector4(0, 0, 0, 0));

                            material.SetVector("_Position", new Vector4(0, 0, 0, 0));

                            material.SetFloat("_MaxAngle", 0);
                        }
                    }
                }
            }
        }
    }
}
