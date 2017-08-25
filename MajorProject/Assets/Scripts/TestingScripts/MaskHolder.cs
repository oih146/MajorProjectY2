using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskHolder : MonoBehaviour {

    public Sprite evilMask;
    public Sprite goodMask;
    public static Sprite good;
    public static Sprite evil;
    public static int childnum;
    public static Dictionary<int, string> masks = new Dictionary<int, string>();

    void Start()
    {
        childnum = 0;
        good = goodMask;
        evil = evilMask;
    }

    public void RecountChildren()
    {
        childnum = 0;
    }

    public void SetMask(string alignment)
    {
        alignment = alignment.ToLower();
        masks.Add(0, alignment);
        childnum++;
    }

    public static Sprite GetMask(int key)
    {
        string alignment;
        masks.TryGetValue(key, out alignment);
        if(alignment == "evil")
        {
            return evil;
        }
        else if(alignment == "light")
        {
            return good;
        }
        return good;
    }
}
