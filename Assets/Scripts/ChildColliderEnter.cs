using UnityEngine;
using System.Collections;

public class ChildColliderEnter : MonoBehaviour
{

    GameObject parent;

    void Start()
    {
        //親(Main)オブジェクトを取得
        parent = gameObject.transform.parent.parent.parent.parent.parent.gameObject;
    }

    //OnCollisionEnterは物体と物体が衝突したときに呼び出される関数
    void OnCollisionEnter(Collision collision)
    {
        parent.SendMessage("RedirectedOnCollisionEnter", collision);
    }

    //OnCollisionExitは物体と物体が衝突した後に、物体から離れたときに呼び出される関数
    void OnCollisionExit(Collision collision)
    {
        parent.SendMessage("RedirectedOnCollisionExit", collision);
    }
}