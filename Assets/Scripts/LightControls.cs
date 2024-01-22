using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControls : MonoBehaviour
{
    float minIntensity = 0.1f;
    float maxIntesity = 1.0f;
    float currIntensity = 0;
    void Start()
    {
        currIntensity = gameObject.GetComponent<Light2D>().intensity;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8)) AddIntensity();
        else if( Input.GetKeyDown(KeyCode.Keypad2)) RemoveIntensity();
    }
    private void AddIntensity()
    {
        if (currIntensity < maxIntesity) 
            currIntensity += 0.1f;
        gameObject.GetComponent<Light2D>().intensity = currIntensity;
    }
    private void RemoveIntensity()
    {
        if (currIntensity > minIntensity) currIntensity -= 0.1f;
        else currIntensity = 0.1f;
        gameObject.GetComponent<Light2D>().intensity = currIntensity;
    }
}
