using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	public GameObject player;
	public float speed;
	public Camera overheadCamera;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		// moveWithPlayer();

		// only move if this camera is enabled
		if (overheadCamera.enabled)
		{
			moveCameraDirectly ();
		}
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
