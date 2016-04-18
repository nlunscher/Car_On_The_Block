using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CarController : MonoBehaviour {

	public float speed;
	public Text carText;
	public Text consoleText;
	public GameObject groundObject;

	private Rigidbody rb;
	private int numCollisions;
	private int state; // 0: move forward, 1: backup for a time, 2: turn/rotate left 90 degrees

	private TimeSpan BACKUP_TIME = TimeSpan.FromSeconds(3);
	private DateTime backupStart;
	private int ROTATE_STEPS = 90;
	private int retateElapse;

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
		float moveHoriz = 0;
		float moveVert = 1;

		Vector3 move = new Vector3 (moveHoriz, 0.0f, moveVert);

		rb.AddForce (move * speed);
	}

	void state1()
	{
		float moveHoriz = 0;
		float moveVert = -1;

		Vector3 move = new Vector3 (moveHoriz, 0.0f, moveVert);

		rb.AddForce (move * 13);

		DateTime current = DateTime.Now;
		TimeSpan backupElaspse = current - backupStart;
		print ("Backup Elapse: " + backupElaspse);

		if (backupElaspse >= BACKUP_TIME) {
			retateElapse = 0;
			state = 2;
		}
	}

	void state2()
	{
		float rotX = 0;
		float rotY = -90.0f / ROTATE_STEPS;
		float rotZ = 0;

		transform.Rotate(new Vector3 (rotX, rotY, rotZ));

		retateElapse += 1;
		print ("Rotate Elapse: " + retateElapse);

		if (retateElapse >= ROTATE_STEPS) {
			ROTATE_STEPS = 0;
			state = 0;
		}
	}

	void printCarInfo()
	{
		string s = "Car Collisions: " + numCollisions + "\n";
		s += "Car State: " + state;
		carPrint (s);
	}

	void carPrint(string s)
	{
		carText.text = s;
	}

	void print(string s)
	{
		consoleText.text = s + "\n" + consoleText.text;
	}
}
