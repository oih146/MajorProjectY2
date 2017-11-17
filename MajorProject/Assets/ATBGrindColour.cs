using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATBGrindColour : MonoBehaviour {

    private ParticleSystem ps;
    public float initialR = 0.0F;
    public float initialG = 0.0F;
    public float initialB = 0.0F;
    public float initialA = 1.0F;
	
	public float newR = 0.0F;
    public float newG = 0.0F;
    public float newB = 0.0F;
    public float newA = 1.0F;
	
	public float colourChangeCoord = 0.0f;
	private float currentPos = 0.0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
		currentPos = transform.position.x;
        var main = ps.main;
		if (currentPos >= colourChangeCoord) 
		{
        main.startColor = new Color(newR, newG, newB, newA);
		}else{
		main.startColor = new Color(initialR, initialG, initialB, initialA);
		}
			
    }

}