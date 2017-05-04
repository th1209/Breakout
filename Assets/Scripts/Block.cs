using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // このブロックが壊れるための、ヒット回数。
    public int breakCount = 1;

    // このブロックにヒットした際に得られるスコア。
    public int hitScore = 100;

    // このブロックが、ボールとヒットした回数。breakCountに達した時点でDestroy。
    protected int hitCount;

    // 通知先コントローラ。
    protected MainController controller;

    public void Start ()
    {
        controller = GameObject.Find("MainController").GetComponent<MainController>();
        hitCount = 0;
    }

    public void SetBreakCount (int breakCount)
    {
        this.breakCount = breakCount;
    }

    void OnCollisionEnter(Collision other)
    {
        // ボールとぶつかった際の処理。
        if (other.gameObject.tag == "Ball") {
            hitCount++;

            // 破壊時処理。
            if (hitCount >= breakCount) {
                // TODO: 効果音
                // TODO: 破壊時Effect
                Destroy(this.gameObject);
            }

            // スコア加算。
            controller.AddScore(hitScore);
        }
    }
}
