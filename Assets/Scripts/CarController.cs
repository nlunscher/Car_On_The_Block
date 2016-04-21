using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CarController : MonoBehaviour {

	public float speed;
	public Text carText;
	public Text consoleText;
	public GameObject groundObject;
	public int ROTATE_STEPS;

	private Rigidbody rb;
	private int numCollisions;
	private int state; // 0: move forward, 1: backup for a time, 2: turn/rotate left 90 degrees

	private TimeSpan BACKUP_TIME = TimeSpan.FromSeconds(3);
	private DateTime backupStart;
	private int rotateElapse;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		numCollisions = 0;
		state = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (state == 0) {
			state0 ();
		}
		else if (state == 1) {
			state1 ();
		}
		else if (state == 2) {
			state2 ();
		}
		printCarInfo ();
	}

	void OnCollisionEnter(Collision collision)
	{ 
		if (collision.gameObject != groundObject) {
			numCollisions += 1;

			backupStart = DateTime.Now;
			state = 1;
		}
	}

	void state0()
	{
		// move forward

		moveCar (1);
	}

	void state1()
	{
		// move backwards 
		moveCar (-1*(13/15.0f));

		DateTime current = DateTime.Now;
		TimeSpan backupElaspse = current - backupStart;
		print ("Backup Elapse: " + backupElaspse);

		if (backupElaspse >= BACKUP_TIME) {
			rotateElapse = 0;
			state = 2;
		}
	}

	void state2()
	{
		// rotate 90 degrees
		print ("Entered State2");

		float rotX = 0;
		float rotY = -90.0f / ROTATE_STEPS;
		float rotZ = 0;

		print ("Rotation - X: " + rotX + " Y: " + rotY + " Z: " + rotZ);
		transform.Rotate(new Vector3 (rotX, rotY, rotZ));

		rotateElapse += 1;
		print ("Rotate Elapse: " + rotateElapse);

		if (rotateElapse >= ROTATE_STEPS) {
			rotateElapse = 0;
			state = 0;
		}
	}

	public Vector3 getForwardDirection()
	{
		float xang = (float)(transform.rotation.eulerAngles.x * Math.PI / 180);
		float yang = (float)(transform.rotation.eulerAngles.y * Math.PI / 180);
		float zang = (float)(transform.rotation.eulerAngles.z * Math.PI / 180);

		//print ("X: " + xang + " Y: " + yang + " Z: " + zang);

		float xdir = (float)(1*Math.Sin (yang) + 0*Math.Cos (yang));
		float ydir = 0;
		float zdir = (float)(1*Math.Cos (yang) - 0*Math.Sin (yang));
		float mag = (float)Math.Sqrt (xdir * xdir + ydir * ydir + zdir * zdir);
		xdir /= mag;
		ydir /= mag;
		zdir /= mag;
		Vector3 direction = new Vector3 (xdir, ydir, zdir);

		return direction;
	}

	public Quaternion getCarRotation()
	{
		return transform.rotation;
	}

	void moveCar(float moveAmount)
	{
		Vector3 carDirection = getForwardDirection ();
		Vector3 move = carDirection * moveAmount;

		rb.AddForce (move * speed);
	}

	void printCarInfo()
	{
		string s = "Car Collisions: " + numCollisions + "\n";
		s += "Car State: " + state + "\n";
		s += "Car Direction: " + getForwardDirection ().ToString ();
		carPrint (s);
	}

	void carPrint(string s)
	{
		carText.text = s;
	}

	//void print(string s)
	//{
	//	consoleText.text = s + "\n" + consoleText.text;
	//}
}
