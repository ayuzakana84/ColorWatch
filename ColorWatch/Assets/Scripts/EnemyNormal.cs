using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.SearchService;

public class EnemyNormal : MonoBehaviour
{
    //EnemyPatrol
    [SerializeField] Transform player;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;
    bool IsDetected = false;

    //FrameEffect
    [SerializeField] GameObject FrameEffect;
    bool EffectFlag = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    private void Update()
    {
        float distance;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectDistance)
        {
            IsDetected = true;
        }
        else
        {
            IsDetected = false;
        }

        if (IsDetected)
        {
            if (!EffectFlag)
            {
                //FreameEffect�̃X�N���v�g�ɔ��ŃG�t�F�N�g���Đ�
                FrameEffect.GetComponent<FrameEffect>().PlayEffect();
                EffectFlag = true;
            }

            agent.destination = player.position;
        }
        else
        {
            if (EffectFlag)
            {
                //FreameEffect�̃X�N���v�g�ɔ��ŃG�t�F�N�g���I��
                FrameEffect.GetComponent<FrameEffect>().StopEffect();
                EffectFlag = false;
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "LightPillarNormal")
    //    {
    //        Destroy(this.gameObject);
    //        kill++;
    //        killCount.SetText("Enemy : {0} / 10", kill);
    //        normal--;
    //        if (normal == 0)
    //        {
    //            GameObject gObj = GameObject.Find("LightPillarNormal");
    //            Destroy(gObj);
    //        }
    //    }
    //}

    //����鎞�G�t�F�N�g���~�߂�
    public void DestroyEffect()
    {
        if (EffectFlag)
        {
            //FreameEffect�̃X�N���v�g�ɔ��ŃG�t�F�N�g���I��
            FrameEffect.GetComponent<FrameEffect>().StopEffect();
        }
    }
}
