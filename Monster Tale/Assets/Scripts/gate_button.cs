using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_button : MonoBehaviour, IGvrGazeResponder
{
    public Animator anim; //door animator. set automatically
    public GameObject door; //door that needs to be animated

    public GameObject button; //first part of button
    public GameObject button2; //2nd part of button
    public GameObject button2_1; //2nd button

    public Material buttonGlow; //shader for the glow on buttons
    private Material buttonOriginalColor;
    public Material[] buttonMaterials; //button's material array (from mesh renderer)
    //public Material[] buttonMaterials2;
    public Material[] buttonMaterials2_1;


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
        //buttonMaterials2[1] = mat;
        buttonMaterials2_1[1] = buttonGlow;


        button.GetComponent<Renderer>().materials = buttonMaterials;
        //button2.GetComponent<Renderer>().materials = buttonMaterials;
        button2_1.GetComponent<Renderer>().materials = buttonMaterials;
    }

    public void OnGazeEnter()
    {
        Debug.Log("Gaze enter");
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
            interacted = true;
            anim.SetBool("button_pressed", true);
            ChangeButtonColor(buttonGlow);
            sound.GetComponent<AudioSource>().Play();
            sound2.GetComponent<AudioSource>().Play();

            Debug.Log("Pressed");
        }
    }

    // Use this for initialization
    void Start()
    {
        script = DistanceCollider.GetComponent<gate_vicinity>();
        Debug.Log("Hello");
        anim = door.GetComponent<Animator>();

        buttonMaterials = button.GetComponent<Renderer>().materials;
        //buttonMaterials2 = button2.GetComponent<Renderer>().materials;
        buttonMaterials2_1 = button2_1.GetComponent<Renderer>().materials;
        buttonOriginalColor = buttonMaterials[1];

        interacted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
