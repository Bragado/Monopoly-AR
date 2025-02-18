﻿using System.Collections;
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
		rb = GetComponent<Rigidbody> ();
		isRolled = false;
		hasLanded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRolled && !hasLanded) {

			RollTheDice(rb.transform.position);
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
		float dirX = Random.Range (0, 1f);
		float dirY = Random.Range (0, 1f);
		float dirZ = Random.Range (0, 1f);
		rb.AddTorque (dirX, dirY, dirZ);
	}

	public void startGravity() {
		/*Debug.Log(Vector3.up);
		Debug.Log(transform.forward);
		Debug.Log(transform.right);
		Debug.Log(transform.up);
		Debug.Log(-transform.up);*/
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

		/*Debug.Log(Vector3.up);
		Debug.Log(transform.forward);
		Debug.Log(transform.right);
		Debug.Log(transform.up);
		Debug.Log(-transform.up);*/

		if ((currentDotProduct = Vector3.Dot (transform.forward, transform.parent.up)) > 0) {
			Debug.Log(currentDotProduct);
			Debug.Log(4);
			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 4;
			}		
		}
		if ((currentDotProduct = Vector3.Dot (-transform.forward, transform.parent.up)) > 0) {
			Debug.Log(currentDotProduct);
			Debug.Log(3);

			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 3;
			}
		}
		if ((currentDotProduct = Vector3.Dot (transform.up, transform.parent.up)) > 0) {
			Debug.Log(currentDotProduct);
			Debug.Log(6);

			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
                diceCount = 6;
			}
		}
		if ((currentDotProduct = Vector3.Dot (-transform.up, transform.parent.up)) > 0) {
			Debug.Log(currentDotProduct);
			Debug.Log(1);

			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 1;
			}
		}
		if ((currentDotProduct = Vector3.Dot (transform.right, transform.parent.up)) > 0) {
			Debug.Log(currentDotProduct);
			Debug.Log(2);

			if(currentDotProduct > dotProduct) {
				dotProduct = currentDotProduct;
				diceCount = 2;
			}
		}
		if ((currentDotProduct = Vector3.Dot (-transform.right, transform.parent.up)) > 0) {
			Debug.Log(currentDotProduct);
			Debug.Log(5);

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
