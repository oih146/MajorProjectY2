using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {
    public Text[] workers;
    float timer = 0.1f;
    public float speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene(0); }

        if (timer < 0)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i].transform.position += Vector3.down * speed;
                if (workers[i].transform.position.y < -1300)
                {
                    workers[i].transform.position = new Vector3(0, 400, 0);
                    Debug.Log(i);
                }
            }

            timer = 0.01f;
        }

        //if (Input.GetKey(KeyCode.Space)) speed = Mathf.MoveTowards(speed, 0.05f + speedInc, 0.25f * Time.deltaTime);
        //if (Input.GetKeyUp(KeyCode.Space)) speed = Mathf.MoveTowards(speed, 0.05f, 0.25f * Time.deltaTime);

        timer -= Time.deltaTime;
    }
}
