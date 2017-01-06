using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


public class Hash_Ceation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //Road型のListを宣言
        List<Road> roads = new List<Road>();

        //Tanten型のListを宣言
        List<Tanten> tantens = new List<Tanten>();

        ////Road型のListを宣言
        //List<Road> tantens_roads_forward_conversion = new List<Road>();

        ////Road型のListを宣言
        //List<Road> tantens_roads_backward_conversion = new List<Road>();

        using (System.IO.StreamReader roadData = new System.IO.StreamReader("Road_DataList.txt", System.Text.Encoding.UTF8))
        using (System.IO.StreamReader tantenData = new System.IO.StreamReader("Tanten_List.txt", System.Text.Encoding.UTF8))
        {
            //読み込んだ結果を格納する変数を宣言
            string roadStr = string.Empty;
            string tantenStr = string.Empty;

            //読み込む文字がなくなるまで繰り返す
            while (roadData.Peek() >= 0)
            {
                //"roaddata.txt"を１行ずつ読み込む
                string roadBuffer = roadData.ReadLine();

                //読み込んだ1行の文字を格納して改行
                roadStr += roadBuffer + Environment.NewLine;
            }



            //読み込む文字がなくなるまで繰り返す
            while (tantenData.Peek() >= 0)
            {
                //"tantendata.txt"を１行ずつ読み込む
                string tantenBuffer = tantenData.ReadLine();

                //読み込んだ1行の文字を格納して改行
                tantenStr += tantenBuffer + Environment.NewLine;
            }



            //格納した文字をカンマと改行で分割して配列に格納
            string[] roadArray = roadStr.Split(',', '\n');
            string[] tantenArray = tantenStr.Split(',', '\n');

            // Count用の変数　初期値は0
            int i = 0;
            int it = 0;
            int r = 0;
            int t = 0;

            //int fc = 0;
            //int bc = 0;

            //変数の初期化
            int x1_point = 0;
            int z1_point = 0;
            int x2_point = 0;
            int z2_point = 0;
            int x_point = 0;
            int z_point = 0;

            //int x_point_forward_conversion = 0;
            //int z_point_forward_conversion = 0;
            //int x_point_backward_conversion = 0;
            //int z_point_backward_conversion = 0;



            //Road型のListに端点のつながり（道）を格納する
            //countの値がroadArrayの-4になるまでループ
            while (r < roadArray.Length - 4)
            {
                string str_x1_point = roadArray[r];
                string str_z1_point = roadArray[r + 1];
                string str_x2_point = roadArray[r + 2];
                string str_z2_poiny = roadArray[r + 3];

                x1_point = int.Parse(str_x1_point);
                z1_point = int.Parse(str_z1_point);
                x2_point = int.Parse(str_x2_point);
                z2_point = int.Parse(str_z2_poiny);

                //ListにAddメソッドでRoad型の変数を順次格納していく
                roads.Add(new Road(x1_point, z1_point, x2_point, z2_point));

                //4つずつ取り出しているのでcount+4
                r = r + 4;
            }



            //Tanten型のListに端点を格納する
            //countの値がtantenArrayの - 2になるまでループ
            while (t < tantenArray.Length - 2)
            {
                string str_x_point = tantenArray[t];
                string str_z_point = tantenArray[t + 1];

                x_point = int.Parse(str_x_point);
                z_point = int.Parse(str_z_point);

                //ListにAddメソッドでTanten型の変数を順次格納していく
                tantens.Add(new Tanten(x_point, z_point));

                //2つずつ取り出しているのでcount+2
                t = t + 2;
            }


            ////Road型のListに端点を格納する
            ////countの値がtantenArrayの - 2になるまでループ
            //while (fc < tantenArray.Length - 2)
            //{
            //    string str_x_point_forward_conversion = tantenArray[fc];
            //    string str_z_point_forward_conversion = tantenArray[fc + 1];

            //    x_point_forward_conversion = int.Parse(str_x_point_forward_conversion);
            //    z_point_forward_conversion = int.Parse(str_z_point_forward_conversion);

            //    //ListにAddメソッドでTanten型の変数を順次格納していく
            //    tantens_roads_forward_conversion.Add(new Road(0, 0, x_point_forward_conversion, z_point_forward_conversion));

            //    //2つずつ取り出しているのでcount+2
            //    fc = fc + 2;
            //}


            ////Road型のListに端点を格納する
            ////countの値がtantenArrayの - 2になるまでループ
            //while (bc < tantenArray.Length - 2)
            //{
            //    string str_x_point_backward_conversion = tantenArray[bc];
            //    string str_z_point_backward_conversion = tantenArray[bc + 1];

            //    x_point_backward_conversion = int.Parse(str_x_point_backward_conversion);
            //    z_point_backward_conversion = int.Parse(str_z_point_backward_conversion);

            //    //ListにAddメソッドでTanten型の変数を順次格納していく
            //    tantens_roads_backward_conversion.Add(new Road(x_point_backward_conversion, z_point_backward_conversion, 0, 0));

            //    //2つずつ取り出しているのでcount+2
            //    bc = bc + 2;
            //}

            ////デバック用
            //foreach (Road item in tantens_roads_forward_conversion)
            //{
            //    Debug.Log(item.x1 + "," + item.z1 + "," + item.x2 + "," + item.z2);
            //}

            ////Dictionaryを作成する
            Dictionary<Tanten, Tanten> Kousaten = new Dictionary<Tanten, Tanten>();

            //int型の配列を宣言
            List<Tanten> roadsArray = new List<Tanten>();
            List<Tanten> tantensArray = new List<Tanten>();
            int mm = 0;
            int zz = 0;


            while (i < roads.Count)
            {
                while (it < tantens.Count)
                {
                    if (roads[i].x1 == tantens[it].x && roads[i].z1 == tantens[it].z)
                    {
                        mm = roads[i].x2;
                        zz = roads[i].z2;
                        
                        roadsArray.Add(new Tanten( mm, zz) );
                        
                        

                    }

                    if (roads[i].x2 == tantens[it].x && roads[i].z2 == tantens[it].z)
                    {
                       // Debug.Log(roads[i].x1 + "," + roads[i].z1);
                    }
                   
                    it++;
                }
                i++;
                it = 0;

            }
        }






        //CastRoad:取り出した要素を比較の都度、例：(*,*,0,290)または(0,290,*,*)にする
        //OK

        //Listから要素を取り出す



        ////比較する処理




        ////デバック用
        //foreach (Road item in roads)
        //{
        //    Debug.Log(item.x1 + "," + item.z1 + "," + item.x2 + "," + item.z2);
        //}

        //一致した要素を出力する

    }

}


