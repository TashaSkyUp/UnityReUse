using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAtTime : MonoBehaviour
{
    public bool notdone = true;
    public float aliveTime = 0;
    public AnimationCurve curve;
    private Vector3 startScale;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        transform.localScale=startScale*curve.Evaluate(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (notdone) {
            aliveTime += Time.deltaTime;
            transform.localScale = startScale * curve.Evaluate(aliveTime);
            if (aliveTime> curve.keys[curve.keys.Length - 1].time)
            {
                notdone = false;
            }
        }
    }
}
