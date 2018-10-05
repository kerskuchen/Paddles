using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchState : MonoBehaviour
{
    public Text scoreLeft;
    public Text scoreRight;
    public Text winLose;

    public PaddleMover paddleLeft;
    public PaddleMover paddleRight;

    public bool matchFinished = false;

    int scoreCountLeft = 0;
    int scoreCountRight = 0;

    bool isAiMatch = true;

    const int NUM_HITS_TO_WIN = 10;

    void Start()
    {
        ResetMatch(isAiMatch);
    }

    public void ResetMatch(bool isAiMatch)
    {
        this.matchFinished = false;

        GameObject.Find("Pong").GetComponent<Glower>().FadeIn();
        this.isAiMatch = isAiMatch;

        paddleLeft.SetAIFast(true);
        paddleRight.SetAIFast(true);
        this.paddleLeft.SetAIActive(true);
        this.paddleRight.SetAIActive(true);

        this.scoreCountLeft = 0;
        this.scoreCountRight = 0;
        this.scoreLeft.text = "0";
        this.scoreRight.text = "0";

        this.winLose.text = "";
    }

    public void HitPaddleLeft()
    {
        paddleLeft.SetAIFast(false);
        paddleRight.SetAIFast(true);
    }

    public void HitPaddleRight()
    {
        paddleLeft.SetAIFast(true);
        paddleRight.SetAIFast(false);
    }

    public void HitWallLeft()
    {
        paddleLeft.SetAIFast(false);
        paddleRight.SetAIFast(true);
        if (this.scoreCountLeft < NUM_HITS_TO_WIN && this.scoreCountRight < NUM_HITS_TO_WIN)
        {
            this.scoreCountRight += 1;
            this.scoreRight.text = scoreCountRight.ToString();
            this.scoreRight.GetComponent<FontGlower>().StartGlow();

            if (this.scoreCountRight == NUM_HITS_TO_WIN && !this.isAiMatch)
            {
                GameObject.Find("Pong").GetComponent<Glower>().FadeOut();
                this.paddleLeft.isPlayerControlled = false;
                this.paddleLeft.SetAIActive(false);
                this.paddleRight.SetAIActive(false);
                this.winLose.text = "YOU LOST!";
                this.matchFinished = true;
                this.winLose.GetComponent<FontGlower>().StartGlow();
            }
        }
    }

    public void HitWallRight()
    {
        paddleLeft.SetAIFast(true);
        paddleRight.SetAIFast(false);
        if (this.scoreCountLeft < NUM_HITS_TO_WIN && this.scoreCountRight < NUM_HITS_TO_WIN)
        {
            this.scoreCountLeft += 1;
            this.scoreLeft.text = scoreCountLeft.ToString();
            this.scoreLeft.GetComponent<FontGlower>().StartGlow();

            if (this.scoreCountLeft == NUM_HITS_TO_WIN && !this.isAiMatch)
            {
                GameObject.Find("Pong").GetComponent<Glower>().FadeOut();
                this.paddleLeft.isPlayerControlled = false;
                this.paddleLeft.SetAIActive(false);
                this.paddleRight.SetAIActive(false);
                this.winLose.text = "YOU WON!";
                this.matchFinished = true;
                this.winLose.GetComponent<FontGlower>().StartGlow();
            }
        }
    }
}
