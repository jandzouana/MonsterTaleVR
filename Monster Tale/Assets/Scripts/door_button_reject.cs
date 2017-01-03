using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_button_reject : MonoBehaviour, IGvrGazeResponder {
    //Door doesn't need to be animated here
    //public Animator anim; //door animator. set automatically
    //public GameObject door; //door that needs to be animated

    public GameObject button; //first part of button
    public GameObject button2; //2nd part of button

    public Material buttonGlow; //shader for the glow on buttons
    private Material buttonOriginalColor;
    public Material[] buttonMaterials; //button's material array (from mesh renderer)
    public Material[] buttonMaterials2;

    public GameObject DistanceCollider;
    public gate_vicinity script; //script for min distance needed to button
    public GameObject activeCamera;
    public GameObject sound;
    public GameObject sound2;

    private bool interacted;


    public void SetMovement(bool b)
    {
        activeCamera.GetComponent<AutoWalk>().canMove = b;
    }
    public void ChangeButtonColor(Material mat)
    {
        buttonMaterials[1] = mat;
        buttonMaterials2[1] = mat;

        button.GetComponent<Renderer>().materials = buttonMaterials;
        button2.GetComponent<Renderer>().materials = buttonMaterials;
    }

    public void OnGazeEnter()
    {
        Debug.Log("Gaze enter gate");
        SetMovement(false);
    }
    public void OnGazeExit()
    {
        SetMovement(true);
    }
    public void OnGazeTrigger()
    {
        if (script.entered == true)
        {
            //opens door
            //anim.SetBool("button_pressed", true);
            ChangeButtonColor(buttonGlow);
            sound.GetComponent<AudioSource>().Play();

            //Plays sound when gate opens
            //sound2.GetComponent<AudioSource>().Play();

            interacted = true;
            Debug.Log("Pressed gate 1");

        }
    }

    // Use this for initialization
    void Start()
    {
        script = DistanceCollider.GetComponent<gate_vicinity>();
        Debug.Log("Hello");
        //anim = door.GetComponent<Animator>();

        buttonMaterials = button.GetComponent<Renderer>().materials;
        buttonMaterials2 = button2.GetComponent<Renderer>().materials;

        buttonOriginalColor = buttonMaterials[1];
        interacted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!script.entered && interacted)
        {
            ChangeButtonColor(buttonOriginalColor);
            interacted = false;
        }
    }
}
