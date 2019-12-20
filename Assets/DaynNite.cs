using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaynNite : MonoBehaviour
{
    private new Light light;
    public float current;
    public float maxIntensity;


   
    void Start()
    {
        light = GetComponent<Light>();        
    }

    void Update()
    {
        current = transform.localEulerAngles.x;

        light.transform.Rotate(new Vector3(1, 0, 0), Time.deltaTime);

        if(current > 180)
        {
            light.intensity = 0;
        }
        else
        {
            light.intensity = maxIntensity;
        }

        if(current >= 360)
        {
            current = 0;
        }
    }
   
   

}
