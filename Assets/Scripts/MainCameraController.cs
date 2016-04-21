using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	public GameObject player;
	public float speed;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		// moveWithPlayer();

		moveCameraDirectly ();
	}

	void moveCameraDirectly()
	{
		// move camera directionly
		float moveHoriz = Input.GetAxis("Horizontal");
		float moveVert = Input.GetAxis("Vertical");

		Vector3 move = new Vector3 (moveHoriz, 0.0f, moveVert);

		transform.position += move * speed;
	}

	void moveWithPlayer()
	{
		// move to match player movement
		transform.position = player.transform.position + offset;
	}
}
