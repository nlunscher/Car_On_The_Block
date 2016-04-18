using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		float moveHoriz = Input.GetAxis("Horizontal");
		float moveVert = Input.GetAxis("Vertical");

		Vector3 move = new Vector3 (moveHoriz, 0.0f, moveVert);

		rb.AddForce (move * speed);
	}
}
