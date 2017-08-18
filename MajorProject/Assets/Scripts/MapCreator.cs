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
    public static MapCreator instance;
    ScrollRect scrollRect;
	// Use this for initialization
	void Start () {
        instance = this;
        scrollRect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateMap()
    {
        m_screenCells = new Image[m_mapDividers.Length];
        Vector2 pos = scrollRect.content.sizeDelta;
        pos.x = mapSquare.rectTransform.sizeDelta.x * (m_mapDividers.Length + 1);
        scrollRect.content.sizeDelta = pos;
        mapSquare = Instantiate(mapSquare, scrollRect.content);
        mapSquare.rectTransform.sizeDelta = new Vector2(100, 85);
        mapSquare.rectTransform.position = new Vector3(-(scrollRect.content.sizeDelta.x / 2) - mapSquare.rectTransform.sizeDelta.x, 0, 0);

        for (int i = 0; i < m_mapDividers.Length - 1; i++)
        {

            scrollRect.content.position = pos;
            Image temp = Instantiate(mapSquare, scrollRect.content);
            temp.rectTransform.sizeDelta = new Vector2(100, 85);
            temp.rectTransform.position = new Vector3((-(scrollRect.content.sizeDelta.x / 2) - temp.rectTransform.sizeDelta.x) + (i * 120), 0, 0);
            m_screenCells[i] = temp;
        }
    }

    public void NewMapPosition(bool forward)
    {

        Vector3 tempPos = mapSquare.rectTransform.position;
        mapSquare.rectTransform.position = m_screenCells[m_playerMapPosition].rectTransform.position;
        m_screenCells[m_playerMapPosition].rectTransform.position = tempPos;
        m_playerMapPosition += (forward) ? 1 : -1;
    }
}
