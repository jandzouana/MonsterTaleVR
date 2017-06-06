using UnityEngine;
using System.Collections;

public class disable_script : MonoBehaviour {
    public GameObject target;
    // Use this for initialization

    public void DeactivateTarget()
    {
        target.GetComponent<spaceship_open>().enabled = false;
    }

    public void ActivateTarget()
    {
        target.GetComponent<spaceship_open>().enabled = true;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
