using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_button_open : MonoBehaviour
{
    public GameObject button; //first part of button
    public GameObject button2; //2nd part of button
    public ButtonInteract script;
    public GameObject scriptObject;

    public Material buttonGlow; //shader for the glow on buttons
    public GameObject DistanceCollider;
    public GameObject activeCamera; //or player. needed for autowalk
    public GameObject sound;
    private bool canInteract;
    public bool interacted; //whether door has been opened

    private Material buttonOriginalColor;
    private Material[] buttonMaterials; //button's material array (from mesh renderer)
    private Material[] buttonMaterials2;

    //Changes the glow on the button to buttonGlow
    public void ChangeButtonColor()
    {
        buttonMaterials[1] = buttonGlow;
        buttonMaterials2[1] = buttonGlow;


        button.GetComponent<Renderer>().materials = buttonMaterials;
        button2.GetComponent<Renderer>().materials = buttonMaterials;
        interacted = true;
    }
    public void ChangeButtonColorBack()
    {
        buttonMaterials[1] = buttonOriginalColor;
        buttonMaterials2[1] = buttonOriginalColor;


        button.GetComponent<Renderer>().materials = buttonMaterials;
        button2.GetComponent<Renderer>().materials = buttonMaterials;
    }
    //plays button sound and door open sound;
    public void PlaySound()
    {
        sound.GetComponent<AudioSource>().Play();
    }

    // Use this for initialization
    void Start()
    {
        script = scriptObject.GetComponent<ButtonInteract>();
        buttonMaterials = button.GetComponent<Renderer>().materials;
        buttonMaterials2 = button2.GetComponent<Renderer>().materials;
        interacted = false;
        canInteract = false;
        buttonOriginalColor = button.GetComponent<Renderer>().materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted == true)
        {
            ChangeButtonColorBack();
            interacted = false;
        }
    }
}