using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private enum Direction { UP, DOWN, LEFT, RIGHT };

	public Camera leftCamera;
    public Camera rightCamera;

    [Range(0,10)]
	public float distanceEntreLesCamera = 5f;
    public float speedRotation = 5;
	public int focaleCamera;

    public Text leftCamText;
    public Text rightCamText;

    public Toggle toggle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        // Distance
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

        // Orientation
        checkRotation(Input.GetKey(KeyCode.DownArrow), Input.GetKey(KeyCode.S), speedRotation * Time.deltaTime, Direction.DOWN);
        checkRotation(Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.Q), -speedRotation * Time.deltaTime, Direction.LEFT);
        checkRotation(Input.GetKey(KeyCode.RightArrow), Input.GetKey(KeyCode.D), speedRotation * Time.deltaTime, Direction.RIGHT);
        checkRotation(Input.GetKey(KeyCode.UpArrow), Input.GetKey(KeyCode.Z), -speedRotation * Time.deltaTime, Direction.UP);

        // Angle Information
        leftCamText.text = "Caméra gauche angles, X : " + leftCamera.transform.eulerAngles.x + ", Y : " + leftCamera.transform.eulerAngles.y + ", Z : " + leftCamera.transform.eulerAngles.z;
        rightCamText.text = "Caméra droite angles, X : " + rightCamera.transform.eulerAngles.x + ", Y : "  + rightCamera.transform.eulerAngles.y + ", Z : " + rightCamera.transform.eulerAngles.z;
    }

    private void checkRotation(bool rightCamInput, bool leftCamInput, float rotationVal, Direction direction)
    {
        float axisRotationValue = 0f;

        bool moveBothCamera = toggle.isOn;

        if (rightCamInput || leftCamInput)
        {
            axisRotationValue = rotationVal;
        }

        if (rightCamInput || moveBothCamera)
        {
            checkDirection(rightCamera, direction, axisRotationValue);
        }

        if (leftCamInput || moveBothCamera)
        {
            checkDirection(leftCamera, direction, axisRotationValue);
        }
    }

    private void checkDirection(Camera camera, Direction direction, float axisRotationValue)
    {
        if (direction == Direction.DOWN || direction == Direction.UP)
        {
            camera.transform.Rotate(new Vector3(axisRotationValue, 0f, 0f));
        }
        else if (direction == Direction.RIGHT || direction == Direction.LEFT)
        {
            camera.transform.Rotate(new Vector3(0f, axisRotationValue, 0f));
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 40), "Distance entre les caméras : " + distanceEntreLesCamera);
        distanceEntreLesCamera = GUI.HorizontalSlider(new Rect(10, 25, 200, 10), distanceEntreLesCamera, 0, 10);
    }

    public void reset()
    {
        distanceEntreLesCamera = 10f;
        leftCamera.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        rightCamera.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}
