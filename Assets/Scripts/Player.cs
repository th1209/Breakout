using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 残りボール数。
    public int life = 3;

    // 現在のスコア。
    protected int score = 0;

    public void addScore(int increment)
    {
        score += increment;
    }

    public void decreaseLife()
    {
        if (life > 0) {
            life--;
        }
    }

    public bool isDead()
    {
        return (life <= 0);
    }
}
