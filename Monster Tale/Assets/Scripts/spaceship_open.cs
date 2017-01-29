using UnityEngine;
using System.Collections;

public class spaceship_open : MonoBehaviour {
	public GameObject spaceshipCollider;// GO where the script for spaceship vicinity is located
	public GameObject spaceshipTop;// var for the top of the spaceship so we can open it
	public float smooth = 0.50F;
	private float tiltAngle = 130.0F;
	private spaceship_vicinity enteredValueScript;// script return that tell us whether the player is in the vicinity
	private bool rotating = true;
	public bool isOpen = false; 
	public bool spaceshipClick;

	// Function that uses transform functions to open the spaceship

	public void SpaceshipClick(){
		spaceshipClick = true;
	}
	public void SpaceshipOpen(){
		rotating = true;
		isOpen = true;
		if (rotating)
		{
			Vector3 to = new Vector3(0, spaceshipTop.transform.localEulerAngles.y, tiltAngle);
			if (Vector3.Distance(spaceshipTop.transform.eulerAngles, to) > 0.01f)
				//if (spaceshipTop.transform.localEulerAngles.z < tiltAngle)
			{
				spaceshipTop.transform.eulerAngles = Vector3.Lerp(spaceshipTop.transform.rotation.eulerAngles, to, Time.deltaTime * smooth);
			}
			else
			{
				spaceshipTop.transform.eulerAngles = to;
				rotating = false;
			}
		}
	}
	// Function that uses transform functions to close the spaceship
	public void SpaceshipClose(){
		isOpen = false;
		rotating = true;
		if (rotating)
		{
			Vector3 to = new Vector3(0, spaceshipTop.transform.localEulerAngles.y, 0);
			//if (spaceshipTop.transform.localEulerAngles.z > tiltAngle)
			if (Vector3.Distance(spaceshipTop.transform.eulerAngles, to) > 0.01f)
			{
				spaceshipTop.transform.eulerAngles = Vector3.Lerp(spaceshipTop.transform.rotation.eulerAngles, to, Time.deltaTime * smooth);
			}
			else
			{
				spaceshipTop.transform.eulerAngles = to;
				rotating = false;
			}
		}
	}
	// Use this for initialization
	void Start () {
		enteredValueScript = spaceshipCollider.GetComponent <spaceship_vicinity> ();
		isOpen = false;
		spaceshipClick = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (enteredValueScript.entered == true && !spaceshipClick) {
			SpaceshipOpen ();
			Debug.Log ("Hello");
		} 
		else if (enteredValueScript.entered == false) {
			SpaceshipClose ();
		} else if (spaceshipClick) {
			SpaceshipClose();
			Debug.Log ("Spaceship click");
		}
	}	
}


