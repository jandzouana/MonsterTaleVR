using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour {
	private Vector3 startPosition;
	private Vector3 initialPosition;
	private Vector3 endPosition;
    private float t, s;

	private bool hasTouched;
    private bool leftHasTouched;
    private bool rightHasTouched;

    public bool on;
    //specify how fast the button lowers and bounces back to the origial position
	public float timeLower;
	public float timeUpper;
	//specify how far you can push the button
	public float distance;
	//how far you need to push the button to trigger an action
	private float distanceTrigger;

    //game object that contains button
    public GameObject buttonPush;
    //script that enables haptic feedback
    public OculusHapticsController OHCscript;

    //Switch used to trigger an action when button collides with it
    private bool switchTrigger; //if button collided with switch, then switchTrigger is true
    private bool rest; //if button is not moving, rest is true

    public void OnTriggerEnter(Collider col){
        //Checking collisions with hands
        if (col.name.Contains("hand")) hasTouched = true;
        if (col.name.Contains("hands:b_l")) leftHasTouched = true;
        else if (col.name.Contains("hands:b_r")) rightHasTouched = true;
	}

    public void OnTriggerExit(Collider col)
    {

        //Checking collisions with hands
        if (col.name.Contains("hands:b_l")) leftHasTouched = false;
        else if (col.name.Contains("hands:b_r")) rightHasTouched = false;
        if (!(leftHasTouched && rightHasTouched)) hasTouched = false;

    }
    // Use this for initialization
    void Start () {
		hasTouched = leftHasTouched = rightHasTouched = switchTrigger = on = false;
        rest = true;
		initialPosition = new Vector3(buttonPush.transform.localPosition.x, buttonPush.transform.localPosition.y, buttonPush.transform.localPosition.z);
        //change -distance to change which way button moves
        endPosition = new Vector3(buttonPush.transform.localPosition.x - distance, buttonPush.transform.localPosition.y, buttonPush.transform.localPosition.z);
		startPosition = new Vector3(buttonPush.transform.localPosition.x, buttonPush.transform.localPosition.y, buttonPush.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
        //If the player's hand/finger has collided, then lower button in timeLower time
        if (hasTouched){
            s = 0;
		    t+=Time.deltaTime/timeLower;
            buttonPush.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            rest = false;
        }
        //If the player is no longer touching the button, reset button to original position in timeUpper time
        else
        {
			t=0;
			s+=Time.deltaTime/timeUpper;
			startPosition = new Vector3(buttonPush.transform.localPosition.x, buttonPush.transform.localPosition.y, buttonPush.transform.localPosition.z);
            buttonPush.transform.localPosition = Vector3.Lerp(startPosition, initialPosition, s);
            rest = true;
		}
        //turn button on when user has sufficiently pressed the button
        if (hasTouched && t >= timeLower) on = true;
        //vibrate when user pressed button enough
        if (leftHasTouched && t >= timeLower) OHCscript.SimpleVibrate("left", 0);
        else if (rightHasTouched && t >= timeLower) OHCscript.SimpleVibrate("right", 0);
    }
}
