using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_button : MonoBehaviour {
    private ButtonInteract scriptButton;
    private door_button_open scriptDoor;
    public GameObject button;
    public GameObject door;

    private void reset()
    {
        scriptDoor.ChangeButtonColor("original");
        scriptDoor.interacted = false;
    }

        // Use this for initialization
        void Start () {
        scriptDoor = door.GetComponent<door_button_open>();
        scriptButton = button.GetComponent <ButtonInteract>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!scriptButton.on && scriptDoor.interacted && scriptDoor.willReset)
        {
            reset();
        }
	}
}
