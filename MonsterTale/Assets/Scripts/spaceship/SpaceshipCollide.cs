using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollide : MonoBehaviour {
    public bool hitSafeZone;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("safe"))
        {
            hitSafeZone = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("safe"))
        {
            hitSafeZone = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        hitSafeZone = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
