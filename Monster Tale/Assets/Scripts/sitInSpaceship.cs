using UnityEngine;
using System.Collections;

public class sitInSpaceship : MonoBehaviour {
	private Vector3 spaceshipPosition = new Vector3(62.69f, 0.83f, -0.19f);
	public bool isInsideSpaceship;

	public void TeleportToSpaceship(){
		transform.localPosition = spaceshipPosition;
		isInsideSpaceship = true;
	}
	// Use this for initialization
	void Start () {
		isInsideSpaceship = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

}


// player didnt move with the ship
// it went down frame by frame
// when the player enter the ship it kept moving for a bit
// 
