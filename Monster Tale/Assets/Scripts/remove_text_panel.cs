using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_text_panel : MonoBehaviour {
    /*This script hides the text panel either when
      1. Player steps into text
      2. An action has been triggered
      */ 
    public GameObject panel;
    public door_button_open script;
    public GameObject scriptObject;

    //Hides selected text
    public void DisableText()
    {
        panel.SetActive(false);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            DisableText();
        }
    }


	// Use this for initialization
	void Start () {
        script = scriptObject.GetComponent<door_button_open>();
	}
	
	// Update is called once per frame
	void Update () {
        if (script.interacted)
        {
            DisableText();
        }
    }
}
