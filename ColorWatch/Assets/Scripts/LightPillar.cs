using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy; //�G������z��
    [SerializeField] GameObject LightPillarManager;

    int rnd; //����������int�^
    [HideInInspector] public int normal = 0, shy = 0, boar = 0, octopus = 0, tutorial = 0; //�G�̃J�E���g

    List<int> enemyList = new List<int>(); //�������d�����Ȃ��悤�Ƀ��X�g���g��

    void Start()
    {
        for (int i = 0; i < Enemy.Length; i++)
        {
            enemyList.Add(i);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rnd = Random.Range(0, enemyList.Count); //�����_���ȗv�f���擾
            Destroy(Enemy[enemyList[rnd]]); //�I�u�W�F�N�g���폜
            enemyList.RemoveAt(rnd); //���X�g����폜

            //���̖��O�uLightPillarNormal�v�Ȃǂœ|�����G�̃J�E���g�𑝂₷
            if (this.gameObject.name == "LightPillarNormal")
                normal++;
            else if (this.gameObject.name == "LightPillarShy")
                shy++;
            else if (this.gameObject.name == "LightPillarBoar")
                boar++;
            else if (this.gameObject.name == "LightPillarOctopus")
                octopus++;
            else
                tutorial++;

                if (enemyList.Count > 0)
            {
                LightPillarManager.GetComponent<LightPillarManager>().ChangePillarPoint(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
