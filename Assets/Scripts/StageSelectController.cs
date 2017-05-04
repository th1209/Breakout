using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    protected BlockGenerator blockGenerator;

    public void ToTop()
    {
        SceneManager.LoadScene("Top");
    }

    public void ToMain(string stage)
    {
        // ブロック生成クラスに、現在のステージを教える。
        BlockGenerator.SetStage(stage);
        SceneManager.LoadScene("Main");
    }
}
