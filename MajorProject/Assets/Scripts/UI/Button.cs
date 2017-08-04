using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public enum Buttons { Start, Exit }
public enum Transitions { Pulse, ColorTransition}

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Buttons buttonType;
    public Transitions transType;
    Color defaultColour;
    bool pulseActive = false;
    bool grow = true;
    float min;
    float max;
    public float difference = 1.0f;
    public float speed = 5.0f;
    bool mClick;
    static bool mDown;

    void Awake()
    {
        min = 1 - difference;
        max = 1 + difference;
        defaultColour = GetComponent<Image>().color;
        mClick = false;

    }
    void Update()
    {
        if (pulseActive == true) Pulse();
        else
        {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, 1.0f, speed * Time.deltaTime), Mathf.MoveTowards(transform.localScale.y, 1.0f, speed * Time.deltaTime), 0);
            grow = true;
        }

        mClick = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Vector4(0.85f, 0.85f, 0.85f, 1.0f);
        //pulseActive = false;
        mDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().color = defaultColour;
        mDown = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!mDown) pulseActive = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!mDown) pulseActive = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        mClick = true;
    }
    void Pulse()
    {
        if (transform.localScale.x >= max) grow = false;
        if (transform.localScale.x <= min) grow = true;
        if(grow == true ) transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, max, speed * Time.deltaTime), Mathf.MoveTowards(transform.localScale.y, max, speed * Time.deltaTime), 0);
        if(grow == false) transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, min, speed * Time.deltaTime), Mathf.MoveTowards(transform.localScale.y, min, speed * Time.deltaTime), 0);
    }

    public bool ReturnClick()
    {
        if (mClick)
        {
            mClick = false;
            return true;
        }
        return false;
    }

}