using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class BitmapUtils : MonoBehaviour
{
    public static float[] FloatRegion(Rect r, int color, int downScale, Bitmap nbmp)
    {
        //var graphics = System.Drawing.Graphics.FromImage(nbmp);


        r.width = (int)Mathf.Ceil(r.width / downScale) * downScale;
        r.height = (int)Mathf.Ceil(r.height / downScale) * downScale;

        int xs = (int)r.xMin; int ys = (int)r.yMin;
        int xe = (int)r.xMax; int ye = (int)r.yMax;

        float[] o = new float[(int)(((xe - xs) / downScale) * ((ye - ys) / downScale))];

        long idx = 0;

        for (int y = ys; y < ye; y = y + downScale){            
                for (int x = xs; x < xe; x = x + downScale){

                if (color == 0)
                {
                    o[idx] = HowRed(nbmp.GetPixel(x, y));

                }
                else if (color == 2)
                {
                    o[idx] = HowBlue(nbmp.GetPixel(x, y));

                }
                else if (color == 3)
                {
                    o[idx] = GreyScale(nbmp.GetPixel(x, y));
                }
                else if (color == 4)
                {
                    o[idx] = White(nbmp.GetPixel(x, y));
                }

                idx++;

            }
        }
        return o;

    }
    


    public static float HowBlue(System.Drawing.Color c)
    {
        int lo = c.B - (c.R + c.G);
        if (lo > 0)
        {
            return (lo / 256f);
        }
        else
        {
            return (lo / 512f);
        }
    }
    public static float HowRed(System.Drawing.Color c)
    {
        int lo = c.R - (c.B + c.G);
        if (lo > 0)
        {
            return (lo / 256f);
        }
        else
        {
            return (lo / 512f);
        }
    }
    public static float GreyScale(System.Drawing.Color c)
    {
        return ((float)(c.R + c.G + c.B) / 768f);
    }
    public static float White(System.Drawing.Color c)
    {
        if (((c.R + c.G + c.B) / 768f) > .805f)
        {
            return 1f;
        }
        else
        {
            return 0f;
        }


    }
}
