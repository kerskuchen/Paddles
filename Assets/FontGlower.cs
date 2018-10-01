using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontGlower : MonoBehaviour
{
    public Color baseColor;
    public Color glowColor;
    public float glowIntensity;
    public float glowTime;
    public float glowStartRatio;

    private AnimationCurve animationCurve;
    private Material material;
    private Text text;

    // Use this for initialization
    void Start()
    {
        this.text = GetComponent<Text>();
        this.material = Material.Instantiate(this.text.material);
        this.text.material = this.material;
        this.animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 0));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float alpha = this.animationCurve.Evaluate(Time.time);
        alpha *= alpha;

        float baseGamma = Mathf.GammaToLinearSpace(1.0f);
        // Change color
        float emission = baseGamma + this.glowIntensity * alpha;
        Color finalColor =
            Color.Lerp(this.baseColor, this.glowColor, alpha) * Mathf.LinearToGammaSpace(emission);

        this.material.SetColor("_EmissionColor", finalColor);
    }

    public void StartGlow()
    {
        this.animationCurve = new AnimationCurve(
            new Keyframe(Time.time, 0),
            new Keyframe(Time.time + this.glowStartRatio * this.glowTime, 1),
            new Keyframe(Time.time + this.glowTime, 0)
            );
        this.animationCurve.preWrapMode = WrapMode.Once;
        this.animationCurve.postWrapMode = WrapMode.Once;
    }
}
