using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glower : MonoBehaviour
{
    public Color baseColor;
    public Color glowColor;
    public float glowIntensity;
    public float glowTime;
    public float glowStartRatio;

    private AnimationCurve glowCurve;
    private AnimationCurve opacityCurve;
    private Material material;

    // Use this for initialization
    void Start()
    {
        this.material = GetComponent<Renderer>().material;
        this.glowCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 0));
        this.opacityCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Opacity
        Color baseColor = this.material.GetColor("_Color");
        baseColor.a = this.opacityCurve.Evaluate(Time.time);
        this.material.SetColor("_Color", baseColor);

        // Glow
        float alpha = this.glowCurve.Evaluate(Time.time);
        alpha *= alpha;

        float baseGamma = Mathf.GammaToLinearSpace(0.8f);
        // Change color
        float emission = baseGamma + this.glowIntensity * alpha;
        Color finalColor =
            Color.Lerp(this.baseColor, this.glowColor, alpha) * Mathf.LinearToGammaSpace(emission);
        finalColor.a = this.opacityCurve.Evaluate(Time.time);
        this.material.SetColor("_EmissionColor", finalColor);
    }

    public void StartGlow()
    {
        this.glowCurve = new AnimationCurve(
            new Keyframe(Time.time, 0),
            new Keyframe(Time.time + this.glowStartRatio * this.glowTime, 1),
            new Keyframe(Time.time + this.glowTime, 0)
            );
        this.glowCurve.preWrapMode = WrapMode.Once;
        this.glowCurve.postWrapMode = WrapMode.Once;
    }

    public void FadeIn()
    {
        this.opacityCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    }

    public void FadeOut()
    {
        this.opacityCurve = new AnimationCurve(
            new Keyframe(Time.time, 1),
            new Keyframe(Time.time + 0.5f, 0)
            );
        this.opacityCurve.preWrapMode = WrapMode.ClampForever;
        this.opacityCurve.postWrapMode = WrapMode.ClampForever;
    }

    public bool IsInvisible()
    {
        return this.opacityCurve.Evaluate(Time.time) == 0;
    }
}
