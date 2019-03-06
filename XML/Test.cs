using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private IntFromXML ifx;
    // Start is called before the first frame update
    void Start()
    {
        var thing = new IntFromXML[10];
        
        ifx = new IntFromXML("IntFromXML_test.xml", "test_int",1);
        ifx.Value = 2;

        var ifx2 = new IntFromXML("IntFromXML_test.xml", "test_int2");
        ifx2.Value = 4;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
