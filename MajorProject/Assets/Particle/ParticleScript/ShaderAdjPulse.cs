using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAdjPulse : MonoBehaviour { 




    public Color colour = new Color(0.2F, 0.3F, 0.4F, 0.5F);

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float floor = 0.3f;
        float ceiling = 1.0f;
        float emission = floor + Mathf.PingPong(Time.time, ceiling - floor);


        Color finalColor = colour * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
}