using UnityEngine;
using System.Collections;

public class CarCameraController : MonoBehaviour {

	public GameObject car;

	private Vector3 offset;
	private CarController carScript; 
	private float carSize;

	// Use this for initialization
	void Start () {
		carSize = transform.localScale.z;

		carScript = (CarController) car.GetComponent (typeof(CarController));
	}
	
	void LateUpdate () {

		Vector3 carDirection = carScript.getForwardDirection ();
		//print ("Direction of Car: " + carDirection);

		transform.position = car.transform.position + carDirection * carSize;

		Quaternion carRotation = carScript.getCarRotation ();

		transform.rotation = carRotation;
	}


}
