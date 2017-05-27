using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    protected BlockGenerator blockGenerator;

    // SE再生用クラス。
    protected SePlayer sePlayer;

    public void Start ()
    {
        this.sePlayer       = GameObject.Find("SePlayer").GetComponent<SePlayer>();
    }

    public void ToTop()
    {
        this.sePlayer.Play("button");
        SceneManager.LoadScene("Top");
    }

    public void ToMain(string stage)
    {
        this.sePlayer.Play("button");
        // ブロック生成クラスに、現在のステージを教える。
        BlockGenerator.SetStage(stage);
        SceneManager.LoadScene("Main");
    }
}
