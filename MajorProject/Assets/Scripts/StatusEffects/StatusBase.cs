﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBase : ScriptableObject {

    public eEffects m_effectType;
    protected float m_timeActive = 0;
    protected float m_strength = 0;

    public float TimeActive { get { return m_timeActive; } set { m_timeActive = value; } }
    public float Strength { get { return m_strength; } set { m_strength = value; } }
    public bool IsActive { get { return m_timeActive > 0 ? true : false; } }

    public StatusBase Init(float p_timeActive, float p_strength)
    {
        TimeActive = p_timeActive;
        Strength = p_strength;
        return this;
    }

    void Start()
    {

    }

    /*Is the time up?*/
    public bool TakeAndCheckActive()
    {
        TakeTime();
        return CheckTime();
    }

    public void TakeTime()
    {
        TimeActive--;
    }

    public bool CheckTime()
    {
        if (TimeActive <= 0)
            return true;
        return false;
    }


    //Virtual Functions
    //------------------------------------------------------------

    public virtual void OnApply(CharacterStatSheet applyTo)
    {
        applyTo.GetEffectArray()[(int)m_effectType] = this;
        //applyTo.GetEffectArray()[(int)m_effectType].m_effectType = this.m_effectType;
        //applyTo.GetEffectArray()[(int)m_effectType].m_strength = this.Strength;
        //applyTo.GetEffectArray()[(int)m_effectType].m_timeActive = this.m_timeActive;
    }

    public virtual void Remove(CharacterStatSheet removeFrom)
    {

    }

    public virtual void Use(CharacterStatSheet useOn)
    {

    }

    public virtual void Setup(CharacterStatSheet attacker)
    {

    }

    public virtual void OnUpdate(CharacterStatSheet useOn)
    {

    }
}
