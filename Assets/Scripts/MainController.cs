﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    // 初期状態でのライフ。
    public int defaultLife = 5;

    // ボールの雛形。
    public GameObject ballPrefab;

    // ブロックの雛形
    // TODO: 後ほど、別のブロック生成用クラスに切り出すこと。
    public GameObject blockPrefab;

    // このコントローラが操作するUIコンポーネント。
    protected GameObject readyPanel;
    protected GameObject gameOverPanel;
    protected GameObject gameClearPanel;
    protected Text scoreText;
    protected Text lifeText;

    // SE再生用クラス。
    protected SePlayer sePlayer;

    // このコントローラーで操作するボールとバー。
    // ボールは、必要に応じて破棄と再生成を行う。
    protected GameObject ball;
    protected Bar bar;

    // ブロック生成に責務を持つクラス。
    protected BlockGenerator blockGenerator;

    // 現在のスコアとライフ。
    protected int score;
    protected int life;

    // メインシーン中での状態遷移。
    enum State
    {
        // ボールやバーをリセットするための状態。
        Start,
        // 一旦ポーズして、プレイヤーの入力を促す状態。
        Ready,
        Play,
        GameOver,
        GameClear,
    }

    // 現在の状態。
    State currentState;

    /**
     * このコントローラの生成時に、一度だけ呼ばれる処理をまとめる。
     */
    public void Start ()
    {
        // 各GameObjectやComponentの参照を保持しておく。
        this.readyPanel     = GameObject.Find("ReadyPanel");
        this.gameOverPanel  = GameObject.Find("GameOverPanel");
        this.gameClearPanel = GameObject.Find("GameClearPanel");
        this.scoreText      = GameObject.Find("ScoreText").GetComponent<Text>();
        this.lifeText       = GameObject.Find("LifeText").GetComponent<Text>();
        this.sePlayer       = GameObject.Find("SePlayer").GetComponent<SePlayer>();

        // バーを取得。
        bar = GameObject.Find("Bar").GetComponent<Bar>();

        // ブロック生成用のインスタンスを取得。
        blockGenerator = GameObject.Find("BlockGenerator").GetComponent<BlockGenerator>();

        // その他初期化処理(メインシーンのリセット時にも、同じ処理が呼ばれる)。
        ResetGame();
    }

    /**
     * メインシーンのリセット(初回含む)で呼ばれるべき処理を実行する。
     */
    public void ResetGame()
    {
        // このコントローラが管理する変数をリセット。
        score = 0;
        life  = defaultLife;

        // ブロックの初期化。
        InitBlocks();

        // UIのリセット。
        InitUI();

        currentState = State.Start;
    }

    /**
     * 各種ブロックを初期化する。
     */
    protected void InitBlocks()
    {
        blockGenerator.GenerateBlocks();
    }

    /**
     * UIを初期化する。
     */
    protected void InitUI()
    {
        scoreText.text = "Score : " + score.ToString();
        scoreText.gameObject.SetActive(true);
        lifeText.text = "Ball : " + life.ToString();
        lifeText.gameObject.SetActive(true);
        readyPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        gameClearPanel.gameObject.SetActive(false);
    }

    /**
     * ボールとバーを初期化する。
     */
    protected void InitBallAndBar()
    {
        ball = (GameObject)Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        ball.GetComponent<Ball>().InitPosition();
        bar.AlignCenter();
    }

    /**
     * 各フレームの最後に呼ばれる。
     * 現在の状態を監視し、必要に応じて状態の切り替え処理を実施。
     */
    void LateUpdate()
    {
        switch (currentState)
        {
            case State.Start:
                InitBallAndBar();
                readyPanel.gameObject.SetActive(true);
                currentState = State.Ready;
                break;
            case State.Ready:
                if (Input.GetButtonDown("Jump")) {
                    ball.GetComponent<Ball>().InitVelocity();
                    readyPanel.gameObject.SetActive(false);
                    currentState = State.Play;
                }
                break;
            case State.Play:
                // クリア判定。
                int restBlockNum = GameObject.FindGameObjectsWithTag("Block").Length;
                if (restBlockNum <= 0) {
                    Destroy(ball);
                    currentState = State.GameClear;
                }
                break;
            case State.GameOver:
                // ※状態の遷移はuGUI上のボタンで行う。
                gameOverPanel.gameObject.SetActive(true);
                break;
            case State.GameClear:
                // ※状態の遷移はuGUI上のボタンで行う。
                gameClearPanel.gameObject.SetActive(true);
                break;
            default:
                Debug.Log("Invalid State!");
                break;
        }
    }

    /**
     * ボールを失った時の処理をまとめて実行する。
     */
    public void LoseBall()
    {
        Destroy(ball);
        DecreaseLife();
        if (IsDead()) {
            currentState = State.GameOver;
        } else {
            currentState = State.Start;
        }
    }

    /**
     * スコア加算時に呼ばれる処理をまとめて実行する。
     */
    public void AddScore(int increment)
    {
        score += increment;
        scoreText.text = "Score : " + score.ToString();
    }

    /**
     * 残りボール数が減った時の処理をまとめて実行する。
     */
    public void DecreaseLife()
    {
        if (life > 0) {
            life--;
        }
        lifeText.text = "Ball : " + life.ToString();
    }

    /**
     * 残りボール数の状態を判定。
     */
    public bool IsDead()
    {
        return (life <= 0);
    }

    public void ToStageSelect()
    {
        this.sePlayer.Play("button");
        SceneManager.LoadScene("StageSelect");
    }

    public void Retry()
    {
        this.sePlayer.Play("button");
        this.ResetGame();
    }
}
