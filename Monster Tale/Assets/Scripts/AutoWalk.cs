﻿using UnityEngine;
using System.Collections;

public class AutoWalk : MonoBehaviour {

	public Transform vrCamera;
	public float speed;
	private float defspeed = 3.0f;
	public bool moveForward;
	private CharacterController cc;
    public bool canMove;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		speed = defspeed;
        canMove = true;
	}

	// Update is called once per frame
	void Update () {

		if (moveForward) {

			Vector3 forward = vrCamera.TransformDirection (Vector3.forward); //checking for forward direction
			cc.SimpleMove(forward * speed);

		}

		if (Input.GetButtonDown("Fire1") && canMove && moveForward == false) {
			moveForward = true;
		} 
		else if (Input.GetButtonDown("Fire1")&&moveForward == true){
			moveForward = false;
		}

	}
}
