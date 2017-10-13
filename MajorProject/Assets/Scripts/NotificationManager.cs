using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    public GameObject m_notificationPrefab;
    public Notification m_currentNotification;
    public static NotificationManager Instance;
    private List<string> m_notificationText = new List<string>();
    public bool m_spotFree = true;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateNotification()
    {
        m_currentNotification = Instantiate(m_notificationPrefab, transform.position, Quaternion.identity, transform).GetComponent<Notification>();
        m_currentNotification.Activate();
        m_currentNotification.SetText(m_notificationText[0]);
        m_notificationText.RemoveAt(0);
        m_currentNotification.StartLerping();
        m_spotFree = false;
    }

    public void AddToList(string newText)
    {
        if (m_notificationText.Count > 0)
            m_notificationText.Insert(m_notificationText.Count, newText);
        else
            m_notificationText.Add(newText);
        if(m_spotFree)
        {
            CreateNotification();
        }
    }

    public void DestroyCurrentNotification()
    {
        Destroy(m_currentNotification.gameObject);
        m_spotFree = true;
        if (m_notificationText.Count > 0)
            CreateNotification();
    }
}
