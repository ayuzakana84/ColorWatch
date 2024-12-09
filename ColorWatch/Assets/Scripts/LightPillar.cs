using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy; //�G������z��
    [SerializeField] GameObject LightPillarManager;

    int rnd; //����������int�^
    [HideInInspector] public int normal = 3, shy = 3, boar = 3, octopus = 3, tutorial = 1; //�G�̃J�E���g

    List<int> enemyList = new List<int>(); //�������d�����Ȃ��悤�Ƀ��X�g���g��

    //KillCountDisplay
    public TextMeshProUGUI normalKillCount;
    public TextMeshProUGUI shyKillCount;
    public TextMeshProUGUI boarKillCount;
    public TextMeshProUGUI octopusKillCount;
    public TextMeshProUGUI tutorialKillCount;

    private void Awake()
    {
        shy = 3;
    }

    void Start()
    {
        Debug.Log(shy);

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

            //���̖��O�uLightPillarNormal�v�Ȃǂœ|�����G�̃J�E���g�𑝂₷
            if (this.gameObject.name == "LightPillarNormal")
            {
                normal--;
                normalKillCount.SetText("�~{0}", normal);

                //EnemyNormal�ɔ��ŃG�t�F�N�g���~�߂�
                Enemy[enemyList[rnd]].GetComponent<EnemyNormal>().DestroyEffect();
            }
            else if (this.gameObject.name == "LightPillarShy")
            {
                shy--;
                shyKillCount.SetText("�~{0}", shy);

                //EnemyShy�ɔ��ŃG�t�F�N�g���~�߂�
                Enemy[enemyList[rnd]].GetComponent<EnemyShy>().DestroyEffect();
            }
            else if (this.gameObject.name == "LightPillarBoar")
            {
                boar--;
                boarKillCount.SetText("�~{0}", boar);

                //EnemyBoarSearch�ɔ��ŃG�t�F�N�g���~�߂�
                Enemy[enemyList[rnd]].GetComponentInChildren<EnemyBoarSearch>().DestroyEffect();
            }
            else if (this.gameObject.name == "LightPillarOctopus")
            {
                octopus--;
                octopusKillCount.SetText("�~{0}", octopus);
            }
            else
            {
                tutorial--;
                tutorialKillCount.SetText("�~{0}", tutorial);
            }

            Destroy(Enemy[enemyList[rnd]]); //�G���폜
            enemyList.RemoveAt(rnd); //���X�g����폜

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
