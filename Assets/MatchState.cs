using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchState : MonoBehaviour
{
    public Text scoreLeft;
    public Text scoreRight;

    private int scoreCountLeft = 0;
    private int scoreCountRight = 0;

    void Start()
    {
        ResetMatch();
    }

    public void ResetMatch()
    {
        this.scoreCountLeft = 0;
        this.scoreCountRight = 0;
        this.scoreLeft.text = "0";
        this.scoreRight.text = "0";
    }

    public void HitPaddleLeft()
    {
    }

    public void HitPaddleRight()
    {
    }

    public void HitWallLeft()
    {
        this.scoreCountRight += 1;
        this.scoreRight.text = scoreCountRight.ToString();
        this.scoreRight.GetComponent<FontGlower>().StartGlow();
    }

    public void HitWallRight()
    {
        this.scoreCountLeft += 1;
        this.scoreLeft.text = scoreCountLeft.ToString();
        this.scoreLeft.GetComponent<FontGlower>().StartGlow();
    }
}
