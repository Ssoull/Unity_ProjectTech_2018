using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour {

    public Light lighting;
    public GameObject cameraManager;

    public void onClick()
    {
        LightManager lightManagerScript = lighting.GetComponent<LightManager>();
        lightManagerScript.reset();

        CameraManager cameraManagerScript = cameraManager.GetComponent<CameraManager>();
        cameraManagerScript.reset();
    }
}
