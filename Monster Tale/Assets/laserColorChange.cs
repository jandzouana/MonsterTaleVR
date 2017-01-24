using UnityEngine;
using System.Collections;

public class laserColorChange : MonoBehaviour {
	public float gazeTime = 2f;
	public Material initialLaserMaterial;
	public Material newLaserMaterial;

	private bool gazedAt;
	private float time;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<LineRenderer>().material = initialLaserMaterial;
	}
	
	// Update is called once per frame
	void Update () {

		// if looking at the box then it'll change
		if (gazedAt) {
			gameObject.GetComponent<LineRenderer>().material = newLaserMaterial;
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

	public void PointerDown(){
			
	}	
}
