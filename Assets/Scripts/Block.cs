using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // このブロックの表面を決める、マテリアル配列。
    public Material[] materialPrefabs;

    // このブロックが壊れるための、ヒット回数。
    protected int breakCount;

    // このブロックが、ボールとヒットした回数。breakCountに達した時点でDestroy。
    protected int hitCount;

    // このブロックにヒットした際に得られるスコア。
    protected int hitScore;

    // 通知先コントローラ。
    protected MainController controller;

    // 破壊用オブジェクト。
    protected BlockDestroyer destroyer;


    // ヒットした際に得られるスコアのデフォルト値。
    protected static int defaultHitScore = 100;

    public AudioClip breakSe;

    public GameObject explosionPrefab;

    /// <summary>
    /// ブロックに関する諸々の初期化処理を行う。ブロック初期化時に、必ずこのメソッドを呼び出すこと。
    /// </summary>
    /// <param name="breakCount">何回ヒットでこのブロックが壊れるか。</param>
    public void Initialze (int breakCount)
    {
        this.controller = GameObject.Find("MainController").GetComponent<MainController>();
        this.destroyer = GameObject.Find("BlockDestroyer").GetComponent<BlockDestroyer>();
        this.hitCount = 0;
        // TODO ヒット時スコアは決め打ち。ブロックの硬さに応じてヒット時スコアが変わるようにするのも良いかも。
        this.hitScore = Block.defaultHitScore;

        this.breakCount = breakCount;
        this.GetComponent<Renderer>().material= materialPrefabs[breakCount - 1];
    }


    void OnCollisionEnter(Collision other)
    {
        // ボールとぶつかった際の処理。
        if (other.gameObject.tag == "Ball") {
            this.hitCount++;
            controller.AddScore(this.hitScore);

            // 破壊時処理。
            if (this.hitCount >= this.breakCount)
            {
                destroyer.DestroyBlock(this);
            }
        }
    }
}
