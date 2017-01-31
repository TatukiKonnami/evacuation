using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class Prototype_Maine : MonoBehaviour
{
    public Tanten position;
    private float intervalTime = 0.0f;
    int itt = 0;
    public GameObject uis;
    static Vector3 hand_position;
    UnityEngine.Object clones;
    bool isRunning = false;
    Tanten tugimichi;
    Tanten imaichi;
    Vector3 fingerposition;
    //Road型(int x1, int z1, int x2, int z2)のListを宣言
    List<Road> roads = new List<Road>();
    //Tanten型(int x, int z)のListを宣言
    List<Tanten> ikisakis = new List<Tanten>();
    List<Tanten> ikisakiss = new List<Tanten>();
    //Sentaku型(float x1,float z1, int x2,int z2)のListを宣言
    List<Sentaku> sentakus = new List<Sentaku>();
    //UI判定用のリストを作成
    List<Vector3> uiv = new List<Vector3>();
    //Tanten型(int x, int z)の変数を宣言
    Tanten ikisakiT = new Tanten(0, 0);
    Tanten sentakumichi = new Tanten(0, 0);
    Vector3 uin;
    int k = Int32.MaxValue;
    public UiPoint ui;
    public bool uipointr;
    public bool fixintersection;
    public Tanten totyuumichi;

    void Awake()
    {
        //"Road_DataList.txtからroadsリストを作成する
        using (System.IO.StreamReader roadData = new System.IO.StreamReader("Road_DataList.txt", System.Text.Encoding.UTF8))
        {
            roads = CreationRoadList(roadData);
        }

    }

    // Use this for initialization
    void Start()
    {
        position = new Tanten(-91, 252);
        InitialProcessing();
    }

    void InitialProcessing()
    {
        imaichi = position;
        //positionのx座標を絶対値化する処理
        position = MinusDelete(position);
        //Ikisakiメソッド：initialpositionをroadsリストから検索
        //続き：繋がっている端点をikisakis（リスト）に格納
        ikisakis = Ikisaki(position, roads);
        //UI関連の処理
        UIProcessing();
        //UiSummoningメソッド：UIを出す
        UiSummoning(uiv);
    }

    void UIProcessing()
    {
        int itr = 0;
        while (itr < ikisakis.Count)
        {
            //ikisakis（リスト）をikisakiTに格納
            ikisakiT = ikisakis[itr];
            //Kakudoメソッド：座標間の角度を出す
            double kakudo = Kakudo(position, ikisakiT);
            //UIZahyouメソッド：initialpositionからkakudoの角度の直線状の位置を出す
            UiPoint ui = UIZahyou(kakudo, position);
            //UI座標から次の端点を検索するためのリストに追加
            sentakus.Add(new Sentaku(-ui.x, ui.z, ikisakiT.x, ikisakiT.z));
            //UI判定用のリストにUIの範囲を追加
            uiv.Add(new Vector3(-ui.x, 1.5f, ui.z));
            itr++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //手の座標取得
        fingerposition = hand_position;
        //判定処理
        bool uipointr = CollisionDetection(fingerposition, uiv);
        if (uipointr == true)
        {
            intervalTime += Time.deltaTime;
            if (intervalTime >= 2f)
            {
                //UI削除
                UIDestroy();
                //sentakusリストからUI座標を検索して選択した端点を返す
                sentakumichi = SelectionSearch(uin, sentakus);
                //移動処理
                StartCoroutine("move");
                intervalTime = 0.0f;
            }
        }
        //if (uipointr == false)
        //{
        //    intervalTime = 0.0f;
        //}
    }

    IEnumerator move()
    {
        if (isRunning)
            yield break;
        isRunning = true;
        //交差点を探す処理
        IntersectionSearch();
        ikisakis.Clear();
        //positionのx座標を絶対値化する処理
        position = MinusDelete(sentakumichi);
        //Ikisakiメソッド：positionをroadsリストから検索、繋がっている端点をikisakis（リスト）に格納     
        ikisakis = Ikisaki(position, roads);
        //交差点から選択肢それぞれが距離：2の位置に表示
        sentakus.Clear();
        uiv.Clear();
        //UI関連の処理
        UIProcessing();
        UiSummoning(uiv);
        ikisakis.Clear();
        ikisakiss.Clear();
        isRunning = false;
        yield return null;
    }

    void IntersectionSearch()
    {
        //sentakumichiのx座標を絶対値化する処理
        sentakumichi = MinusDelete(sentakumichi);
        //Ikisakiメソッド：sentakumichiをroadsリストから検索
        //続き：繋がっている端点をikisakis（リスト）に格納
        ikisakis = Ikisaki(sentakumichi, roads);
        //KousatenHantenメソッドから交差点or中点or行き止まりorエラーを判定
        k = KousatenHanten(ikisakis);
        //MinusAddメソッド：sentakumichiのx座標にマイナスを付ける処理
        sentakumichi = MinusAdd(sentakumichi);
        //交差点時の処理
        Itp();
    }

    void Itp()
    {
        if (k == 0)
        {
            while (k <= 0)
            {
                Navigate();
                int it = 0;
                while (it < ikisakis.Count)
                {
                    if (imaichi.x != -ikisakis[it].x && imaichi.z != ikisakis[it].z)
                    {
                        sentakumichi = ikisakis[it];
                    }
                    it++;
                }
                ikisakiss = Ikisaki(sentakumichi, roads);
                k = KousatenHanten(ikisakiss);
                if (k == 1 || k == 2)
                {
                    imaichi = sentakumichi;
                    //MinusAddメソッド：sentakumichiのx座標にマイナスを付ける処理
                    sentakumichi = MinusAdd(sentakumichi);
                    Navigate();
                    break;
                }
                sentakumichi = MinusAdd(sentakumichi);
            }

        }
        else if (k == 1 || k == 2)
        {
            imaichi = sentakumichi;
            //sentakumichi座標をgoal（行き先）に格納し、navmeshagentに目的地を取得させる
            Navigate();
        }
    }

    void Navigate()
    {
        var agent = GetComponent<NavMeshAgent>();
        Vector3 goal;
        goal = new Vector3(sentakumichi.x, 1.5f, sentakumichi.z);
        //Debug.Log(sentakumichi.x + "," + sentakumichi.z);
        agent.destination = goal;
    }

    void UIDestroy()
    {
        GameObject[] uid = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject obj in uid)
        {
            Destroy(obj);
        }
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

    private List<Tanten> Ikisaki(Tanten sentakumichi, List<Road> roads)
    {
        List<Tanten> IkisakiList = new List<Tanten>();

        int i = 0;

        while (i < roads.Count)
        {

            if (sentakumichi.x == roads[i].x1 && sentakumichi.z == roads[i].z1)
            {
                IkisakiList.Add(new Tanten(roads[i].x2, roads[i].z2));
                //Debug.Log(roads[i].x2 + "," + roads[i].z2);
            }

            if (sentakumichi.x == roads[i].x2 && sentakumichi.z == roads[i].z2)
            {
                IkisakiList.Add(new Tanten(roads[i].x1, roads[i].z1));
                //Debug.Log(roads[i].x1 + "," + roads[i].z1);
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

    //float型の値を絶対値化する
    private float MinusDeletef(float a)
    {
        float x = System.Math.Abs(a);




        return x;
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

    //座標間の角度を返す
    private double Kakudo(Tanten sentakumichi, Tanten ikisakiT)
    {
        double kakudo = 0.0;

        double x1 = sentakumichi.x;
        double z1 = sentakumichi.z;
        double x2 = ikisakiT.x;
        double z2 = ikisakiT.z;

        kakudo = Math.Atan2(z2 - z1, x2 - x1);


        return kakudo;
    }

    //HandpositionスクリプトからSendMessage
    //そのスクリプトが格納されているオブジェクトの座標を取得
    private void Handposition(Vector3 hand_positions)
    {
        hand_position = hand_positions;
    }

    //座標と角度からdistanceで指定した距離分離れている場所の座標を返す
    private UiPoint UIZahyou(double kakudo, Tanten sentakumichi)
    {
        UiPoint ui;

        double x1 = sentakumichi.x;
        double z1 = sentakumichi.z;

        double distance = 0.3;
        double x1t = Math.Cos(kakudo) * distance;
        double z1t = Math.Sin(kakudo) * distance;

        float x1f = (float)x1t + (float)x1;
        float z1f = (float)z1t + (float)z1;

        ui = new UiPoint(x1f, z1f);



        return ui;
    }

    //手の座標とUIのリストから距離を計算し、rで指定した値より距離が小さい場合（手がUIに一定距離近づいたら）trueで
    //uinに、近づいたUIの座標値を格納する
    private bool CollisionDetection(Vector3 fingerposition, List<Vector3> uiv)
    {
        int i = 0;
        while (i < uiv.Count)
        {
            UiPoint ff = new UiPoint(fingerposition.x, fingerposition.z);
            UiPoint vv = new UiPoint(uiv[i].x, uiv[i].z);
            float p = Kyori(ff, vv);
            float r = 0.1f;
            if (p <= r)
            {
                uin = new Vector3(uiv[i].x, uiv[i].y, uiv[i].z);

                return true;
            }
            i++;
        }
        return false;

    }

    //2つの座標値を与えて距離返す
    private float Kyori(UiPoint ff, UiPoint vv)
    {
        double kyori;
        float x1x2 = ff.x - vv.x;
        float z1z2 = ff.z - vv.z;

        decimal x1x2de = (decimal)MinusDeletef(x1x2);
        decimal z1z2de = (decimal)MinusDeletef(z1z2);

        double x1x2do = decimal.ToDouble(x1x2de);
        double z1z2do = decimal.ToDouble(z1z2de);

        kyori = Math.Sqrt((x1x2do) * (x1x2do) + (z1z2do) * (z1z2do));
        return (float)kyori;
    }

    //uinの座標値をsentakusリストから検索して、それに対応する端点を返す
    private Tanten SelectionSearch(Vector3 uin, List<Sentaku> sentakus)
    {
        Tanten selection = new Tanten(0, 0);
        int i = 0;
        while (i < sentakus.Count)
        {
            if (uin.x == sentakus[i].x1 && uin.z == sentakus[i].z1)
            {
                selection = new Tanten(sentakus[i].x2, sentakus[i].z2);
            }
            i++;
        }
        return selection;
    }

    private void UiSummoning(List<Vector3> uiv)
    {
        int i = 0;
        while (i < uiv.Count)
        {
            Instantiate(uis, new Vector3(uiv[i].x, 1.5f, uiv[i].z), Quaternion.identity);

            i++;
        }
    }
}