using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCharge : MonoBehaviour {

    [SerializeField]
    private ParticleSystem m_onPart;

    [SerializeField]
    private ParticleSystem m_popPart;

    public void TurnOn()
    {
        m_onPart.gameObject.SetActive(true);
        m_popPart.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        m_popPart.gameObject.SetActive(false);
    }

    public void TurnOff()
    {
        if (m_onPart.gameObject.activeInHierarchy)
        {
            m_onPart.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            m_onPart.gameObject.SetActive(false);
            m_popPart.gameObject.SetActive(true);
            m_popPart.Play(true);
            StartCoroutine(WaitToPop());
        }
    }

    public void SilentTurnOff()
    {
        m_onPart.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        m_onPart.gameObject.SetActive(false);
        m_popPart.gameObject.SetActive(true);
        m_popPart.Stop(true);
    }

    IEnumerator WaitToPop()
    {
        yield return new WaitForSeconds(5.0f);
        m_popPart.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
