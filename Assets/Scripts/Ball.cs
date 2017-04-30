using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // ボールが動き始めた時の、速度のx成分(最小・最大値)。
    public float startXMin;
    public float startXMax;

    // ボールが動き始めた時の、速度のz成分。
    public float startZ;

    // ボールが他オブジェクトと衝突した際に掛け合わせる係数。
    public float coefficientOnCollision;

    // 通知先コントローラ。
    public MainController controller;

    // このインスタンスのRigidbody。
    protected Rigidbody rb;

    public void Start ()
    {
        controller = GameObject.Find("MainController").GetComponent<MainController>();
        rb = this.GetComponent<Rigidbody>();
    }

    /**
     * このボールの速度を初期化する。
     */
    public void InitVelocity()
    {
        rb.velocity = new Vector3(Random.Range(startXMin, startXMax), 0, startZ);
    }

    public void OnCollisionEnter(Collision other)
    {
        // 衝突時にベクトルを正規化して補正値をかけることで、
        // 摩擦などによるボールの速度が減少を抑える。
        // MEMO:
        // * この方法は、「壁にぶつかったらボールが跳ね返る」といった
        //   断続的な動きをするゲームでしか通用しないだろう。
        //   速度に直接値を代入するのはUnity自体でも推奨していなくて、
        //   普通ならば力を与えてやる方法が良さそう。
        // * 今回についても、PhysicMaterialのFricitionを0にする方法でもいいかもしれない。
        //rb = this.GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * coefficientOnCollision;

        if (other.gameObject.tag == "FenceBottom") {
            // もし画面下部の壁に衝突したら、Controllerに諸々の処理をしてもらう。
            controller.LoseBall();
        }
    }
}
