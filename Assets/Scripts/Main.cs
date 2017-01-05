using UnityEngine;
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
                string tantenBuffer = tantenData.ReadLine();

                //読み込んだ1行の文字を格納して改行
                roadStr += roadBuffer + Environment.NewLine;
                tantenStr += tantenBuffer + Environment.NewLine;
            }

            //格納した文字を改行で分割して配列に格納
            string[] roadArray = roadStr.Split('\n');
            string[] tantenArray = tantenStr.Split('\n');



            int r = 0;
            int t = 0;
            string pattern_start = @"^";
            string pattern_end = "$";
            int i = 0;


            while (r < roadArray.Length)
            {
                while (t < tantenArray.Length)
                {
                    //IndexOf("?")が読み込んだ文字にあるかないか
                    if (0 <= roadArray[r].IndexOf((tantenArray[t])))
                    {
                       pattern_start = pattern_start + tantenArray[t];
                        pattern_end = "@" + tantenArray[t] + pattern_end;
                        //Debug.Log(roadArray[r]);
                        //ある場合はpatternで最先頭または最後方であるか確かめる
                        if (Regex.IsMatch(roadArray[r], pattern_start))
                            {

                               
                           
                            }
                        else if(Regex.IsMatch(roadArray[r], pattern_end))
                        {
                            Debug.Log(roadArray[r].Trim());
                        }
                        i++;
                        //Debug.Log(i);
                    }

                    //Debug.Log(tantenArray[t]);
                    t = t + 1;
                }
                r = r + 1;
                t = 0;
            }
        }
    }
}