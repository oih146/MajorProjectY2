using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSlider : MonoBehaviour {

    public bool m_isOpen = false;
    Animator m_animator;

	// Use this for initialization
	void Start () {
        m_animator = gameObject.GetComponent<Animator>();
        Debug.Log(m_animator);
	}

    public void SetOpenClosed()
    {
        m_isOpen = !m_isOpen;

        if (m_isOpen)
            m_animator.Play("OpenState");
        else
            m_animator.Play("ClosedState");

    }
}
