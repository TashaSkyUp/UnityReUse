using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class IntFromXML
{
    private string xmlSRC;
    public class manyLI
    {
        public List<LabeledInt> values= new List<LabeledInt>();

        internal LabeledInt Get(string uidLabel)
        {
            foreach (var n in values)
            {
                if (n.label == uidLabel)
                {
                    return n;
                }
            }
            return null;
        }
    }
    public class LabeledInt {
        public string label;
        public int _int;        
    }


    LabeledInt lint = new LabeledInt();
    manyLI all = new manyLI();

    public IntFromXML(string file, string uidLabel, int value)
    {

        xmlSRC = file;

        if (File.Exists(file))
        {
            LoadAll(file, uidLabel );
            all.Get(uidLabel)._int = value;
            SaveAll(file);
        }
        else
        {
            lint.label = uidLabel;
            lint._int = value;
            all.values.Add(lint);
            SaveAll(file);
        }
    }

    private void SaveAll(string file)
    {
        var xmlSerializer = new XmlSerializer(all.GetType());
        FileStream stream = File.Open(file, FileMode.Create);
        xmlSerializer.Serialize(stream, all);
        stream.Close();       
    }

    private void LoadAll(string file, string uidLabel)
    {
        xmlSRC = file;
        var xmlSerializer = new XmlSerializer(all.GetType());
        FileStream stream = File.Open(xmlSRC, FileMode.Open);
        all = (manyLI)xmlSerializer.Deserialize(stream);
        stream.Close();
    }

    public IntFromXML(string file, string uidLabel)
    {
        LoadAll(file, uidLabel);

        lint = all.Get(uidLabel);
        if (lint == null) {
            lint = new LabeledInt();
            lint.label=uidLabel;
            all.values.Add(lint);
        }
        SaveAll(file);

    }

    public int Value { get => lint._int; set =>
            Save(value);
    }
    public void Save(int v)
    {
        
        lint._int = v;
        SaveAll(xmlSRC);
    }
    
}
