using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ブロック破壊時の諸々の処理を行うクラス。
/// </summary>
/// <remarks>
/// 本来は「ブロック破壊」という機能でなく、
/// 「SE再生」や「エフェクト再生」という実装で、クラスを作るべき(その方が汎用性が高いから)。
/// e.g.) AudioPlayer, EffectGenerator
/// 今回は時間がないのとC#力が無いことを言い訳にして、このようなやっつけクラスを作っているが、今後修正を入れたほうが良いはず...。
/// </remarks>
public class BlockDestroyer : MonoBehaviour
{
    // ブロック破壊時効果音。
    public AudioClip breakSe;

    // ブロック破壊時エフェクト。
    public GameObject explosionEffectPrefab;

    /// <summary>
    /// ブロック破壊時の処理をまとめて実施する。
    /// </summary>
    /// <remarks>SEもエフェクトも、同時に一定数以上の数が再生されないように調整すべきかも。</remarks>
    /// <param name="block">Block.</param>
    public void DestroyBlock(Block block)
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(breakSe);

        GameObject effect = (GameObject)Instantiate(explosionEffectPrefab, block.gameObject.transform.position, Quaternion.identity);
        // エフェクト終了までに時間がかかるので、一定時間待ってからDestroy。
        Destroy(effect, 3f);

        Destroy(block.gameObject);
    }
}