﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMover : MonoBehaviour
{
    public bool isPlayerControlled = false;
    public GameObject pong;

    float vel = 0.0f;
    float pos = 0.0f;
    float aiAcc = 0.0f;
    float paddleSize = 3.5f;

    const float MAX_VELOCITY = 15;
    const float ACCEL = 100;
    const float DEACCEL = 400;
    const float TURN_ACC_MULTIPLIER = 4;

    bool aiFast = true;
    bool aiActive = true;

    public void SetAIFast(bool fast)
    {
        this.aiFast = fast;
    }

    public void SetAIActive(bool active)
    {
        this.aiActive = active;
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        float axisValue = 0.0f;
        if (this.isPlayerControlled)
            axisValue = doUserInput();
        else if (this.aiActive)
            axisValue = doAIInput();

        doMovement(axisValue);
    }

    public void Reset()
    {
        this.vel = 0.0f;
        this.pos = 0.0f;
    }


    float doUserInput()
    {
        return Input.GetAxis("Vertical");
    }

    float doAIInput()
    {
        float multiplier = 1.0f;
        if (!this.aiFast)
            multiplier = 0.1f;


        float distance = pong.transform.position.y - this.transform.position.y;
        if (Mathf.Abs(distance) > paddleSize / 2)
            this.aiAcc = Mathf.Lerp(this.aiAcc, Mathf.Sign(distance), 0.055f * multiplier);
        else
            this.aiAcc = Mathf.Lerp(this.aiAcc, 0, 0.9f * multiplier);
        return this.aiAcc;
    }

    void doMovement(float axisValue)
    {
        float delta = Time.deltaTime;
        if (axisValue == 0)
        {
            // no direction is pressed (or both are) so we deaccelerate
            float velAbs = Mathf.Abs(this.vel);
            velAbs -= delta * DEACCEL;

            if (velAbs < 0)
                velAbs = 0;

            this.vel = Mathf.Sign(this.vel) * velAbs;
        }
        else
        {
            // acc_multiplier defines the speed of direction change
            float acc_multiplier = 1;
            if (Mathf.Sign(this.vel) != Mathf.Sign(axisValue))
            {
                //if the direction input is in the opposite direction of
                // the current velocity, we want to change directions fast
                acc_multiplier = TURN_ACC_MULTIPLIER;
            }

            var increment = axisValue * delta * acc_multiplier * ACCEL;
            this.vel = ClampAbsolute(this.vel + increment, MAX_VELOCITY);
        }

        this.pos += Time.fixedDeltaTime * this.vel;

        float upperBound = 6.0f - paddleSize / 2;
        float lowerBound = -upperBound;
        if (this.pos > upperBound)
        {
            this.pos = upperBound;
            if (this.vel > 0)
                this.vel = 0;
        }
        if (this.pos < lowerBound)
        {
            this.pos = lowerBound;
            if (this.vel < 0)
                this.vel = 0;
        }

        Vector3 temp = this.transform.position;
        temp.y = this.pos;
        this.transform.position = temp;
    }

    float ClampAbsolute(float value, float absoluteMax)
    {
        float absVal = Mathf.Clamp(Mathf.Abs(value), 0, absoluteMax);
        return Mathf.Sign(value) * absVal;
    }
}
