using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoarSearch : MonoBehaviour
{
    //EnemyPatrol
    NavMeshAgent agent;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    bool IsDetected = false;

    //EnemySearch
    public Transform player;
    //private GameObject lookToPlayer;
    public GameObject enemyBoar;
    private RaycastHit hit;
    private Vector3 playerPos;
    private Vector3 targetPos;

    void Start()
    {
        agent = enemyBoar.GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    private void Update()
    {
        playerPos = player.transform.position;

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

        //if (IsDetected)
        //{
        //    agent.destination = targetPos;
        //}
        //else
        //{
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        //}
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player")) //ok
    //    {
    //        var diff = targetPos - transform.position;
    //        var distance = diff.magnitude;
    //        var direction = diff.normalized;

    //        if (Physics.Raycast(transform.position, direction, out hit, distance)) //ok
    //        {
    //            if (hit.transform.gameObject == player) //�������ĂȂ� player�������H
    //            {
    //                Debug.Log("touch!");
    //                agent.speed = 0;
    //                Invoke("AgentSpeedUp", 2.0f);
    //                agent.destination = targetPos;
    //                if (targetPos == agent.transform.position)
    //                {
    //                    agent.speed = 0;
    //                }
    //            }
    //            else
    //            {
    //                GotoNextPoint();
    //            }
    //        }
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (IsDetected) //�͈͓��̂Ƃ�
        {
            StartCoroutine("Rotate");
            targetPos = playerPos; //player�̈ʒu�擾
            var diff = targetPos - transform.position;
            var distance2 = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance2)) //ok
            {
                if (hit.transform.gameObject == player) //�������ĂȂ� player�������H
                {
                    Debug.Log("touch!");
                    agent.speed = 0;
                    Invoke("AgentSpeedUp", 2.0f);
                    agent.destination = targetPos;
                    if (targetPos == agent.transform.position)
                    {
                        agent.speed = 0;
                    }
                }
                else
                {
                    GotoNextPoint();
                }
            }
        }
    }

    void AgentSpeedUp()
    {
        agent.speed += 5;
    }

    void AgentSpeedDown()
    {
        agent.speed -= 5;
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

    IEnumerator Rotate() //�v���C���[�̕���������
    {
        Vector3 vector3 = targetPos - enemyBoar.transform.position; //player��boar�̍��W����x�N�g�����v�Z
        vector3.y = 0f; //�㉺�̉�]���Ȃ�
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        enemyBoar.transform.rotation = quaternion;
        yield return new WaitForSeconds(1.5f);
    }
}
