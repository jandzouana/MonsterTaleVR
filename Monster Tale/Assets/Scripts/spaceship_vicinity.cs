using UnityEngine;
using System.Collections;

public class spaceship_vicinity : MonoBehaviour {
	public bool entered;


	// Use this for initialization
	void Start () {
		entered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Function to see if player is in the collider spaceship area
	private void OnTriggerEnter (Collider col) {
		if (col.CompareTag("Player")) {
			entered = true;
			Debug.Log("entered"); //************
		}	
	}

	// Function to see if player is in the collider spaceship area and leaves
	private void OnTriggerExit (Collider col) {
			entered = false;
			Debug.Log("exited");// ***************
	}
}
