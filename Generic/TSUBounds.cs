using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TSUBounds : MonoBehaviour
    {
        public Vector3 repsawnAtLocal;
        public float maxCubicMeasurment;
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (OutOfBounds(transform.localPosition))
            {
                transform.localPosition = repsawnAtLocal;
            }
        }
        private bool OutOfBounds(Vector3 inv3)
        {
            if (inv3.x > maxCubicMeasurment) return true;
            if (inv3.x < -maxCubicMeasurment) return true;
            if (inv3.y > maxCubicMeasurment) return true;
            if (inv3.y < -maxCubicMeasurment) return true;
            if (inv3.z > maxCubicMeasurment) return true;
            if (inv3.z < -maxCubicMeasurment) return true;

            return false;

        }
 
}
