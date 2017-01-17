using UnityEngine;
using System.Collections;

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
        hand_positions = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        parent.SendMessage("Handposition", hand_positions);
    }


}
