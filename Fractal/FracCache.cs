using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class FracCache : MonoBehaviour
{
    public enum fracSizes
    {
        small,
        medium,
        large,
        XL,
        XXL,
           
    };
    public bool Generated=false;
    public bool GenOrLoadIfExsists = false;
    public float SecondsToSpendCreating = 1f;
    public fracSizes fracSize = fracSizes.small;
    private int FS=64;
    private string folderPath;
    private List<float[]> _fracs;

    void Start()
    {
        if (fracSize == fracSizes.small)  FS = 128; 
        if (fracSize == fracSizes.medium)  FS = 256; 
        if (fracSize == fracSizes.large) { FS = 1024; }
        if (fracSize == fracSizes.XL) { FS = 2048; }
        if (fracSize == fracSizes.XXL) { FS = 4096*2; }
        //var a = File.ReadAllBytes("frac.dat");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        folderPath = Path.Combine(Application.persistentDataPath, "FracData");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (GenOrLoadIfExsists)
            LoadIfExists();
            
        if (!Generated)
            Generate();

    }

    private void LoadIfExists()
    {
        if (File.Exists( GetDataFilePath() )) {
            _fracs = LoadFrac(GetDataFilePath());
            Generated = true;
        }
        else
        {
            Generate();
            //SaveFrac(_fracs,GetDataFilePath());
        }
    }
    private string GetDataFilePath(){
        return folderPath + fracSize.ToString() + ".dat";
    }
    private void Generate()
    {
        var starttime = Time.realtimeSinceStartup;
        int idx = 0;
        _fracs = new List<float[]>();
        
        while (Time.realtimeSinceStartup < starttime + SecondsToSpendCreating)
        {
            //todo:Multithread this.
            _fracs.Add(Fractal.createNormalized2DFract1DArray(FS, 1));
            idx++;
            Debug.Log("Generated fractal: " + idx.ToString());
        }
        SaveFrac(_fracs, GetDataFilePath());
        Generated = true;
        
    }
    public float[] GetRandomFractal()
    {
        var n = Random.Range(0, _fracs.Count);
        return _fracs[n];
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    static void SaveFrac(List<float[]> data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    static List<float[]> LoadFrac(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (List<float[]>)binaryFormatter.Deserialize(fileStream);
        }
        

    }
}
