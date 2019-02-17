using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fractal    : MonoBehaviour
{
    private static int _size;
    private static int _subSize;

    internal static float avgDiamondVals(int i, int j, int stride, int size, int subSize, float[] fa)
{
        var v1 = fa[getIdx(i - stride, j)];
        var v2 = fa[getIdx(i + stride, j)];
        var v3 = fa[getIdx(i , j-stride)];
        var v4 = fa[getIdx(i , j+stride)];

        return ((v1 + v2 + v3 + v4) * .25f);
    
}


internal static float avgEndpoints(int i, int stride, float[] fa)
{
    return ((float)(fa[i - stride] + fa[i + stride]) * .5f);
}


internal static float avgSquareVals(int i, int j, int stride, int size, float[] fa)
{

    return ((float)(
            fa[((i - stride) * size) + j - stride] + 
            fa[((i - stride) * size) + j + stride] + 
            fa[((i + stride) * size) + j - stride] + 
            fa[((i + stride) * size) + j + stride]) * .25f);
}

    public static float[] createNormalized2DFract1DArray(int nSize,float scale)
    {
        float[] tmpAr = fill2DFractArray(nSize, 0, 1f, scale);
        return normalizeArray1d(tmpAr);

    }
    public static float[,] createNormalized2DFract2DArray(int nSize, float scale){
    float[] tmpAr = fill2DFractArray(nSize, 0, 1, scale);
    return normalizeArray(tmpAr);
}

    public static float[,] normalizeArray(float[] nFarry)
    {
        float max= float.MinValue;float min= float.MaxValue;
        int d = (int) Mathf.Sqrt(nFarry.Length);
        float[,] o = new float[d, d];

        foreach (float n in nFarry)
        {
            if (n < min) { min = n; }if (n > max) { max = n; }
        }
        long i = 0;
        for (int y = 0; y < d; y++){for (int x = 0; x < d; x++){
                o[x, y] = (nFarry[i] - min) / (max-min);
                i++;
        }}
        return o;
    }
    public static float[] normalizeArray1d(float[] nFarry)
    {
        float max = float.MinValue; float min = float.MaxValue;

        float[] o = new float[nFarry.LongLength];

        foreach (float n in nFarry)
        {
            if (n < min) { min = n; }
            if (n > max) { max = n; }
        }

        for (int i = 0; i < nFarry.LongLength; i++)
        {   
                o[i] = (nFarry[i] - min) / (max-min);            
        }
        return o;
    }
    private static int getIdx(int i,int j)
    {
        //Debug.Log(i + ":" + j);
        if (i < 0) { i = _subSize + i; }
        if (i > _subSize) { i = i-_subSize; }
        if (j < 0) { j = _subSize + j; }
        if (j > _subSize) { j = j - _subSize; }

        return ((((_size)* i)) + j);
        // 0,1,2,3,4,5,6,7,
        //
        //
    }
    private static float[] fill2DFractArray(int size, int seedValue, float heightScale, float h)
{
    
    int i, j;
    int stride;
    int oddline;
    int subSize;
    float ratio, scale;
    float[] fa;
    if (seedValue == 0)
    {
            seedValue = (int)(System.DateTime.Now.Millisecond);
            UnityEngine.Random.InitState(seedValue);
        }
    if (!powerOf2(size) || (size == 1))
    {

        return null;
    }
    fa = new float[(size + 1) * (size + 1)];




        //size--;
        size++;
        _size = size;
        subSize = size-1;
        _subSize= subSize;
        ratio = (float)Math.Pow(2.0, -h);
        scale = heightScale * ratio;



        //stride = Mathf.RoundToInt( subSize / 2f);
        stride = subSize / 2;
        //int 
        //fa[(0 * size) + 0] = fa[(subSize * size) + 0] = fractRand(1f);
        //fa[(subSize * size) + subSize] = fractRand(1f);
        //fa[(0 * size) + subSize] = fractRand(1f);
        //fa[(subSize * size) + 0] = fractRand(1f);




        while (stride >= 1)
    {

        for (i = stride; i < subSize; i += stride)
        {
            for (j = stride; j < subSize; j += stride)
            {
                fa[(i * size) + j] = scale * fractRand(.5f) + avgSquareVals(i, j, stride, size, fa);
                j += stride;
            }
            i += stride;
        }


        oddline = 0;
        for (i = 0; i < subSize; i += stride)
        {
            if (oddline == 0)
            {
                oddline = 1;
            }
            else
            {
                oddline = 0;
            }

            for (j = 0; j < subSize; j += stride)
            {
                if ((oddline != 0) && (j == 0))
                {
                    j += stride;
                }


                fa[(i * size) + j] = scale * fractRand(.5f) + avgDiamondVals(i, j, stride, size, subSize, fa);


                if (i == 0)
                {
                    fa[(subSize * size) + j] = fa[(i * size) + j];
                }
                if (j == 0)
                {
                    fa[(i * size) + subSize] = fa[(i * size) + j];
                }

                j += stride;
            }
        }


        scale *= ratio;
        stride >>= 1;
    }
    return fa;

}
    internal static bool powerOf2(int size)
    {
        if ((size & (size - 1)) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static float fractRand(float v)
    {
        
        return UnityEngine.Random.Range(-v, v);
        
    }



}
