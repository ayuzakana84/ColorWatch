using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LightPillarManager : MonoBehaviour
{
    [SerializeField] GameObject[] PillarPoint; //�X�|�[���ʒu������z��
    [SerializeField] GameObject[] LightPillar; //���̒�������z��

    int rnd; //����������int�^
    int nowPillarNormal;
    int nowPillarShy;
    int nowPillarBoar;

    List<GameObject> PillarList = new List<GameObject>();
    List<GameObject> nowPillarList = new List<GameObject>();

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

            nowPillarList.Add(PillarList[rnd]); //�g�p���ɒǉ�
            Debug.Log(PillarList[rnd].name);

            if (LightPillar[i].name == "LightPillarNormal")
                nowPillarNormal = nowPillarList.IndexOf(PillarList[rnd]); //Normal�̒��̏ꏊ���L��
            else if (LightPillar[i].name == "LightPillarShy")
                nowPillarShy = nowPillarList.IndexOf(PillarList[rnd]); //Shy�̒��̏ꏊ���L��
            else if (LightPillar[i].name == "LightPillarBoar")
                nowPillarBoar = nowPillarList.IndexOf(PillarList[rnd]); //Boar�̒��̏ꏊ���L��

            PillarList.Remove(PillarList[rnd]); //�X�|�[���ʒu����폜
        }
    }

    //���̒��̈ʒu�̕ύX
    public void ChangePillarPoint(GameObject setPillar)
    {
        int index = 0;

        if (setPillar.name == "LightPillarNormal")
            index = nowPillarNormal;
        else if (setPillar.name == "LightPillarShy")
            index = nowPillarShy;
        else if (setPillar.name == "LightPillarBoar")
            index = nowPillarBoar;

        rnd = Random.Range(0, PillarList.Count);
        setPillar.transform.position = PillarList[rnd].transform.position; //�ʒu��ύX

        nowPillarList.Add(PillarList[rnd]); //�g�p���ɒǉ�
        PillarList.Add(nowPillarList[index]); //���������ʒu���X�|�[���ʒu�ɒǉ�
        nowPillarList.Remove(nowPillarList[index]); //���������ʒu���g�p������폜

        if (setPillar.name == "LightPillarNormal")
            nowPillarNormal = nowPillarList.IndexOf(PillarList[rnd]); //Normal�̒��̏ꏊ���L��
        else if (setPillar.name == "LightPillarShy")
            nowPillarShy = nowPillarList.IndexOf(PillarList[rnd]); //Shy�̒��̏ꏊ���L��
        else if (setPillar.name == "LightPillarBoar")
            nowPillarBoar = nowPillarList.IndexOf(PillarList[rnd]); //Boar�̒��̏ꏊ���L��

        PillarList.Remove(PillarList[rnd]); //�X�|�[���ʒu����폜
    }
}
