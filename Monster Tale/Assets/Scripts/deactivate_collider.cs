using UnityEngine;
using System.Collections;

public class deactivate_collider : MonoBehaviour {
    public GameObject target;
	// Use this for initialization

    public void DeactivateTarget()
    {
        target.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void ActivateTarget()
    {
        target.GetComponent<CapsuleCollider>().enabled = true;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
