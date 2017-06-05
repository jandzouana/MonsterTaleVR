using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//need to figure out how to keep rotating and then set interacted to true
public class door_button_open : MonoBehaviour
{
    public GameObject[] buttons; //first part of button
    public ButtonInteract script;
    public GameObject scriptObject;

    public Material buttonGlow; //shader for the glow on buttons
    public GameObject[] sounds;

    public bool interacted; //whether door has been opened

    private Material buttonOriginalColor;
    private Material[] buttonMaterials; //button's material array (from mesh renderer)

    public GameObject door; //door to be animated
    private bool rotating;
    public float tiltAngle = 110.0F; //how far the door rotates
    public float smooth = 0.50F; //ease of door opening

    //Triggers all actions needed when button has been pressed
    public void TriggerOn(){      
        OpenDoor();
        ChangeButtonColor();
        PlaySound();

    }
    //Changes the glow on the button to buttonGlow
    public void ChangeButtonColor()
    {

        buttonMaterials[1] = buttonGlow;
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Renderer>().materials = buttonMaterials;
        }

    }
    public void ChangeButtonColorBack()
    {
        /*
        buttonMaterials[1] = buttonOriginalColor;
        buttonMaterials2[1] = buttonOriginalColor;


        button.GetComponent<Renderer>().materials = buttonMaterials;
        button2.GetComponent<Renderer>().materials = buttonMaterials;
        */
    }
    private void OpenDoor()
    {
        rotating = true;
        if (rotating)
        {
            // Vector3 to = new Vector3(0, door.transform.localEulerAngles.y, tiltAngle);
            Vector3 to = new Vector3(door.transform.localEulerAngles.x, tiltAngle, door.transform.localEulerAngles.z);

            if (Vector3.Distance(door.transform.eulerAngles, to) > 0.01f)
            {
                door.transform.eulerAngles = Vector3.Lerp(door.transform.rotation.eulerAngles, to, Time.deltaTime * smooth);
            }
            else
            {
                door.transform.eulerAngles = to;
                rotating = false;
            }
        }
    }
    //plays button sound and door open sound;
    public void PlaySound()
    {
        foreach(GameObject sound in sounds)
        {
            sound.GetComponent<AudioSource>().Play();
        }
        interacted = true;
    }

    // Use this for initialization
    void Start()
    {
        script = scriptObject.GetComponent<ButtonInteract>();
        buttonOriginalColor = buttons[1].GetComponent<Renderer>().materials[1];
        buttonMaterials = buttons[1].GetComponent<Renderer>().materials;
        interacted = rotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.on && !interacted)
        {
            TriggerOn();
        }

    }
}