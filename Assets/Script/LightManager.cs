using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {


    [Range(0, 3)]
    public float intensityLight = 1f;
    private float newIntensity;
    public Canvas canvas;
    public Light lighting;

    // Use this for initialization
    void Start () {
        newIntensity = intensityLight;
    }
	
	// Update is called once per frame
	void Update () {
        if (newIntensity != intensityLight)
        {
            lighting.intensity = intensityLight = newIntensity;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(canvas.pixelRect.width - 300, 10, 300, 40), "Intensité lumière : " + intensityLight);
        newIntensity = GUI.HorizontalSlider(new Rect(canvas.pixelRect.width - 300, 25, 200, 10), newIntensity, 0, 3);
    }

    public void reset()
    {
        newIntensity = 1f;
    }
}
