﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

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

            //読み込む文字がなくなるまで繰り返す
            while (roadData.Peek() >= 0)
            {
                //"roaddata.txt"を１行ずつ読み込む
                string roadBuffer = roadData.ReadLine();

                //読み込んだ1行の文字を格納して改行
                roadStr += roadBuffer + Environment.NewLine;
            }

            //格納した文字をカンマと改行で分割して配列に格納
            string[] roadArray = roadStr.Split(',', '\n');

            //変数の初期化
            int x_point1 = 0;
            int y_point1 = 0;
            int x_point2 = 0;
            int y_point2 = 0;

            // Count用の変数　初期値は0
            int i = 0;

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


            ////デバック用
            //foreach (Road item in roads)
            //{
            //    Debug.Log(item.x1 + "," + item.y1 + "," + item.x2 + "," + item.y2);
            //}


            //
            ////道路の端点を格納して重複を削除した端点リスト(x,z)を作成する
            //
            //変数の初期化
            int x_point = 0;
            int y_point = 0;

            //Tanten型のListを宣言
            List<Tanten> tantens = new List<Tanten>();

            //Tanten型のListに端点を格納する
            //countの値がroadArrayの-2になるまでループ
            while (i < roadArray.Length - 2)
            {
                string str_x_point = roadArray[i];
                string str_y_point = roadArray[i + 1];

                x_point = int.Parse(str_x_point);
                y_point = int.Parse(str_y_point);

                //ListにAddメソッドでTanten型の変数を順次格納していく
                tantens.Add(new Tanten(x_point, y_point));

                //2つずつ取り出しているのでcount+2
                i = i + 2;

            }

            //重複を削除
            //IEnumerable<Tanten> distinct = tantens.Distinct().ToList();

            //////デバック用
            //foreach(Tanten item in distinct)
            //{
            //    Debug.Log(item.x + "," + item.y);
            //}
        }
    }
}
