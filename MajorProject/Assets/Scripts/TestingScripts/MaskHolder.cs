using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskHolder : MonoBehaviour {

    //To Set Mask in Dialouge manager (Squence)
    //Set based on entryID
    //SendMessage(functionName(SetMask), parameter(evil or chaos), gameObjectThatScriptIsOne(Testing));

    public Sprite m_chaosMask;
    public Sprite m_evilMask;
    public static Sprite m_evil;
    public static Sprite m_chaos;
    public static int childnum;
    public static Dictionary<int, string> masks = new Dictionary<int, string>();

    void Start()
    {
        childnum = 0;
        m_evil = m_evilMask;
        m_chaos = m_chaosMask;
    }

    public void RecountChildren()
    {
        childnum = 0;
    }

    public void SetMask(string alignment)
    {
        alignment = alignment.ToLower();
        masks.Add(childnum, alignment);
        childnum++;
    }

    public static Sprite GetMask(int key)
    {
        //template UI increments
        //minus to equalize
        //key--;
        string alignment;
        masks.TryGetValue(key, out alignment);
        if(alignment == "law")
        {
            return m_chaos;
        }
        else if(alignment == "light")
        {
            return m_evil;
        }
        return m_evil;
    }
}
