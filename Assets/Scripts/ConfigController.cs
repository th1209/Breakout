using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigController : MonoBehaviour
{
    // SE再生用クラス。
    protected SePlayer sePlayer;

    public void Start ()
    {
        this.sePlayer = GameObject.Find("SePlayer").GetComponent<SePlayer>();
    }

    public void ToTop()
    {
        this.sePlayer.Play("button");
        SceneManager.LoadScene("Top");
    }
}
