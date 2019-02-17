﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasterVO
{
    public string Tag="0";
    public UnityEngine.Rect sRect;
    public float[] VO;
 
    protected Texture2D myTex2d;
    private bool initialized=false;
    private float[,] a;

    public void tmp(float[] v)
    {
        VO = v;
    }

    public RasterVO()
    
    {

    }
    protected void UpdateTex2D(Color BC)
    {
        if (myTex2d == null) { myTex2d = new Texture2D((int)sRect.width, (int)sRect.height); }
        var use = this;
        int idx = 0;
        Color UC= new Color();
        if (BC == null) { 
            UnityEngine.Color c = new UnityEngine.Color
            {
                a = 1
            };
        }

       
        int mx = (int)use.sRect.width;
        for (int x = 0; x < mx; x++)
        {
            for (int y = 0; y < use.sRect.height; y++)
            {
                float n = use.VO[use.VO.LongLength - 1 - idx];
                UC.r = BC.r * n;
                UC.g = BC.g * n;
                UC.b = BC.b * n;
                myTex2d.SetPixel((mx - 1) - x, y, UC);
                idx++;
                if (idx >= use.VO.Length - 1) { break; }
            }
            if (idx >= use.VO.Length - 1) { break; }
        }

        //tx.ReadPixels(r,0,0);
        myTex2d.Apply();
        initialized = true;
    }

    public float[,] get2dVO()
    {
        int width = (int)sRect.width;
        float[,] o = new float[width, (int)sRect.height];
        for (int idx = 0; idx < VO.Length; idx++)
        {
            int y = idx / width;
            int x = (idx - (y * width));
            o[x, y] = VO[idx];
            //Debug.Log(x + " : " + y);

        }
        return o;
    }

    private int getIdx(int x,int y)
    {
        //int x = (idx - (y * width));
        return (x + (y * (int)sRect.width));
    }

    public RasterVO(UnityEngine.Rect nsRect, float[] nVO)
    {
        sRect = nsRect;
        VO = nVO;
        myTex2d = new Texture2D((int)sRect.width, (int)sRect.height);
    }
    public RasterVO(UnityEngine.Rect nsRect, float[,] nVO)
    {
        VO = new float[nVO.GetLength(0) * nVO.GetLength(1)];
        sRect = nsRect;
        int idx = 0;
        for (int y = 0; y < nVO.GetLength(1); y++)
        {
            for (int x = 0; x < nVO.GetLength(0); x++)
            {
                VO[idx] = nVO[x, y];
                idx++;
            }
        }
        myTex2d = new Texture2D((int)sRect.width, (int)sRect.height);
    }

    public float PercentAtXOrGreater(float X)
    {
        float o = 0;
        foreach (float n in VO)
        {
            if (n > X) { o++; }
        }
        return (o / VO.Length);
    }
    public Texture2D toTex2D()
    {
        if (initialized == true) { return(myTex2d); } else { UpdateTex2D(new Color(1f, 1f, 1f)); }return (myTex2d);

    }
    public Texture2D toTex2D(Color c)
    {
        if (initialized == true) { return (myTex2d); } else { UpdateTex2D(c); }
        return (myTex2d);

    }


    public Sprite toSprite()
    {
        var it = toTex2D();

        var spr = Sprite.Create(it, new UnityEngine.Rect(0, 0, it.width, it.height), new Vector2(.5f, .5f));
        it = null;
        System.GC.Collect();
        return (spr);

    }

    public void PullValues(float v,float fallOff,float power) {
        for (int i = 0; i < VO.LongLength; i++)
        {
            float n = VO[i];

            float dp = Math.Abs(v - n)/fallOff;
            if (dp > 1) { dp = 1; }
            float t2 = Mathf.Pow(dp, 2/3f);
            float t1 = (Mathf.Sin(t2 * 3.14f));

            VO[i] = (n*(1-t1)) + (v*(t1));
            VO[i] = (VO[i] * power) + (n * (1 - power));

        }
        
    }
    public void patch(int xoff,int yoff,float[,] patch)
    {
        for (int y = 0; y < patch.GetLength(1); y++)
        {
            for (int x = 0; x < patch.GetLength(0); x++)
            {
                VO[getIdx(x + xoff, y + yoff)] = patch[x, y];
            }
        }
    }
    public void PullValues(float v, float fallOff, float power,Rect area)
    {
        
        for (int y = 0; y < area.height; y++)
        {
            for (int x = 0; x < area.width; x++)
            {
                long i = getIdx(x+(int)area.x,y+(int)area.y);
                float n = VO[i];

                float dp = Math.Abs(v - n) / fallOff;
                if (dp > 1) { dp = 1; }
                float t2 = Mathf.Pow(dp, 2 / 3f);
                float t1 = (Mathf.Sin(t2 * 3.14f));
                
                VO[i] = (n * (1 - t1)) + (v * (t1));
                VO[i] = (VO[i] * power) + (n * (1 - power));

                }
            }
    }
}
