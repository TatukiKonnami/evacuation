using UnityEngine;
using System.Collections;

public class MapSample3_1 : MonoBehaviour
{
    public TextAsset _layout;
    public GameObject[] _objs;
    public GameObject parentObject;

    // Use this for initialization
    void Start()
    {
        this.readMap();
    }

    void readMap()
    {
        
        string[] layoutInfo = _layout.text.Split('\n');
        string[] eachInfo;

        for (int i = 0; i < layoutInfo.Length; i++)
        {
            eachInfo = layoutInfo[i].Split(","[0]);
            GameObject obj = _objs[0];

            int j = 0;
            float x_point1, y_point1, x_point2, y_point2, x_distance, y_distance, x_interpolation, y_interpolation, x_distanceAv, y_distanceAv;

            //x1,y1,x2,y2の座標
            x_point1 = float.Parse(eachInfo[0]);
            y_point1 = float.Parse(eachInfo[1]);
            x_point2 = float.Parse(eachInfo[2]);
            y_point2 = float.Parse(eachInfo[3]);

            //x,yのそれぞれの距離
            x_distance = x_point2 - x_point1;
            y_distance = y_point2 - y_point1;

            //x,yのそれぞれの距離の絶対値
            x_distanceAv = System.Math.Abs(x_distance);
            y_distanceAv = System.Math.Abs(y_distance);

            //x,yのそれぞれの補間
            x_interpolation = x_distance / y_distance;
            y_interpolation = y_distance / x_distance;

            //x,yのそれぞれの補間の絶対値
            y_interpolation = System.Math.Abs(y_interpolation);
            x_interpolation = System.Math.Abs(x_interpolation);

            

            if (x_distance >= 0 && y_distance >= 0 && x_distanceAv < y_distanceAv)//0-45
            {
                j = 10;
            }
            else if (x_distance >= 0 && y_distance >= 0 && x_distanceAv > y_distanceAv)//45-90
            {
                j = 11;
            }
            else if (x_distance >= 0 && y_distance <= 0 && x_distanceAv > y_distanceAv)//90-135
            {
                j = 20;
            }
            else if (x_distance >= 0 && y_distance <= 0 && x_distanceAv < y_distanceAv)//135-180
            {
                j = 21;
            }
            else if (x_distance <= 0 && y_distance <= 0 && x_distanceAv < y_distanceAv)//180-225
            {
                j = 30;
            }
            else if (x_distance <= 0 && y_distance <= 0 && x_distanceAv > y_distanceAv)//225-270
            {
                j = 31;
            }
            else if (x_distance <= 0 && y_distance >= 0 && x_distanceAv > y_distanceAv)//270-315
            {
                j = 40;
            }
            else if (x_distance <= 0 && y_distance >= 0 && x_distanceAv < y_distanceAv)//315-360
            {
                j = 41;
            }



            switch (j)
            {
                case 10://0-45
                    for (float y_position = y_point1; y_position <= y_point2; y_position++)
                    {
                        Vector2 pos = new Vector2(x_point1 += x_interpolation, y_position);
                        this.createObj(obj, pos);

                    }
                    break;
                case 11://45-90
                    for (float x_position = x_point1; x_position <= x_point2; x_position++)
                    {
                        Vector2 pos = new Vector2(x_position, y_point1 += y_interpolation);
                        this.createObj(obj, pos);

                    }
                    break;  
                case 20://90-135
                    for (float x_position = x_point1; x_position <= x_point2; x_position++)
                    {
                        Vector2 pos = new Vector2(x_position, y_point1 -= y_interpolation);
                        this.createObj(obj, pos);

                    }
                    break;
                case 21://135-180
                    for (float y_position = y_point1; y_position >= y_point2; y_position--)
                    {
                        Vector2 pos = new Vector2(x_point1 += x_interpolation, y_position);
                        this.createObj(obj, pos);

                    }
                    break;
                case 30://180-225
                    for (float y_position = y_point1; y_position >= y_point2; y_position--)
                    {
                        Vector2 pos = new Vector2(x_point1 -= x_interpolation, y_position);
                        this.createObj(obj, pos);

                    }
                    break;
                case 31://225-270
                    for (float x_position = x_point1; x_position >= x_point2; x_position--)
                    {
                        Vector2 pos = new Vector2(x_position, y_point1 -= y_interpolation);
                        this.createObj(obj, pos);

                    }
                    break;
                case 40://270-315
                    for (float x_position = x_point1; x_position >= x_point2; x_position--)
                    {
                        Vector2 pos = new Vector2(x_position, y_point1 += y_interpolation);
                        this.createObj(obj, pos);

                    }
                    break;
                case 41://315-360
                    for (float y_position = y_point1; y_position <= y_point2; y_position++)
                    {
                        Vector2 pos = new Vector2(x_point1 -= x_interpolation, y_position);
                        this.createObj(obj, pos);

                    }
                    break;
            }
        }
    }



    void createObj(GameObject obj, Vector2 pos)
    {
        GameObject childObject = Instantiate(obj, new Vector3(-pos.x, 0, pos.y), obj.transform.rotation) as GameObject;
        childObject.transform.parent = parentObject.transform;

    }



}
