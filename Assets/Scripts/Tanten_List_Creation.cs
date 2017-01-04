using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Tanten_List_Creation : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Count用の変数　初期値は0
        int i = 0;

        //
        //道路の端点を格納して重複を無くした端点リスト(x,z)を作成する
        //
        // StreamRenderの新しいインスタンスを生成
        using (System.IO.StreamReader roadData2 = new System.IO.StreamReader("roaddata3.txt", System.Text.Encoding.UTF8))
        {
            //読み込んだ結果を格納する変数を宣言
            string roadStr2 = string.Empty;

            //読み込む文字がなくなるまで繰り返す
            while (roadData2.Peek() >= 0)
            {
                //"roaddata.txt"を１行ずつ読み込む
                string roadBuffer2 = roadData2.ReadLine().Trim();

                //読み込んだ1行の文字を格納して改行
                roadStr2 += roadBuffer2 + Environment.NewLine;
            }

            //格納した文字を改行で分割して配列に格納
            string[] roadArray2 = roadStr2.Split('\n').Distinct().ToArray();

            //変数の初期化
            int x_point = 0;
            int y_point = 0;

            //Tanten型のListを宣言
            List<Tanten> tantens = new List<Tanten>();

            //int v = 0;

            //while (v < roadArray2.Length)
            //{
            //    string item = string.Join("\n", roadArray2);
            //    UnityEngine.Debug.Log(item);
            //    string item1_1 = item.Trim();
            //    string[] roadArray3 = item1_1.Split(',');
            //    v = v + 1;
            //}


            foreach (string item in roadArray2)
            {
                //string item1_1 = item.Trim();
                string[] roadArray3 = item.Split(',');

                //デバッグ用
                foreach (string item2 in roadArray3)
                {
                    Debug.Log(item2);
                }
            }
            //for (int v = 1; v <= 5; v++)

            //    {
            //        string str_x_point = roadArray3[i];
            //        string str_y_point = roadArray3[i + 1];

            //        x_point = int.Parse(str_x_point);
            //        y_point = int.Parse(str_y_point);

            //        //ListにAddメソッドでTanten型の変数を順次格納していく
            //        tantens.Add(new Tanten(x_point, y_point));



            //    //2つずつ取り出しているのでcount + 2
            //        v = v + 2;
            //}

        }


        ////Tanten型のListに端点を格納する
        ////countの値がarrayの-1になるまでループ
        //while (i < this.roadArray3.Length - 1)
        //{
        //    string str_x_point = this.roadArray3[i];
        //    string str_y_point = this.roadArray3[i + 1];

        //    x_point = int.Parse(str_x_point);
        //    y_point = int.Parse(str_y_point);

        //    //ListにAddメソッドでTanten型の変数を順次格納していく
        //    tantens.Add(new Tanten(x_point, y_point));



        //    //2つずつ取り出しているのでcount+2
        //    i = i + 2;

        //}

        //foreach (Tanten item2 in tantens)
        //{
        //    UnityEngine.Debug.Log(item2.x + "," + item2.y);
        //}
    }




}


