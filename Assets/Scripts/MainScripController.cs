using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScripController : MonoBehaviour {

	public Camera overHeadCamera;
	public Camera carCamera;
	public Text viewText;


	// Use this for initialization
	void Start () {
		overHeadCamera.enabled = true;
		carCamera.enabled = false;
		viewText.text = "Overhead View";
	}
	
	// Update is called once per frame
	void Update () {
		
		// switch cameras with space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			switchCameras();
		}
	}

	void switchCameras ()
	{
		bool temp = overHeadCamera.enabled;
		overHeadCamera.enabled = carCamera.enabled;
		carCamera.enabled = temp;

		if (overHeadCamera.enabled)
		{
			viewText.text = "Overhead View";
		}
		else
		{
			viewText.text = "Car View";
		}
	}
}
