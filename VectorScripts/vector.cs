using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vector : MonoBehaviour
{
    public float Value;
    public UnityEngine.UI.Text txtValue;
    public UnityEngine.UI.Text txtLabel;
    

    public string Label { get => txtLabel.text; set => txtLabel.text= value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtValue.text = string.Format("{0:P0}", Value); 
    }
}
