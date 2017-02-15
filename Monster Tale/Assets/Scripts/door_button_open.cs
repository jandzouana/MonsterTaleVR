using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_button_open : MonoBehaviour {
    public GameObject door; //door that needs to be animated
    public GameObject button; //first part of button
    public GameObject button2; //2nd part of button

    public Material buttonGlow; //shader for the glow on buttons
    public GameObject DistanceCollider;
    public door_vicinity script; //script for min distance needed to button
    public GameObject activeCamera; //or player. needed for autowalk
    public GameObject sound;
    public GameObject sound2;
    private bool canRotate;
    public float tiltAngle = 110.0F;
    public float smooth = 0.50F;
    public bool interacted; //whether door has been opened

    private bool rotating; //for door roatation
    private Material[] buttonMaterials; //button's material array (from mesh renderer)
    private Material[] buttonMaterials2;

    //Enables and disables movement when player gazes at button
    public void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
    //disables player movement
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

    //plays button sound and door open sound;
    public void PlaySound()
    {
        sound.GetComponent<AudioSource>().Play();
        sound2.GetComponent<AudioSource>().Play();
    }

    public void CanRotate()
    {
        canRotate = true;
        interacted = true;
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
    /*
    //for cardboard
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
    */

	// Use this for initialization
	void Start () {
        script = DistanceCollider.GetComponent<door_vicinity>();
        buttonMaterials = button.GetComponent<Renderer>().materials;
        buttonMaterials2 = button2.GetComponent<Renderer>().materials;
        interacted = false;
        canRotate = false;
    }

    // Update is called once per frame
    void Update () {
        if (canRotate)
        {
            OpenDoor();
        }
	}
}
