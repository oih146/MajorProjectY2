using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    public struct NotificationBlock
    {
        public List<string> stringArray;

        public void Clear()
        {
            stringArray.Clear();
        }
    }

    public GameObject m_notificationPrefab;
    public Notification m_currentNotification;
    public static NotificationManager Instance;
    private NotificationBlock m_noteBlock;
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
        m_noteBlock.stringArray = m_notificationText;

        for (int i = 0; i < m_noteBlock.stringArray.Count; i++)
        {
            m_currentNotification = Instantiate(m_notificationPrefab, new Vector3(transform.position.x, transform.position.y + (i * 1), transform.position.z), Quaternion.identity, transform).GetComponent<Notification>();
            m_currentNotification.Activate();
            m_currentNotification.SetText(m_noteBlock.stringArray[i]);
            m_currentNotification.StartLerping();
        }
        m_spotFree = false;
        m_notificationText.Clear();
        //m_noteBlock.Clear();
    }

    public void AddToList(string newText)
    {
        if (m_notificationText.Count > 0)
            m_notificationText.Insert(m_notificationText.Count, newText);
        else
            m_notificationText.Add(newText);
    }

    public void PushNotificationBlock()
    {
        CreateNotification();
    }

    public void DestroyCurrentNotification()
    {
        m_spotFree = true;
        if (m_notificationText.Count > 0)
            CreateNotification();
    }
}
