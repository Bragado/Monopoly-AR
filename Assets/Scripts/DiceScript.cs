using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	public static Rigidbody rb;
	public Vector3 initPosition;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		initPosition = rb.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			float dirX = Random.Range (0, 500);
			float dirY = Random.Range (0, 500);
			float dirZ = Random.Range (0, 500);
			transform.position = new Vector3 (0, 2, 0);
			transform.rotation = Quaternion.identity;
			rb.AddForce (transform.up * 500);
			rb.AddTorque (dirX, dirY, dirZ);
		}
	}

	public void startGravity() {
		rb.useGravity = true;
		Debug.Log("set gravity");
	}
}
