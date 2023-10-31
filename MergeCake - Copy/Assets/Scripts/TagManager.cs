using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TagManager : SingletoneBase<TagManager>
{
    public string TagReturn(string name)
    {

        Tags tag = (Tags)Enum.Parse(typeof(Tags), name);
        int returnNumber = (int)tag * 2;
        string ObjName = Enum.GetName(typeof(Tags), returnNumber);
        return ObjName;
    }
    public enum Tags : int
    {
        OneSlice = 2,
        TwoSlice = 4,
        FourSlice = 8,
        AllSlice=16,
        CompleteCake = 32,
        Last = 64
    }
}
