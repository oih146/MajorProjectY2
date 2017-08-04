using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentLines : MonoBehaviour {

    //public int[] m_lineDeciderLimits;
    public string[] m_lightLines;
    public string[] m_lawLines;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetLawLine(int value)
    {
        if (value >= 0 && value <= 25)
            return m_lawLines[0];
        else if (value > 25 && value <= 50)
            return m_lawLines[1];
        else if (value > 50 && value <= 75)
            return m_lawLines[2];
        else if (value > 75 && value <= 100)
            return m_lawLines[3];
        return "";
    }

    public string GetLightLine(int value)
    {
        if (value >= 0 && value <= 25)
            return m_lightLines[0];
        else if (value > 25 && value <= 50)
            return m_lightLines[1];
        else if (value > 50 && value <= 75)
            return m_lightLines[2];
        else if (value > 75 && value <= 100)
            return m_lightLines[3];
        return "";
    }
}
