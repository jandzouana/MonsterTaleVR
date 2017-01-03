using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_camera_init : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GetComponent<MouseLook>().enabled = false;
        transform.localEulerAngles = new Vector3(0, 90, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
