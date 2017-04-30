using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    // x軸方向の速度。
    public float x = 50;

    // このインスタンスのRigidbody。
    protected Rigidbody rb;

    public void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void Update ()
    {
        // ユーザの入力に応じて、x軸方向の速度を切り替える。
        // TODO:RigidBodyに対しAddForceをすれば、加速度を使った滑らかな動きになりそう。
        Vector3 tmp = rb.velocity;
        tmp.x = Input.GetAxisRaw("Horizontal") * x;
        rb.velocity = tmp;
    }

    /**
     * このバーを画面中央部に寄せる。
     */
    public void AlignCenter()
    {
        Vector3 tmp = transform.position;
        tmp.x = 0;
        transform.position = tmp;
    }
}
