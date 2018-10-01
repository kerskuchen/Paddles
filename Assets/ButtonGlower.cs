using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGlower : MonoBehaviour
{
    public Color color;
    public float glowIntensity;

    private Material material;
    private Text text;

    void Start()
    {
        // Swap out our material instance with the existing material
        this.text = GetComponent<Text>();
        this.material = Material.Instantiate(this.text.material);
        this.text.material = this.material;
    }

    void FixedUpdate()
    {
        float baseGamma = Mathf.GammaToLinearSpace(1.0f);
        float emission = baseGamma + this.glowIntensity;
        Color finalColor = this.color * Mathf.LinearToGammaSpace(emission);
        this.material.SetColor("_EmissionColor", finalColor);
    }

}

