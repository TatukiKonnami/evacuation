using UnityEngine;
using System;
using System.Collections;

public class Hikaku_Test : MonoBehaviour {
    string s1 = "あいうえおかきくけこ";

    void Start()
    {

        Debug.Log(s1.IndexOf("うえお"));
    }

    
}
