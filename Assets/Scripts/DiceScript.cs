using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	public static Rigidbody rb;
	public Vector3 initPosition;
	public bool isRolled;
	public bool hasLanded;
	private float startingTime;
	int diceCount = 0;

	// Use this for initialization
	void Start () {
		initPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		rb = GetComponent<Rigidbody> ();
		isRolled = false;
		hasLanded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRolled && !hasLanded) {
			if((Time.time - startingTime > 1) && rb.velocity.magnitude < 0.005){
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				isRolled = false;
				hasLanded = true;

				CalculateDiceCount();
			}
		}
	}

	void RollTheDice(Vector3 lastPos) {
		float dirX = Random.Range (0, 0.4f);
		float dirY = Random.Range (0, 0.4f);
		float dirZ = Random.Range (0, 0.4f);
		rb.AddTorque (dirX, dirY, dirZ);
	}

	public void startGravity() {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = true;
		rb.isKinematic = false;
		isRolled = true;
		hasLanded = false;
		startingTime = Time.time;
	}

	void CalculateDiceCount() {
		float dotProduct = 0;
		float currentDotProduct;

		if ((currentDotProduct = Vector3.Dot (transform.forward, Vector3.up)) > 0) {
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 4;
			}		
		}
		if ((currentDotProduct = Vector3.Dot (-transform.forward, Vector3.up)) > 0) {
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 3;
			}
		}
		if ((currentDotProduct = Vector3.Dot (transform.up, Vector3.up)) > 0) {
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 6;
			}
		}
		if ((currentDotProduct = Vector3.Dot (-transform.up, Vector3.up)) > 0) {
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 1;
			}
		}
		if ((currentDotProduct = Vector3.Dot (transform.right, Vector3.up)) > 0) {
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 2;
			}
		}
		if ((currentDotProduct = Vector3.Dot (-transform.right, Vector3.up)) > 1) {
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 5;
			}
		}

		Debug.Log ("diceCount :" + diceCount);
	}

	public int GetDiceCount() {
		return diceCount;
	}
}
