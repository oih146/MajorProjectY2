using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAdjPulse : MonoBehaviour { 




    public Color colour = new Color(0.2F, 0.3F, 0.4F, 0.5F);
    public float floor = 0.3f;
    public float ceiling = 1.0f;
    public float pulseSpeed = 1.0f;
    public float timeOffset = 0.0f;

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;


        float emission = floor + Mathf.PingPong(Time.time*pulseSpeed + timeOffset, ceiling - floor);


        Color finalColor = colour * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
}