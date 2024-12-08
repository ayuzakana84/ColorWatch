using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Video;
//using UnityEditor.SearchService;

public class EnemyShy : MonoBehaviour
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
            //FreameEffect�̃X�N���v�g�ɔ��ŃG�t�F�N�g���Đ�
            if (!EffectFlag)
            {
                FrameEffect.GetComponent<FrameEffect>().PlayEffect();
                EffectFlag = true;
            }

            agent.destination = player.position;
        }
        else
        {
            //FreameEffect�̃X�N���v�g�ɔ��ŃG�t�F�N�g���I��
            if (EffectFlag)
            {
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
    //    if (other.gameObject.name == "LightPillarShy")
    //    {
    //        Destroy(this.gameObject);
    //        en.kill++;
    //        en.killCount.SetText("Enemy : {0} / 10", en.kill);
    //        shy++;
    //        if (shy == 3)
    //        {
    //            GameObject gObj = GameObject.Find("LightPillarShy");
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