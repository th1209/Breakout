using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ブロックオブジェクトのデータを保持するクラス。マップ上への配置やBlockクラスのインスタンス生成時に使う。
/// </summary>
[Serializable]
public class BlockDataHolder
{
    // 描画する際のブロックの行番号。
    public int rowNumber;
    // 描画する際の横幅。
    public float width;
    // 描画する際の縦幅。
    public float height;

    // ブロックの種類。
    public BLOCK_TYPE blockType;

    // 何回ヒットして壊れるか。
    public int breakCount;
    // 一回ヒットする度に得られるスコア。
    public int hitScore;


    public override string ToString()
    {
        return String.Format(
            "hashCode:{0} rowNumber:{1} width:{2} height:{3} type:{4} breakCount:{5} hitScore:{6}",
            GetHashCode(), rowNumber, width, height, blockType, breakCount, hitScore
        );
    }
}
