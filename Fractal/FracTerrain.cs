using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FracTerrain : MonoBehaviour
{
    public int size;
    public FracCache _fracCache;
    public int levels;
    public TerrainData tData;
    
    public RasterVO[] rvo = new RasterVO[10];

    private void Start()
    {
        newTerrian(1f);
    }
    public void newTerrian(float steepness)
    {
        var terrain = gameObject.GetComponent<Terrain>();
        var tCollider = gameObject.GetComponent<TerrainCollider>();
        _fracCache = null;
        tData = Instantiate<TerrainData>(tData);
        tData.heightmapResolution = size + 1;
        tData.size = new Vector3(size, size / 10, size);
        tCollider.terrainData = tData;
        terrain.terrainData = tData;
        //terrain.terrainData.size = new Vector3(64, 6.4f,64) * 10;

        float[,] a; float[,] b;
        if (_fracCache != null)
        {
            a = Fractal.normalizeArray(_fracCache.GetRandomFractal());
            b = Fractal.normalizeArray(_fracCache.GetRandomFractal());
        }
        else
        {
            a = Fractal.createNormalized2DFract2DArray(size, .95f);
            b = Fractal.createNormalized2DFract2DArray(size, .25f);
        }

        rvo[0] = new RasterVO(new Rect(0, 0, size + 1, size + 1), a);
        rvo[1] = new RasterVO(new Rect(0, 0, (128) + 1, (128) + 1), b);

        tData.SetHeights(0, 0, rvo[0].get2dVO());

        if (levels == 0)
        {
            rvo[0].PullValues(.5f, .75f, 1);
            rvo[0].PullValues(.5f, .75f, 1);
            rvo[0].PullValues(.5f, .75f, 1);
        }
        else
        {
            for (float i = 0; i <= 1; i = i + 1 / (float)levels)
            {
                rvo[0].PullValues(i, (1 / (float)levels) * .75f, steepness);
            }
            rvo[0].PullValues(.5f, 1.25f, 1-steepness);
        }
        rvo[1].PullValues(.5f, .8f, .95f);
        tData.terrainLayers[0].diffuseTexture = rvo[1].toTex2D(new Color(.33f, 1f, .33f));
        tData.terrainLayers[0].tileSize = new Vector2(128, 128);
        //rvo[0].patch(0, 0, new float[32, 32]);
        var it = rvo[0].get2dVO();
        //y=z
        //x=x
        //64 offset

        it[0, 0] = 2f; it[2, 0] = 2f; it[4, 0] = 0f;
        it[0, 2] = 2f; it[2, 2] = 2f; it[4, 2] = 2f;
        it[0, 4] = 2f; it[2, 4] = 2f; it[4, 4] = 0f;

        tData.SetHeights(0, 0, it);
    }
    public void UpdateTer()
    {
        tData.SetHeights(0, 0, rvo[0].get2dVO());
    }

}
