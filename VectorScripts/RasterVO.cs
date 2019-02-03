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

    public void tmp(float[] v)
    {
        VO = v;
    }

    public RasterVO()
    
    {

    }
    protected void UpdateTex2D()
    {
        if (myTex2d==null) { myTex2d = new Texture2D((int)sRect.width, (int)sRect.height); }
        var use = this;
        int idx = 0;
        UnityEngine.Color c = new UnityEngine.Color
        {
            a = 1
        };

       
        int mx = (int)use.sRect.width;
        for (int x = 0; x < mx; x++)
        {
            for (int y = 0; y < use.sRect.height; y++)
            {
                float n = use.VO[use.VO.LongLength - 1 - idx];
                c.r = n; c.g = n; c.b = n;
                myTex2d.SetPixel((mx - 1) - x, y, c);
                idx++;
                if (idx >= use.VO.Length - 1) { break; }
            }
            if (idx >= use.VO.Length - 1) { break; }
        }

        //tx.ReadPixels(r,0,0);
        myTex2d.Apply();
        initialized = true;
    }
    public RasterVO(UnityEngine.Rect nsRect, float[] nVO)
    {
        sRect = nsRect;
        VO = nVO;
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
        if (initialized == true) { return(myTex2d); } else { UpdateTex2D(); }

       

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
}
