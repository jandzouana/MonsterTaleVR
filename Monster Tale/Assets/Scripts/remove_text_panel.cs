using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_text_panel : MonoBehaviour {
    public GameObject panel;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            panel.SetActive(false);
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
