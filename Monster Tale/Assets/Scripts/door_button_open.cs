using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_button_open : MonoBehaviour, IGvrGazeResponder {
    public Animator anim; //door animator. set automatically
    public GameObject door; //door that needs to be animated
    public GameObject button; //first part of button
    public GameObject button2; //2nd part of button

    public Material buttonGlow; //shader for the glow on buttons
    public Material[] buttonMaterials; //button's material array (from mesh renderer)
    public Material[] buttonMaterials2;

    public GameObject DistanceCollider;
    public door_vicinity script; //script for min distance needed to button
    public GameObject activeCamera;
    public GameObject sound;
    public GameObject sound2;

    private bool interacted;


    //Enables and disables movement when player gazes at button
    public void SetMovement(bool b)
    {
        activeCamera.GetComponent<AutoWalk>().canMove = b;
    }

    //Changes the glow on the button to buttonGlow
    public void ChangeButtonColor()
    {
        buttonMaterials[1] = buttonGlow;
        buttonMaterials2[1] = buttonGlow;


        button.GetComponent<Renderer>().materials = buttonMaterials;
        button2.GetComponent<Renderer>().materials = buttonMaterials;
    }

    public void OnGazeEnter() {
        Debug.Log("Gaze enter");
        SetMovement(false);
    }
    public void OnGazeExit() {
        SetMovement(true);
    }
    public void OnGazeTrigger()
    {
        if(script.entered == true)
        {
            interacted = true;
            anim.SetBool("button_pressed", true);
            ChangeButtonColor();
            sound.GetComponent<AudioSource>().Play();
            sound2.GetComponent<AudioSource>().Play();

            Debug.Log("Pressed");
        }
    }

	// Use this for initialization
	void Start () {
        script = DistanceCollider.GetComponent<door_vicinity>();
        Debug.Log("Hello");
        anim = door.GetComponent<Animator>();
        buttonMaterials = button.GetComponent<Renderer>().materials;
        buttonMaterials2 = button2.GetComponent<Renderer>().materials;

        interacted = false;
    }

    // Update is called once per frame
    void Update () {
		if (interacted)
        {
            GetComponent<Collider>().enabled = false;
        }
	}
}
