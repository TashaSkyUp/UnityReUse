using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class IntFromXML
{
    public class LabeledInt{
        public string label;
        public int _int;
     
    }
    
    
    LabeledInt lint = new LabeledInt();
    public IntFromXML(string file, string uidLabel,int value)
    {
        lint.label = uidLabel;
        lint._int = value;
    
        
        var xmlSerializer = new XmlSerializer(lint.GetType());
        var stream = File.Open(file, FileMode.OpenOrCreate);
        
        xmlSerializer.Serialize(stream, lint);
        stream.Close();

    }

        public IntFromXML(string file,string uidLabel)
    {
        
        var xmlSerializer = new XmlSerializer(lint.GetType());
        var stream = File.Open(file, FileMode.Open);

        lint = (LabeledInt) xmlSerializer.Deserialize(stream);

        stream.Close();

            //_int = deserializedItems.items[i];
            //itemDictionary.Add(itemCur.name, itemCur);
            //Debug.Log("Item " + i + " name: " + itemCur.name + " Value: " + itemCur.value);
            //Debug.Log(itemDictionary["Item 1"].value);

    }

    public int Value { get => lint._int;}
}
