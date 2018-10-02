using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchState : MonoBehaviour
{
    public Text scoreLeft;
    public Text scoreRight;

    public PaddleMover paddleLeft;
    public PaddleMover paddleRight;

    private int scoreCountLeft = 0;
    private int scoreCountRight = 0;

    void Start()
    {
        ResetMatch();
    }

    public void ResetMatch()
    {
        paddleLeft.SetAiActive(true);
        paddleRight.SetAiActive(true);

        this.scoreCountLeft = 0;
        this.scoreCountRight = 0;
        this.scoreLeft.text = "0";
        this.scoreRight.text = "0";
    }

    public void HitPaddleLeft()
    {
        paddleLeft.SetAiActive(false);
        paddleRight.SetAiActive(true);
    }

    public void HitPaddleRight()
    {
        paddleLeft.SetAiActive(true);
        paddleRight.SetAiActive(false);
    }

    public void HitWallLeft()
    {
        paddleLeft.SetAiActive(false);
        paddleRight.SetAiActive(true);
        this.scoreCountRight += 1;
        this.scoreRight.text = scoreCountRight.ToString();
        this.scoreRight.GetComponent<FontGlower>().StartGlow();
    }

    public void HitWallRight()
    {
        paddleLeft.SetAiActive(true);
        paddleRight.SetAiActive(false);
        this.scoreCountLeft += 1;
        this.scoreLeft.text = scoreCountLeft.ToString();
        this.scoreLeft.GetComponent<FontGlower>().StartGlow();
    }
}
