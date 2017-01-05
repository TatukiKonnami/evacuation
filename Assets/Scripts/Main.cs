﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Main : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //
        ////道路の端点を格納した道路データリスト(x1,z1,x2,z2)を作成する
        //
        // StreamRenderの新しいインスタンスを生成
        using (System.IO.StreamReader roadData = new System.IO.StreamReader("roaddata2.txt", System.Text.Encoding.UTF8))
        {
            //読み込んだ結果を格納する変数を宣言
            string roadStr = string.Empty;

            var pattern = @"^0";

            //読み込む文字がなくなるまで繰り返す
            while (roadData.Peek() >= 0)
            {
                //"roaddata.txt2"を１行ずつ読み込む
                string roadBuffer = roadData.ReadLine();

                ////"roaddata.txt2"を１行ずつ読み込んで回すたびに削除
                //string roadBuffer_one = roadData.ReadLine();
                //roadBuffer_one = roadBuffer_one.Remove(0,11);
                //Debug.Log(roadBuffer_one);



                if (0 <= roadBuffer.IndexOf(("0,290")))
                {
                    //Debug.Log(roadBuffer);
                    if(Regex.IsMatch(roadBuffer,pattern))
                    {
                        Debug.Log(roadBuffer);
                    }
                }

                else
                {
                    //Debug.Log("ない");
                }

                //読み込んだ1行の文字を格納して改行
                roadStr += roadBuffer + Environment.NewLine;
            }




            //格納した文字をカンマと改行で分割して配列に格納
            string[] roadArray = roadStr.Split(',', '\n');



            // Count用の変数　初期値は0
            int i = 0;

            //変数の初期化
            int x_point1 = 0;
            int y_point1 = 0;
            int x_point2 = 0;
            int y_point2 = 0;

            //Road型のListを宣言
            List<Road> roads = new List<Road>();
            

            //Road型のListに端点のつながり（道）を格納する
            //countの値がroadArrayの-4になるまでループ
            while (i < roadArray.Length - 4)
            {
                string str_x_point1 = roadArray[i];
                string str_y_point1 = roadArray[i + 1];
                string str_x_point2 = roadArray[i + 2];
                string str_y_poiny2 = roadArray[i + 3];

                x_point1 = int.Parse(str_x_point1);
                y_point1 = int.Parse(str_y_point1);
                x_point2 = int.Parse(str_x_point2);
                y_point2 = int.Parse(str_y_poiny2);

                //ListにAddメソッドでRoad型の変数を順次格納していく
                roads.Add(new Road(x_point1, y_point1, x_point2, y_point2));
                
                //4つずつ取り出しているのでcount+4
                i = i + 4;


            }


            //デバック用
            //foreach (Road item in roads)
            //{
            //    //Debug.Log(item.x1 + "," + item.y1 + "," + item.x2 + "," + item.y2);
            //    if (0 <= roadss.IndexOf(("0,290")))
            //    {
            //        Debug.Log("ある");
            //        //読み込んだ1行の文字を格納して改行
            //        /*roadStr += roadBuffer + Environment.NewLine*/;
            //    }
            //}

        }









        //if (0 <= roadBuffer.IndexOf(("0,290")))
        //{
        //    Debug.Log("ある");
        //    //読み込んだ1行の文字を格納して改行
        //    roadStr += roadBuffer + Environment.NewLine;
        //}

        //else
        //{
        //    //Debug.Log("ない");
        //}

        //Debug.Log(roadStr);


        ////読み込む文字がなくなるまで繰り返す
        //while (roadData2.Peek() >= 0)
        //{
        //    //"Tanten_List.txt"を１行ずつ読み込む
        //    string roadBuffer2 = roadData2.ReadLine();
        //    //Debug.Log(roadBuffer2);

        //}





    }


}


