using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour {

    //All map dividers must be placed in array,
    //will always have at least on divider
    public GameObject[] m_mapDividers;
    public Image[] m_screenCells;
    public int m_playerMapPosition = 0;
    public Image mapSquare;
    public Image playerSquare;
    public static MapCreator instance;
    public RectTransform scrollRect;
    float m_timeSinceStart;
    public float m_cellTransitionSpeed;
    float m_InitXPos;
    float m_ToXPos;
    public bool m_lerp;
	// Use this for initialization
	void Start () {
        instance = this;
        CreateMap();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_lerp)
        {
            Lerping();
        }
	}

    void CreateMap()
    {
        m_screenCells = new Image[m_mapDividers.Length + 1];
        Vector2 pos = scrollRect.sizeDelta;
        pos.x = (mapSquare.rectTransform.sizeDelta.x + 5) * (m_mapDividers.Length + 1);
        scrollRect.sizeDelta = pos;
        scrollRect.localPosition = new Vector3( -15 + (mapSquare.rectTransform.sizeDelta.x / 2) * (m_mapDividers.Length + 1), -46, 0);
        //playerSquare = Instantiate(playerSquare, scrollRect);
        //playerSquare.rectTransform.sizeDelta = new Vector2(50, 42);
        //playerSquare.rectTransform.localPosition = new Vector3(-((scrollRect.sizeDelta.x / 2) - (playerSquare.rectTransform.sizeDelta.x / 2) ), 0, 0);

        for (int i = 0; i < m_mapDividers.Length + 1; i++)
        {
            Image temp = Instantiate(mapSquare, scrollRect);
            temp.rectTransform.sizeDelta = new Vector2(50, 42);
            temp.rectTransform.localPosition = new Vector3((i * 55) -((scrollRect.sizeDelta.x / 2) - (temp.rectTransform.sizeDelta.x / 2)), 0, 0);
            m_screenCells[i] = temp;
        }
    }

    void SetupLerp()
    {
        m_timeSinceStart = Time.time;
        m_InitXPos = scrollRect.parent.localPosition.x;
        m_ToXPos = 0 - (((mapSquare.rectTransform.sizeDelta.x) + 5) * m_playerMapPosition);
    }

    public void Lerping()
    {
        float timeSinceLerp = Time.time - m_timeSinceStart;
        float percentageComplete = timeSinceLerp / m_cellTransitionSpeed;

        Vector3 temp = scrollRect.parent.localPosition;
        temp.x = Mathf.Lerp(m_InitXPos, m_ToXPos, percentageComplete);
        scrollRect.parent.localPosition = temp;
        if (scrollRect.parent.localPosition.x > m_ToXPos - 0.001f && scrollRect.parent.localPosition.x < m_ToXPos + 0.001f)
        {
            m_lerp = false;
        }
    }
    public void NewMapPosition(bool forward)
    {
        if (!forward)
            m_playerMapPosition -= 1;
        if (forward)
            m_playerMapPosition += 1;
        SetupLerp();
        m_lerp = true;
    }
}
