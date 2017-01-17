using UnityEngine;
using System.Collections;

//palm用
public class HandPosition : MonoBehaviour
{
    GameObject parent;
    Vector3 hand_positions;

    void Start()
    {
        //親(OSVR_Leap_Player3)オブジェクトを取得
        parent = gameObject.transform.parent.parent.parent.parent.parent.gameObject;
    }

    void Update()
    {
        //palmの現在座標を取得する
        hand_positions = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //親(OSVR_Leap_Player3)のHandpositionメソッドにpalmの現在座標を飛ばす
        //（対象メソッド,引数）
        parent.SendMessage("Handposition", hand_positions);
    }
}
