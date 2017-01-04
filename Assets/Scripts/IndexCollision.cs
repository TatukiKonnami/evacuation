using UnityEngine;
using System.Collections;

public class IndexCollision : MonoBehaviour
{
    //index専用isIndexTouchを変えれば他の指親にアタッチ可
    public static bool isIndexTouch;

    void RedirectedOnCollisionEnter(Collision collision)
    {
        //衝突したらtrue
        isIndexTouch = true;
        Debug.Log(isIndexTouch);
    }

    void RedirectedOnCollisionExit(Collision collision)
    {
        //衝突後離れたらfalse
        isIndexTouch = false;
        Debug.Log(isIndexTouch);
    }
}