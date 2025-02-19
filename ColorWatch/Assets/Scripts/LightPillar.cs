using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LightPillar : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy; //敵を入れる配列
    [SerializeField] GameObject LightPillarManager;

    int rnd; //乱数を入れるint型
    [HideInInspector] public int normal = 3; //敵のカウント
    [HideInInspector] public int shy = 3;
    [HideInInspector] public int boar = 3;
    [HideInInspector] public int octopus = 3;
    [HideInInspector] public int tutorial = 1;

    List<int> enemyList = new List<int>(); //乱数が重複しないようにリストを使う

    //KillCountDisplay
    public TextMeshProUGUI normalKillCount;
    public TextMeshProUGUI shyKillCount;
    public TextMeshProUGUI boarKillCount;
    public TextMeshProUGUI octopusKillCount;
    public TextMeshProUGUI tutorialKillCount;

    //色を取り戻したときのモーション
    [SerializeField] VideoPlayer returnColorEffect; //柱の色と対応したものを入れる

    private void Awake()
    {
        normal = 3;
        shy = 3;
        boar = 3;
        octopus = 3;
        tutorial = 1;
    }

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
            rnd = Random.Range(0, enemyList.Count); //ランダムな要素を取得

            //柱の名前「LightPillarNormal」などで倒した敵のカウントを増やす
            if (this.gameObject.name == "LightPillarNormal")
            {
                normal--;
                normalKillCount.SetText("×{0}", normal);

                //EnemyNormalに飛んでエフェクトを止める
                Enemy[enemyList[rnd]].GetComponent<EnemyNormal>().DestroyEffect();
            }
            else if (this.gameObject.name == "LightPillarShy")
            {
                shy--;
                shyKillCount.SetText("×{0}", shy);

                //EnemyShyに飛んでエフェクトを止める
                Enemy[enemyList[rnd]].GetComponent<EnemyShy>().DestroyEffect();
            }
            else if (this.gameObject.name == "LightPillarBoar")
            {
                boar--;
                boarKillCount.SetText("×{0}", boar);

                //EnemyBoarSearchに飛んでエフェクトを止める
                Enemy[enemyList[rnd]].GetComponentInChildren<EnemyBoarSearch>().DestroyEffect();
            }
            else if (this.gameObject.name == "LightPillarOctopus")
            {
                octopus--;
                octopusKillCount.SetText("×{0}", octopus);
            }
            else
            {
                tutorial--;
                tutorialKillCount.SetText("×{0}", tutorial);
            }

            Destroy(Enemy[enemyList[rnd]]); //敵を削除
            enemyList.RemoveAt(rnd); //リストから削除

            if (enemyList.Count > 0)
            {
                LightPillarManager.GetComponent<LightPillarManager>().ChangePillarPoint(this.gameObject);
            }
            else
            {
                returnColorEffect.Play(); //エフェクトの再生

                Destroy(this.gameObject);
            }
        }
    }
}
