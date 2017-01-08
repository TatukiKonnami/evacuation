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

            //変数の初期化
            int x1_point = 0;
            int z1_point = 0;
            int x2_point = 0;
            int z2_point = 0;
            int x_point = 0;
            int z_point = 0;

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

            ////Dictionaryを作成する
            Dictionary<Tanten, Vector2> interSection = new Dictionary<Tanten, Vector2>();

            //Tanten型のListを宣言
            List<Tanten> road_temporary_x2z2 = new List<Tanten>();
            List<Tanten> road_temporary_x1z1 = new List<Tanten>();

            ////変数の初期化
            //int x2_temporary = 0;
            //int z2_temporary = 0;
            //int x1_temporary = 0;
            //int z1_temporary = 0;


            //
            //countの値がroadsに格納されている要素の数になるまでループ
            while (i < roads.Count)
            {
                //countの値がtantensに格納されている要素の数になるまでループ
                while (it < tantens.Count)
                {
                    //roads(x1,z1,x2,z2)のx1,z1とtentens(x,z)が一致するかどうか
                    if (roads[i].x1 == tantens[it].x && roads[i].z1 == tantens[it].z)
                    {
                        //x2_temporary = roads[i].x2;
                        //z2_temporary = roads[i].z2;
                        //roads_temporary_x2z2.Add(new Tanten(x2_temporary, z2_temporary));

                        //複数の要素を保存できるようにする
                        //同じkeyを持つ要素が既に辞書に存在しても追加保存する


                        Vector2 roads_temporary_x2z2 = new Vector2(roads[i].x2, roads[i].z2);
                        interSection.Add(tantens[it], roads_temporary_x2z2);

                    }
                    //roads(x1,z1,x2,z2)のx2,z2とtentens(x,z)が一致するかどうか
                    if (roads[i].x2 == tantens[it].x && roads[i].z2 == tantens[it].z)
                    {
                        //x1_temporary = roads[i].x1;
                        //z1_temporary = roads[i].z1;
                        //roads_temporary_x1z1.Add(new Tanten(x1_temporary, z1_temporary));

                        Vector2 roads_temporary_x1z1 = new Vector2(roads[i].x1, roads[i].z1);
                        interSection.Add(tantens[it], roads_temporary_x1z1);
                    }

                    it++;
                }
                i++;
                it = 0;

            }

            Debug.Log(interSection);

        }

    }



}
