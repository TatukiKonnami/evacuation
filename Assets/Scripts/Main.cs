using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Main : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        using (System.IO.StreamReader roadData = new System.IO.StreamReader("Road_DataList.txt", System.Text.Encoding.UTF8))
        using (System.IO.StreamReader roadData2 = new System.IO.StreamReader("Tanten_List.txt", System.Text.Encoding.UTF8))
        {

            //読み込む文字がなくなるまで繰り返す
            while (roadData.Peek() >= 0)
            {
                //"Road_DataList.txt"を１行ずつ読み込む
                string roadBuffer = roadData.ReadLine();
                Debug.Log(roadBuffer);
            }

            //読み込む文字がなくなるまで繰り返す
            while (roadData2.Peek() >= 0)
            {
                //"Tanten_List.txt"を１行ずつ読み込む
                string roadBuffer2 = roadData2.ReadLine();
                //Debug.Log(roadBuffer2);

            }


        }


    }
}

