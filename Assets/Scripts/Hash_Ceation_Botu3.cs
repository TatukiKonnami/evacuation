//using UnityEngine;
//using System.Collections;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;

//public class Hash_Ceation_Botu3 : MonoBehaviour
//{

//    // Use this for initialization
//    void Start()
//    {
//        //
//        ////道路の端点を格納した道路データリスト(x1,z1,x2,z2)を作成する
//        //
//        // StreamRenderの新しいインスタンスを生成
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
//                string tantenBuffer = tantenData.ReadLine();

//                //読み込んだ1行の文字を格納して改行
//                roadStr += roadBuffer + Environment.NewLine;
//                tantenStr += tantenBuffer + Environment.NewLine;
//            }

//            //格納した文字をカンマと改行で分割して配列に格納
//            string[] roadArray = roadStr.Split(',', '\n');
//            string[] tantenArray = tantenStr.Split(',', '\n');

//            // Count用の変数　初期値は0
//            int r = 0;
//            int t = 0;

//            //変数の初期化
//            int x1_point = 0;
//            int z1_point = 0;
//            int x2_point = 0;
//            int z2_point = 0;
//            int x_point = 0;
//            int z_point = 0;

//            //Road型のListを宣言
//            List<Road> roads = new List<Road>();

//            //Tanten型のListを宣言
//            List<Tanten> tantens = new List<Tanten>();

//            //Road型のListに端点のつながり（道）を格納する
//            //countの値がroadArrayの-4になるまでループ
//            while (r < roadArray.Length - 4)
//            {
//                string str_x1_point = roadArray[r];
//                string str_z1_point = roadArray[r + 1];
//                string str_x2_point = roadArray[r + 2];
//                string str_z2_poiny = roadArray[r + 3];

//                x1_point = int.Parse(str_x1_point);
//                z1_point = int.Parse(str_z1_point);
//                x2_point = int.Parse(str_x2_point);
//                z2_point = int.Parse(str_z2_poiny);

//                //ListにAddメソッドでRoad型の変数を順次格納していく
//                roads.Add(new Road(x1_point, z1_point, x2_point, z2_point));

//                //4つずつ取り出しているのでcount+4
//                r = r + 4;
//            }

//            //Tanten型のListに端点を格納する
//            //countの値がtantenArrayの-2になるまでループ
//            while (t < tantenArray.Length - 2)
//            {
//                string str_x_point = tantenArray[t];
//                string str_z_point = tantenArray[t + 1];

//                x_point = int.Parse(str_x_point);
//                z_point = int.Parse(str_z_point);

//                //ListにAddメソッドでTanten型の変数を順次格納していく
//                tantens.Add(new Tanten(x_point, z_point));

//                //2つずつ取り出しているのでcount+2
//                t = t + 2;
//            }

//            //foreach (Road item in roads)
//            //{
//            //    Debug.Log(item.x1 + "," + item.y1 + "," + item.x2 + "," + item.y2);
//            //}
//        }

//    }

//}
