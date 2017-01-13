using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
    public static bool uis;

    // Use this for initialization
    void Start () {
        uis = false;
	
	}

    //OnCollisionEnterは物体と物体が衝突したときに呼び出される関数
    void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Player")
        {
            uis = true;
            Debug.Log(uis);
        }
    }


}
