using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    public void ToTop()
    {
        SceneManager.LoadScene("Top");
    }

    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
