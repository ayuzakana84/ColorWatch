using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoarSearch : MonoBehaviour
{
    //EnemyPatrol
    //public Transform[] points;
    //private int destPoint = 0;

    //EnemySearch
    //float speed = 5;
    public Transform player;
    //public Transform ebPos;
    public GameObject enemyBoar;
    NavMeshAgent agent;
    private RaycastHit hit;
    private Vector3 playerPos;
    private Vector3 target;

    void Start()
    {
        agent = enemyBoar.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerPos = player.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) //ok
        {
            target = playerPos;

            var diff = target - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance)) //ok
            {
                //ebPos.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y), speed * Time.deltaTime);

                if (hit.transform.gameObject == player) //反応してない
                {
                    Debug.Log("touch!");
                    agent.speed = 0;
                    //
                    agent.speed += 5;
                    agent.destination = target;
                    agent.speed -= 5;
                }
                else
                {
                    //GotoNextPoint();
                }
            }
        }
    }

    //void GotoNextPoint()
    //{
    //    if (points.Length == 0)
    //    {
    //        return;
    //    }

    //    agent.destination = points[destPoint].position;

    //    destPoint = (destPoint + 1) % points.Length;
    //}
}
