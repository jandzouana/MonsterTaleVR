using UnityEngine;
using System.Collections;

public class hide_on_collision : MonoBehaviour {
    public GameObject target;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            target.SetActive(false);
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
