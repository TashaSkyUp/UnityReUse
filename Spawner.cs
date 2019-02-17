using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject go;
    public int Number;
    public float Period;
    private int done;
    private float myTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (myTime > Period)
        {
            myTime = 0;
            if (go != null)
            {
                //if (DarknessAgent.allDarkness.Count < Number)
                {
                    var a = GameObject.Instantiate(go,transform.position,transform.rotation, transform.parent);
                    a.SetActive( true);
                    done++;
                }
            }
            
        }
    }
}
