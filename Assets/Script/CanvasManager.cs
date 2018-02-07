using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    public Light lighting;
    public GameObject cameraManager;
    public Camera rightCamera;
    public Camera leftCamera;
    public Text expansionText;
    public Slider sliderExpansion;

    private static int number_folder = 0;
    private string pathCreation = "";

    private void Start()
    {
        List<string> dirs = new List<string>(System.IO.Directory.GetDirectories(Application.dataPath + "/Screenshots/"));
        number_folder = dirs.Count;
        expansionText.text = "Agrandissement (x" + sliderExpansion.value + ") :";
    }

    public void onExpansionValueChanged()
    {
        expansionText.text = "Agrandissement (x" + sliderExpansion.value + ") :";
    }

    public void onClick()
    {
        LightManager lightManagerScript = lighting.GetComponent<LightManager>();
        lightManagerScript.reset();

        CameraManager cameraManagerScript = cameraManager.GetComponent<CameraManager>();
        cameraManagerScript.reset();
    }
    
    private bool takeHiResShot = false;

    public static string screenshotName(int width, int height, string pathCreation, string text)
    {
        return string.Format("{0}/{1}_camera.png", pathCreation, text);
    }


    public void takeScreenshot()
    {
        takeHiResShot = true;
    }


    // Inspiration : https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
    void LateUpdate()
    {
        if (takeHiResShot)
        {
            pathCreation = Application.dataPath + "/Screenshots/screenshot_" + number_folder++;
            System.IO.Directory.CreateDirectory(pathCreation);
            screenshot(leftCamera, "left");
            screenshot(rightCamera, "right");
            takeHiResShot = false;
        }
    }
    

    private void screenshot(Camera camera, string text)
    {
        RenderTexture oldRenderTexture = camera.targetTexture;

        int width, height;
        width = oldRenderTexture.width * (int)sliderExpansion.value;
        height = oldRenderTexture.height * (int)sliderExpansion.value;

        RenderTexture tmpRenderTexture = new RenderTexture(width, height, 24);
        camera.targetTexture = tmpRenderTexture;
        camera.Render();
        RenderTexture.active = tmpRenderTexture;

        Texture2D tex = new Texture2D(width, height);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        
        byte[] bytes = tex.EncodeToPNG();

        RenderTexture.active = null;
        Destroy(tmpRenderTexture);

        string filename = screenshotName(width, height, pathCreation, text);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        camera.targetTexture = oldRenderTexture;
    }
}
