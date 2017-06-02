using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ブロック生成用のクラス。
 */
public class BlockGenerator : MonoBehaviour
{
    // 現在選択しているステージ。
    public static string currentStage = "1-1";

    // ブロックの雛形。
    public GameObject blockPrefab;

    // ブロックのマテリアルの雛形の配列。
    public List<Material> materialPrefabs;

    // ブロックの配置を開始する初期位置。
    // TODO:
    // ステージの形が変わる場合、配置の初期位置も変わるはず。
    // StageGeneratorなどのクラスを作って、初期位置を埋め込めるようにすること。
    //public float initX = 0.0f;
    //public float initZ = 0.0f;
    public float initX = -23.0f;
    public float initZ = 17.0f;

    private float curX;
    private float curZ;
    private List<BlockDataHolder> blockDataHolderList;

    public static void SetStage(string stage)
    {
        currentStage = stage;
    }

    /// <summary>
    /// Blockインスタンスをcsvから生成し、画面上に描画する。
    /// </summary>
    public void GenerateBlocks()
    {
        this.ReadBlockListFromCsv();
        this.InstantiateBlocks();
    }

    private void ReadBlockListFromCsv()
    {
        // 一旦ブロック配列を初期化する。
        this.blockDataHolderList = new List<BlockDataHolder>();

        // Csvファイルから文字列を読み取る。
        TextAsset textAsset = Resources.Load<TextAsset>("Csv/MapData/" + currentStage + "_blockList");
        if (textAsset == null) {
            Debug.Assert(false);
        }
        string text = textAsset.text;
        if (text.IndexOf('\r') < 0) {
            Debug.Assert(false, "改行コードにCRが含まれています。");
        }

        // ブロック配列に値を詰める。
        string[] row = text.Split('\n');
        for (int i = 0; i < row.Length; i++) {
            if (i == 0) continue;

            if (row[i] == "") continue;

            var columns = row[i].Split(',');
            this.blockDataHolderList.Add(new BlockDataHolder {
                rowNumber  = int.Parse(columns[0]),
                width      = float.Parse(columns[1]),
                height     = float.Parse(columns[2]),
                blockType  = (BLOCK_TYPE)(int.Parse(columns[3])),
                breakCount = int.Parse(columns[4]),
                hitScore   = int.Parse(columns[5]),
            });
        }
    }

    private void InstantiateBlocks()
    {
        // 一旦現在の座標情報をリセット。
        this.curX = this.initX;
        this.curZ = this.initZ;

        int curRowNum = 0;
        foreach (BlockDataHolder dataHolder in this.blockDataHolderList) {
            if (curRowNum != dataHolder.rowNumber) {
                // 現在の座標情報を更新。
                curRowNum = dataHolder.rowNumber;
                this.curX = this.initX;
                this.curZ -= dataHolder.height;
            }

            float x = this.curX + dataHolder.width  / 2.0f;
            float z = this.curZ - dataHolder.height / 2.0f;

            if (dataHolder.blockType >= BLOCK_TYPE.BLOCK_TYPE_NORMAL) {
                // ブロック生成。
                Block block = Instantiate(this.blockPrefab, new Vector3(x, 1, z), Quaternion.identity).GetComponent<Block>();
                block.InitFromDataHolder(dataHolder);
                block.InitMaterial(this.materialPrefabs);
            }

            this.curX += dataHolder.width;
        }
    }
}
