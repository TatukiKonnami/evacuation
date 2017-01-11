using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    public GameObject prefab;

    // Use this for initialization
    void Start()
    {
        //Road型のListを宣言
        List<Road> roads = new List<Road>();

        //Tanten型のListを宣言
        List<Tanten> tantens = new List<Tanten>();
        List<Tanten> ikisakis = new List<Tanten>();

        int k = Int32.MaxValue;

        using (System.IO.StreamReader roadData = new System.IO.StreamReader("Road_DataList.txt", System.Text.Encoding.UTF8))
        using (System.IO.StreamReader tantenData = new System.IO.StreamReader("Tanten_List.txt", System.Text.Encoding.UTF8))
        {
            roads = CreationRoadList(roadData);
            tantens = CreationTantenList(tantenData);
        }

        //現在地を取得してgenzaichiに格納（Unity)
        //IntCastメソッド：transform.positionをint型にキャストする
        Tanten genzaichi = IntCast(transform.position.x, transform.position.z);

        //genzaichiのx座標を絶対値化する処理
        genzaichi = MinusDelete(genzaichi);

        //Ikisakiメソッド：genzaichiをroadsリストから検索
        //続き：繋がっている端点をikisakis（リスト）に格納
        ikisakis = Ikisaki(genzaichi, roads);

        //ikisakis（リスト）に格納されている座標（端点）に選択肢UIを表示（Unity)
        int i = 0;
        while (i < ikisakis.Count)
        {
            Instantiate(prefab, new Vector3(-ikisakis[i].x, 1.5f, ikisakis[i].z), Quaternion.identity);
            i++;
        }

        //LeapMotionで選択（Unity）

        //選択した端点をsentakumichiに格納（Unity）
        Tanten sentakumichi = new Tanten(-124, 359);

        //sentakumichiのx座標を絶対値化する処理
        sentakumichi = MinusDelete(sentakumichi);

        //Ikisakiメソッド：sentakumichiをroadsリストから検索
        //続き：繋がっている端点をikisakis（リスト）に格納
        ikisakis = Ikisaki(sentakumichi, roads);

        //KousatenHantenメソッドから交差点or中点or行き止まりorエラーを判定
        k = KousatenHanten(ikisakis);

        //中点時の処理
        //未Debug
        while (k == 0)
        {
            int it = 0;
            while (it < ikisakis.Count)
            {

                if (genzaichi.x != ikisakis[it].x && genzaichi.z != ikisakis[it].z)
                {
                    sentakumichi = ikisakis[it];

                }

                it++;
            }
            ikisakis = Ikisaki(sentakumichi, roads);
            k = KousatenHanten(ikisakis);
        }

        //sentakumichiのx座標にマイナスを付ける処理
        sentakumichi = MinusAdd(sentakumichi);

        //ナビゲーションシステムで移動する処理(Unity)
        //行き先はsentakumichiに格納されている
        Idou(sentakumichi);



    }

   


    // Update is called once per frame
    void Update()
    {

    }

    // 以下Method　

    //textデータをRoad型リストにする
    private List<Road> CreationRoadList(StreamReader textData)
    {
        List<Road> textList = new List<Road>();

        //読み込んだ結果を格納する変数を宣言
        string textStr = string.Empty;

        //読み込む文字がなくなるまで繰り返す
        while (textData.Peek() >= 0)
        {
            //"roaddata.txt"を１行ずつ読み込む
            string textBuffer = textData.ReadLine();

            //読み込んだ1行の文字を格納して改行
            textStr += textBuffer + Environment.NewLine;
        }
        //格納した文字をカンマと改行で分割して配列に格納
        string[] textArray = textStr.Split(',', '\n');

        int i = 0;

        int x1_point = 0;
        int z1_point = 0;
        int x2_point = 0;
        int z2_point = 0;

        while (i < textArray.Length - 4)
        {
            string str_x1_point = textArray[i];
            string str_z1_point = textArray[i + 1];
            string str_x2_point = textArray[i + 2];
            string str_z2_poiny = textArray[i + 3];

            x1_point = int.Parse(str_x1_point);
            z1_point = int.Parse(str_z1_point);
            x2_point = int.Parse(str_x2_point);
            z2_point = int.Parse(str_z2_poiny);

            //ListにAddメソッドでRoad型の変数を順次格納していく
            textList.Add(new Road(x1_point, z1_point, x2_point, z2_point));

            //4つずつ取り出しているのでcount+4
            i = i + 4;
        }

        return textList;
    }

    //textデータをTanten型リストにする
    private List<Tanten> CreationTantenList(StreamReader textData)
    {
        List<Tanten> textList = new List<Tanten>();

        //読み込んだ結果を格納する変数を宣言
        string textStr = string.Empty;

        //読み込む文字がなくなるまで繰り返す
        while (textData.Peek() >= 0)
        {
            //"roaddata.txt"を１行ずつ読み込む
            string textBuffer = textData.ReadLine();

            //読み込んだ1行の文字を格納して改行
            textStr += textBuffer + Environment.NewLine;
        }
        //格納した文字をカンマと改行で分割して配列に格納
        string[] textArray = textStr.Split(',', '\n');

        int i = 0;

        int x1_point = 0;
        int z1_point = 0;

        while (i < textArray.Length - 2)
        {
            string str_x1_point = textArray[i];
            string str_z1_point = textArray[i + 1];

            x1_point = int.Parse(str_x1_point);
            z1_point = int.Parse(str_z1_point);

            //ListにAddメソッドでRoad型の変数を順次格納していく
            textList.Add(new Tanten(x1_point, z1_point));

            //4つずつ取り出しているのでcount+4
            i = i + 2;
        }

        return textList;
    }

    private List<Tanten> Ikisaki(Tanten genzaichi, List<Road> roads)
    {
        List<Tanten> IkisakiList = new List<Tanten>();

        int i = 0;

        while (i < roads.Count)
        {
            if (genzaichi.x == roads[i].x1 && genzaichi.z == roads[i].z1)
            {
                IkisakiList.Add(new Tanten(roads[i].x2, roads[i].z2));
            }

            if (genzaichi.x == roads[i].x2 && genzaichi.z == roads[i].z2)
            {
                IkisakiList.Add(new Tanten(roads[i].x1, roads[i].z1));
            }

            i++;
        }

        return IkisakiList;
    }

    //交差点を判定する
    //交差点：1,中点:0,行き止まり:2,エラー:-1
    private int KousatenHanten(List<Tanten> ikisakis)
    {
        if (ikisakis.Count >= 3)
        {
            return 1;
        }
        else if (ikisakis.Count == 2)
        {
            return 0;
        }
        else if (ikisakis.Count == 1)
        {
            return 2;
        }
        else
        {
            return -1;
        }


    }

    //Tanten型の値を絶対値化する
    private Tanten MinusDelete(Tanten a)
    {
        int x = System.Math.Abs(a.x);

        int z = System.Math.Abs(a.z);

        Tanten plus = new Tanten(x, z);

        return plus;
    }

    //Tanten型のxの値をマイナス化する
    private Tanten MinusAdd(Tanten a)
    {
        int x = -a.x;

        int z = a.z;

        Tanten minus = new Tanten(x, z);

        return minus; 
    }

    //float型をint型に変換
    private Tanten IntCast(float a, float b)
    {
        int x = (int)a;
        int z = (int)b;

        Tanten ints = new Tanten(x, z);

        return ints;
    }

    private void Idou(Tanten sentakumichi)
    {
        
    }

}
