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

	// Use this for initialization
	void Start () {
		enteredValueScript = spaceshipCollider.GetComponent <spaceship_vicinity> ();
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (enteredValueScript.entered == true) {
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

		else if (enteredValueScript.entered == false) {
			isOpen = false;
			rotating = true;
			if (rotating)
   			{
         		Vector3 to = new Vector3(0, spaceshipTop.transform.localEulerAngles.y, 0);
         		Debug.Log(spaceshipTop.transform.localEulerAngles.z);
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


	}	

	// Function that uses transform functions to open the spaceship
 	private void SpaceshipOpen () {
		//	float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;

 	}
}


