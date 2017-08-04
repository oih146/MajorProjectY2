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
        //gameObject.transform.localPosition = new Vector3(-220, 40, 1);
        nextPos = transform.localPosition.y + upPoint;

	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y < nextPos - 0.01f || transform.localPosition.y > nextPos + 0.01f)
        {
            Vector3 buff = transform.localPosition;
            buff.y = Mathf.Lerp(transform.localPosition.y, nextPos, upRate * Time.deltaTime);
            transform.localPosition = buff;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
