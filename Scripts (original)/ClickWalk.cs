using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickWalk : MonoBehaviour{

	public Transform vrCamera;
	public float speed;
	private float MaxSpeed = 5.0f; //max speed
	public bool moveForward;
	private CharacterController cc;

	public float Acceleration = 10;
	public float Deceleration = 10;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		speed = 0;
	}
		
	// Update is called once per frame
	void Update () {
		Debug.Log (speed.ToString ("F2"));

		/*The input is both mouse and trigger on carboard*/
		if (Input.GetButton ("Fire1")) {
			moveForward = true;
		} 
		else { 
			Debug.Log ("stopped moving");
			moveForward = false;
		}
		/*Gradual acceleration*/
		if (Input.GetButton ("Fire1")&& (speed < MaxSpeed)){
			speed = speed + Acceleration * Time.deltaTime;
		}
		else{
			if (speed > Deceleration * Time.deltaTime)
				speed = speed - Deceleration * Time.deltaTime;
			else if (speed < -Deceleration * Time.deltaTime) {
				speed = speed + Deceleration * Time.deltaTime;
			}
			else speed = 0;
		}
		/*Transforms object position*/
		if (moveForward) {
			Vector3 forward = vrCamera.TransformDirection (Vector3.forward); //checking for forward direction
			cc.SimpleMove(forward * speed);
		}
			
	}
}


















/*Changing speed; speed start must be 0;
		if (Input.GetButton ("Fire1")&& (speed < MaxSpeed)){
			speed = speed + Acceleration * Time.deltaTime;
		}
		else{
			if (speed > Deceleration * Time.deltaTime)
				speed = speed - Deceleration * Time.deltaTime;
			else if (speed < -Deceleration * Time.deltaTime) {
				speed = speed + Deceleration * Time.deltaTime;
			}
			else speed = 0;
		}
		

		*/
/*
 * 
	public Transform vrCamera;
	public float speed;
	private float defspeed = 5.0f; //max speed
	public bool moveForward;
	private CharacterController cc;

	public float Acceleration = 10;
	public float Deceleration = 10;


	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		speed = defspeed;
	}
		
	// Update is called once per frame
	void Update () {
		Debug.Log (speed.ToString ("F2"));

		if (Input.GetButton ("Fire1") && moveForward == false) {
			moveForward = true;
		} else { 
			moveForward = false;
		}
		
		if (moveForward) {
			Vector3 forward = vrCamera.TransformDirection (Vector3.forward); //checking for forward direction
			cc.SimpleMove(forward * speed);
		}


	}
	*/