    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // このブロックの表面を決める、マテリアル配列。
    //public Material[] materialPrefabs;
    public List<Material> materialPrefabs;

    // このブロックが壊れるための、ヒット回数。
    protected int breakCount;
    // このブロックにヒットした際に得られるスコア。
    protected int hitScore;
    // このブロックが、ボールとヒットした回数。breakCountに達した時点でDestroy。
    protected int hitCount;

    // 通知先コントローラ。
    protected MainController controller;
    // 破壊用オブジェクト。
    protected BlockDestroyer destroyer;

    /// <summary>
    /// データ保持クラスの情報を元に、このクラスを初期化する。
    /// </summary>
    /// <param name="dataHolder">Data holder.</param>
    public void InitFromDataHolder(BlockDataHolder dataHolder)
    {
        this.breakCount = dataHolder.breakCount;
        this.hitScore   = dataHolder.hitScore;
        this.hitCount   = 0;

        this.gameObject.transform.localScale = new Vector3(dataHolder.width, 1, dataHolder.height);

        this.controller = GameObject.Find("MainController").GetComponent<MainController>();
        this.destroyer  = GameObject.Find("BlockDestroyer").GetComponent<BlockDestroyer>();

        //this.GetComponent<Renderer>().material = materialPrefabs [breakCount - 1];
    }

    /// <summary>
    /// Inits the material.
    /// </summary>
    /// <param name="materials">Materials.</param>
    public void InitMaterial(List<Material> materials)
    {
        // TODO GetComponentって、gameObj介さなくても使えるんだっけ?
        this.GetComponent<Renderer>().material = materials[this.breakCount - 1];
    }

    /// <summary>
    /// Ons the collision enter.
    /// </summary>
    /// <param name="other">Other.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") {
            this.hitCount++;
            controller.AddScore(this.hitScore);

            if (this.hitCount >= this.breakCount)
            {
                destroyer.DestroyBlock(this);
            }
        }
    }
}
