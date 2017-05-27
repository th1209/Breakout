using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopController : MonoBehaviour
{
    // SE再生用クラス。
    protected SePlayer sePlayer;

    public void Start ()
    {
        this.sePlayer       = GameObject.Find("SePlayer").GetComponent<SePlayer>();
    }

    public void ToStageSelect()
    {
        this.sePlayer.Play("button");
        SceneManager.LoadScene("StageSelect");
    }

    public void ToConfig()
    {
        this.sePlayer.Play("button");
        this.sePlayer.Play("button");
        SceneManager.LoadScene("Config");
    }
}
