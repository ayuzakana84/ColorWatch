using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LightPillarManager : MonoBehaviour
{
    [SerializeField] GameObject[] PillarPoint; //�X�|�[���ʒu������z��
    [SerializeField] GameObject[] LightPillar; //���̒�������z��
    GameObject nowPillarNormal;
    GameObject nowPillarShy;
    GameObject nowPillarBoar;
    GameObject nowPillarOctopus;

    int rnd; //����������int�^

    List<GameObject> PillarList = new List<GameObject>();

    void Start()
    {
        //�X�|�[���ʒu��z��ɓ����
        for (int i = 0; i < PillarPoint.Length; i++)
        {
            PillarList.Add(PillarPoint[i]);
        }

        //���̒��������_���Ȉʒu�ɓ����
        for (int i = 0; i < LightPillar.Length; i++)
        {
            rnd = Random.Range(0, PillarList.Count);
            LightPillar[i].transform.position = PillarList[rnd].transform.position; //�ʒu��ύX

            //Debug.Log(PillarList[rnd].name);

            if (LightPillar[i].name == "LightPillarNormal") //Normal�̒��̏ꏊ���L��
                nowPillarNormal = PillarList[rnd];
            else if (LightPillar[i].name == "LightPillarShy") //Shy�̒��̏ꏊ���L��
                nowPillarShy = PillarList[rnd];
            else if (LightPillar[i].name == "LightPillarBoar") //Boar�̒��̏ꏊ���L��
                nowPillarBoar = PillarList[rnd];
            else if (LightPillar[i].name == "LightPillarOctopus") //Octopus�̒��̏ꏊ���L��
                nowPillarOctopus = PillarList[rnd];

                PillarList.Remove(PillarList[rnd]); //�X�|�[���ʒu����폜
        }
    }

    //���̒��̈ʒu�̕ύX
    public void ChangePillarPoint(GameObject setPillar)
    {
        rnd = Random.Range(0, PillarList.Count);
        setPillar.transform.position = PillarList[rnd].transform.position; //�ʒu��ύX

        if (setPillar.name == "LightPillarNormal")
        {
            PillarList.Add(nowPillarNormal); //�����������̏ꏊ���X�|�[���ʒu�ɒǉ�
            nowPillarNormal = PillarList[rnd]; //Normal�̒��̏ꏊ���L��
        }
        else if (setPillar.name == "LightPillarShy")
        {
            PillarList.Add(nowPillarShy); //�����������̏ꏊ���X�|�[���ʒu�ɒǉ�
            nowPillarShy = PillarList[rnd]; //Shy�̒��̏ꏊ���L��
        }
        else if (setPillar.name == "LightPillarBoar")
        {
            PillarList.Add(nowPillarBoar); //�����������̏ꏊ���X�|�[���ʒu�ɒǉ�
            nowPillarBoar = PillarList[rnd]; //Boar�̒��̏ꏊ���L��
        }
        else if (setPillar.name == "LightPillarOctopus")
        {
            PillarList.Add(nowPillarOctopus);  //�����������̏ꏊ���X�|�[���ʒu�ɒǉ�
            nowPillarOctopus = PillarList[rnd]; //Octopus�̒��̏ꏊ���L��
        }

        PillarList.Remove(PillarList[rnd]); //�V�������̈ʒu���X�|�[���ʒu����폜
    }
}
