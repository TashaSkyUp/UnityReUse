using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Drawing;

public class RasVOSet 
{
    public class MyEqualityComparer : IEqualityComparer<float[]>
    {
        public bool Equals(float[] x, float[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(float[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + (int)obj[i];
                }
            }
            return result;
        }
    }
    public  List<RasterVO> eVOS = new List<RasterVO>();
    public Dictionary<float[], string> trans = new Dictionary<float[], string>(new MyEqualityComparer());
    private Bitmap lBmp;

    public void SaveToFile(string fn)
    {
        List<string> o = new List<string>();
        foreach (var n in eVOS)
        {
            o.Add(JsonUtility.ToJson(n));
        }
        File.AppendAllLines(fn, o);
    }
    public RasVOSet(){}

    public RasVOSet(Bitmap fn)
    {
        lBmp = fn;
    }
    public RasVOSet(string fn)
    {
        if (File.Exists(fn))
        {
            var b = File.ReadLines(fn);
            List<string> c = new List<string>(b);

            foreach (var n in c)
            {
                //var d = JsonUtility.FromJson<EuclidianVO>(c.ToArray()[0]);
                var a = JsonUtility.FromJson<RasterVO>(n);
                
                eVOS.Add(a);
            }
            foreach (RasterVO nn in eVOS)
            {
                trans.Add(nn.VO, nn.Tag.ToString());
            }
        }
    }

    public void add(RasterVO n)
    {
        if ((n.VO == null) &&(lBmp==null)) { throw new Exception { }; }
        if (n.VO == null)
        {
            n.VO = BitmapUtils.FloatRegion(n.sRect, n.cc, n.downScale, lBmp);
        }
        foreach (RasterVO evo in eVOS)
        {
            if (compareArray(n.VO, evo.VO))
            {
                return;
            }
        }
        eVOS.Add(n);
    }


    public bool compareArray(float[] x, float[] y)
    {
        if (x.Length != y.Length) { return false; }

        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }

        return true;

    }

    internal void observe(Bitmap screenShot)
    {
        foreach (RasterVO rvo in eVOS)
        {
            rvo.observe(screenShot);
        }
    }
}
