using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MainScripController : MonoBehaviour {

	public Camera overHeadCamera;
	public Camera carCamera;
	public Text viewText;

	public bool constantShots = false;

	private bool takeHiRexShot = false;
	private int resWidth = 480;
	private int resHeight = 360;
	private TimeSpan screenInterval = TimeSpan.FromMilliseconds(500);
	private DateTime lastScreen;

	// Use this for initialization
	void Start () {
		overHeadCamera.enabled = true;
		carCamera.enabled = false;
		viewText.text = "Overhead View";
		lastScreen = DateTime.Now - screenInterval;
	}
	
	// Update is called once per frame
	void Update () {
		
		// switch cameras with space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			switchCameras();
		}

		// take screenshot with 'k'
		if (Input.GetKeyDown("k"))
		{
			takeScreenShot();
		}
		// take screenshot every screenInterval
		if (constantShots && DateTime.Now - lastScreen >= screenInterval) {
			takeScreenShot();
			lastScreen = DateTime.Now;
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

	public static string ScreenShotName(int width, int height)
	{
		string name = string.Format ("{0}/ScreenShots/screen_{1}x{2}_{3}.jpg",
			              Application.dataPath,
			              width, height,
			              System.DateTime.Now.ToString ("yyyy-MM-dd_HH_mm-ss.ffff"));
		return name;
	}

	void takeScreenShot()
	{
		RenderTexture rt = new RenderTexture (resWidth, resHeight, 24);
		carCamera.targetTexture = rt;
		Texture2D screenShot = new Texture2D (resWidth, resHeight, TextureFormat.RGB24, false);
		carCamera.Render ();
		RenderTexture.active = rt;
		screenShot.ReadPixels (new Rect (0, 0, resWidth, resHeight), 0, 0);
		carCamera.targetTexture = null;
		RenderTexture.active = null; // added to avoid errors
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToJPG ();
		string fileName = ScreenShotName (resWidth, resHeight);
		System.IO.File.WriteAllBytes (fileName, bytes);
		Debug.Log (string.Format ("Took screenshot to: {0}", fileName));
	}
}
