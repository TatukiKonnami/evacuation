﻿using System;

internal class Road
{
    public int x1;
    public int y1;
    public int x2;
    public int y2;

    //Roadの中には端点情報を入れる
    public Road(int x1, int y1, int x2, int y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }
}