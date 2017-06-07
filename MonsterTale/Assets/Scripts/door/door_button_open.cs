using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//need to figure out how to keep rotating and then set interacted to true
public class door_button_open : MonoBehaviour
{
    public GameObject[] buttons; //first part of button
    private ButtonInteract script;
    public GameObject scriptObject;

    public Material buttonGlow; //shader for the glow on buttons
    public GameObject[] sounds;

    public bool interacted; //whether door has been opened
    private bool played;
    private Material buttonOriginalColor;
    private Material[] buttonMaterials; //button's material array (from mesh renderer)

    public GameObject door; //door to be animated
    private bool rotating;
    public float tiltAngle = 110.0F; //how far the door rotates
    public float smooth = 0.50F; //ease of door opening


    private void Sequence()
    {
        StartCoroutine(Seq());
    }

    private IEnumerator Seq()
    {
        //sound and color change happens at the same time
        yield return StartCoroutine(PlaySound());
        ChangeButtonColor(buttonGlow);
        StartCoroutine(OpenDoor());

        //turns off button after 3 seconds
        yield return new WaitForSeconds(3);
        StartCoroutine(Settle());

        //yield return StartCoroutine(OpenDoor()); // turns off button when door is completely open
    }

    //turns button off (can be turned on again)
    private IEnumerator Settle()
    {
        script.on = false;
        yield return null;
    }
    //Changes the glow on the button to buttonGlow
    private void ChangeButtonColor(Material mat)
    {
        buttonMaterials[1] = mat;
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Renderer>().materials = buttonMaterials;
        }

    }
    public void ChangeButtonColor(string str)
    {
        if (str == "original")
        {
            buttonMaterials[1] = buttonOriginalColor;
            foreach (GameObject button in buttons)
            {
                button.GetComponent<Renderer>().materials = buttonMaterials;
            }
        }

    }

    private IEnumerator OpenDoor()
    {
        rotating = true;
        while (rotating)
        {
            yield return null;
            // Vector3 to = new Vector3(0, door.transform.localEulerAngles.y, tiltAngle);
            Vector3 to = new Vector3(door.transform.localEulerAngles.x, tiltAngle, door.transform.localEulerAngles.z);

            //change 0.01
            if (Vector3.Distance(door.transform.eulerAngles, to) > 0.1f)
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
    private IEnumerator PlaySound()
    {
        foreach(GameObject sound in sounds)
        {
            sound.GetComponent<AudioSource>().Play();
        }
        yield return null;
    }

    // Use this for initialization
    void Start()
    {
        script = scriptObject.GetComponent<ButtonInteract>();
        buttonOriginalColor = buttons[0].GetComponent<Renderer>().materials[1];
        buttonMaterials = buttons[1].GetComponent<Renderer>().materials;
        interacted = rotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Executes sequence once
        if (script.on && !interacted)
        {
            Sequence();
            interacted = true;
        }

    }
}