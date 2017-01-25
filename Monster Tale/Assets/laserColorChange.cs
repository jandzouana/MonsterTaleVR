using UnityEngine;
using System.Collections;

public class laserColorChange : MonoBehaviour {
	public float gazeTime = 2f;
	public Material initialLaserMaterial;
	public Material newLaserMaterial;
	public bool buttonClicked;

	private bool gazedAt;
	private float time;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<LineRenderer>().material = initialLaserMaterial;
		buttonClicked = false;
	}
	
	// Update is called once per frame
	void Update () {

		// if looking at the ship then color will change
		if (gazedAt) {
			gameObject.GetComponent<LineRenderer>().material = newLaserMaterial;

			if (buttonClicked == true) {
				//call teleport function

			}
		}

		//changes the color back
		if (!gazedAt) {
			gameObject.GetComponent<LineRenderer>().material = initialLaserMaterial;
		}
	}

	public void PointerEnter(){
		gazedAt = true;
	}

	public void PointerExit(){
		gazedAt = false;
	}

	public void PointerClick(){
		buttonClicked = true;
	}
	public void PointerUp(){
		buttonClicked = false;
	}

}
