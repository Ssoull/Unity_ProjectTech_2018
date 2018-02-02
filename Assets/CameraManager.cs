using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera leftCamera;
	public Camera rightCamera;

    [Range(0,10)]
	public float distanceEntreLesCamera = 5f;
	public int focaleCamera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float diff = rightCamera.transform.position.x - leftCamera.transform.position.x;
		if (Mathf.Abs(diff) != distanceEntreLesCamera) {
			float value = distanceEntreLesCamera - diff;
			Vector3 v = rightCamera.transform.position;
			v.x += value / 2;
			rightCamera.transform.position = v; 
			v = leftCamera.transform.position;
			v.x -= value / 2;
			leftCamera.transform.position = v; 
		}
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 40), "Volume des effets sonore : " + distanceEntreLesCamera);
        distanceEntreLesCamera = GUI.HorizontalSlider(new Rect(10, 50, 200, 10), distanceEntreLesCamera, 0, 10);
    }
}
