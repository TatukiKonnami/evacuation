using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
using System.Collections.Generic;
using System;

public class Leap_Hand_Acquisition : MonoBehaviour {
    [SerializeField]
    GameObject m_ProviderObject;

    LeapServiceProvider m_Provider;

	// Use this for initialization
	void Start () {
        m_Provider = m_ProviderObject.GetComponent<LeapServiceProvider>();	

	}
	
	// Update is called once per frame
	void Update () {
        Frame frame = m_Provider.CurrentFrame;

        //右手を取得する
        Hand rightHand = null;
        foreach(Hand hand in frame.Hands)
        {
            if(hand.IsRight)
            {
                rightHand = hand;
                break;
            }
        }

        if(rightHand == null)
        {
            return;
        }


        
        //右手の向き
        Vector3 direction = rightHand.Direction.ToVector3();




        //ライトアームオブジェクト


        //コンソール出力
        Debug.Log(direction);


    }
}
