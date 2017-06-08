using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollide : MonoBehaviour {
    public bool hit;
    public string[] compares;

    private void OnTriggerEnter(Collider col)
    {
        foreach (string compare in compares)
        {
            if (col.CompareTag(compare))
            {
                hit = true;
            }
        }
    }
    private void OnTriggerStay(Collider col)
    {
        foreach (string compare in compares)
        {
            if (col.CompareTag(compare))
            {
                hit = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        foreach (string compare in compares)
        {
            if (col.CompareTag(compare))
            {
                hit = false;
            }
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
