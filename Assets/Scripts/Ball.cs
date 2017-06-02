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

    // x or z 成分の値の最小値。どちらかの成分がこの値以下の場合、速度を補正する。
    public float velocityMin;

    // 通知先コントローラ。
    public MainController controller;

    // このインスタンスのRigidbody。
    protected Rigidbody rb;

    public void Start ()
    {
        controller = GameObject.Find("MainController").GetComponent<MainController>();
        rb = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// このボールの速度を初期化する。
    /// </summary>
    public void InitPosition()
    {
        this.gameObject.transform.localPosition = new Vector3(0, 1.5f, -6);
    }

    /// <summary>
    /// このボールの位置を初期化する。
    /// </summary>
    public void InitVelocity()
    {
        rb.velocity = new Vector3(Random.Range(startXMin, startXMax), 0, startZ);
    }

    /// <summary>
    /// Ons the collision enter.
    /// </summary>
    /// <param name="other">Other.</param>
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
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * coefficientOnCollision;

        // x or z 成分の速度が一定以下の場合、補正する。
        if (Mathf.Abs(rb.velocity.x) <= velocityMin) {
            rb.velocity.Set(rb.velocity.x *  velocityMin, rb.velocity.y, rb.velocity.z);
        }
        if (Mathf.Abs(rb.velocity.z) <= velocityMin) {
            rb.velocity.Set(rb.velocity.x, rb.velocity.y, rb.velocity.z * velocityMin);
        }

        if (other.gameObject.tag == "FenceBottom") {
            // もし画面下部の壁に衝突したら、Controllerに諸々の処理をしてもらう。
            controller.LoseBall();
        }
    }
}
