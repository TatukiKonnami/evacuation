//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;
//using System;

//public class Hash_Creation_Vector : MonoBehaviour
//{

//    // Use this for initialization
//    void Start()
//    {

//        List<Vector2> roads = new List<Vector2>();

//        List<Vector2> tantens = new List<Vector2>();


//        using (System.IO.StreamReader roadData = new System.IO.StreamReader("Road_DataList.txt", System.Text.Encoding.UTF8))
//        using (System.IO.StreamReader tantenData = new System.IO.StreamReader("Tanten_List.txt", System.Text.Encoding.UTF8))
//        {
//            //読み込んだ結果を格納する変数を宣言
//            string roadStr = string.Empty;
//            string tantenStr = string.Empty;

//            //読み込む文字がなくなるまで繰り返す
//            while (roadData.Peek() >= 0)
//            {
//                //"roaddata.txt"を１行ずつ読み込む
//                string roadBuffer = roadData.ReadLine();

//                //読み込んだ1行の文字を格納して改行
//                roadStr += roadBuffer + Environment.NewLine;
//            }

//            //読み込む文字がなくなるまで繰り返す
//            while (tantenData.Peek() >= 0)
//            {
//                //"tantendata.txt"を１行ずつ読み込む
//                string tantenBuffer = tantenData.ReadLine();

//                //読み込んだ1行の文字を格納して改行
//                tantenStr += tantenBuffer + Environment.NewLine;
//            }

//            //格納した文字をカンマと改行で分割して配列に格納
//            string[] roadArray = roadStr.Split(',', '\n');
//            string[] tantenArray = tantenStr.Split(',', '\n');
//        }






//    }
//}
