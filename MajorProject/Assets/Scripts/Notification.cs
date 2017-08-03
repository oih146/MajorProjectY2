using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    private Text text;
    public float upPoint;
    public float upRate;
    public float nextPos;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        nextPos = transform.position.y + upPoint;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < nextPos - 0.01f || transform.position.y > nextPos + 0.01f)
        {
            Vector3 buff = transform.position;
            buff.y = Mathf.Lerp(transform.position.y, nextPos, upRate * Time.deltaTime);
            transform.position = buff;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
