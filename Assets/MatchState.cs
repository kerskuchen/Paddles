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

    // Use this for initialization
    void Start()
    {
        this.scoreLeft.text = "2";
        this.scoreRight.text = "3";
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
        this.scoreRight.gameObject.SendMessage("StartGlow");
    }

    public void HitWallRight()
    {
        this.scoreCountLeft += 1;
        this.scoreLeft.text = scoreCountLeft.ToString();
        this.scoreLeft.gameObject.SendMessage("StartGlow");
    }
}
