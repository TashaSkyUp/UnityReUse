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
        public LabeledInt(){}
        public LabeledInt(string nlabel, int value)
        {
            label = nlabel;
            _int=value;
        }
    }


    
    manyLI all = new manyLI();
    string uidLabel;
    public IntFromXML(string file, string nUIDLabel, int value)
    {

        xmlSRC = file;
        uidLabel = nUIDLabel;
        if (File.Exists(file))
        {
            LoadAll(file );

            var t = all.Get(uidLabel);

            if (t == null)//uid is not defined
            {
                
                all.values.Add(new LabeledInt(uidLabel,value));
                SaveAll(file);
            }
            else//uid is defined
            {

            }

            
        }
        else
        {
            all.values.Add(new LabeledInt(uidLabel, value));
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

    private void LoadAll(string file)
    {
        xmlSRC = file;
        var xmlSerializer = new XmlSerializer(all.GetType());
        FileStream stream = File.Open(xmlSRC, FileMode.Open);
        all = (manyLI)xmlSerializer.Deserialize(stream);
        
        stream.Close();
    }

    public IntFromXML(string file, string nuidLabel)
    {
        uidLabel = nuidLabel;
        LoadAll(file);

        var lint = all.Get(uidLabel);
        if (lint == null) {            
            lint = new LabeledInt(uidLabel,0);
            lint.label=uidLabel;
            all.values.Add(lint);
        }
        SaveAll(file);

    }

    public int Value { get => all.Get(uidLabel)._int;
        set =>
            Save(value);
    }
    public void Save(int v)
    {        
        all.Get(uidLabel)._int= v;
        SaveAll(xmlSRC);
    }
    
}
