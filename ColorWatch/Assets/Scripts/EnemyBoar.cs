using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyBoar : MonoBehaviour
{
    EnemyNormal en;

    //EnemyPatrol
    [SerializeField] Transform player;
    [SerializeField] float detectDistance;
    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;
    bool IsDetected = false;

    //Color
    [SerializeField] GameObject obj;
    [SerializeField] Material m;
    int boar = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GotoNextPoint();

        Rigidbody rb = this.transform.GetComponent<Rigidbody>();
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
            //agent.destination = player.position;
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPillar"))
        {
            Destroy(this.gameObject);
            en.kill++;
            en.killCount.SetText("Enemy : {0} / 15", en.kill);
            if (other.gameObject.name == "LightPillarBoar")
            {
                boar++;
                if (boar == 4)
                {
                    GameObject gObj = GameObject.Find("LightPillarBoar");
                    Destroy(gObj);
                    obj.GetComponent<Renderer>().material = m; //オブジェクト複数個になるからタグで判別の方がいいかも
                }
            }
        }
    }
}
