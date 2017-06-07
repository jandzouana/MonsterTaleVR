using UnityEngine;
using System.Collections;

public class FlyingMechanism : MonoBehaviour {
    public Transform spaceship;
    private float speed;
    private float rotationSpeed;
    public float defSpeed;
    public float defTurnSpeed;
    public float defReverseSpeed;


    public GameObject player; // where script for sitInSpaceship is located
    public GameObject safeZone;
    public GameObject controller;
    public bool hitSafeZone;
    public GameObject target;
    public GameObject SittingScriptObject;

    private Vector3 euler; //for rotating spaceship
    private bool moveForward;
    private bool moveBack;

    private float counter;
    private sitInSpaceship sittingScript; // function if is sitting

    public void PlaySound(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("safe"))
        {
            hitSafeZone = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("safe"))
        {
            hitSafeZone = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        hitSafeZone = false;
    }

    private IEnumerator RotationMechanisms()
    {
        rotationSpeed = defTurnSpeed;
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        euler.y += secondaryAxis.x;
        euler.x -= secondaryAxis.y;
        spaceship.transform.rotation = Quaternion.Euler(euler*rotationSpeed);
        yield return null;
    }
    private IEnumerator MovingMechanisms()
    {
        //Move Forward
        if (OVRInput.GetDown(OVRInput.Button.One) && sittingScript.isInsideSpaceship && !moveForward && !moveBack)
        {
            moveForward = true;
        }
        //Move Backward
        else if (OVRInput.GetDown(OVRInput.Button.Two) && sittingScript.isInsideSpaceship && !moveBack && !moveForward)
        {
            moveBack = true;
        }

        //Stop
        else if (OVRInput.GetUp(OVRInput.Button.Two) && sittingScript.isInsideSpaceship && moveForward && !moveBack)
        {
            moveForward = false;
            moveBack = false;
        }
        else if (OVRInput.GetUp(OVRInput.Button.One) && sittingScript.isInsideSpaceship && moveBack && !moveForward)
        {
            moveBack = false;
            moveBack = false;
        }
        yield return null;

    }
    public static IEnumerator Frames(int frameCount)
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
    // Use this for initialization
    void Start()
    {
        sittingScript = SittingScriptObject.GetComponent<sitInSpaceship>();
        rotationSpeed = defTurnSpeed; 
        moveForward = moveBack = false;
        //GetComponent<CharacterController>().enabled = false;//disables character controller on start (roatational problems) 
        euler = spaceship.transform.rotation.eulerAngles;
    }


    // Update is called once per frame
    void Update () {
        //Moving forward mechanism
        if (sittingScript.isInsideSpaceship)
        {
            StartCoroutine(MovingMechanisms());
            StartCoroutine(RotationMechanisms());
        }
        if (moveForward)
        {
            speed = defSpeed;
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            spaceship.GetComponent<CharacterController>().Move(forward * speed);
		}
        
        //Moving backward
        else if (moveBack)
        {
            speed = defReverseSpeed;
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            spaceship.GetComponent<CharacterController>().Move(-forward * speed);
        }
        else if (!moveForward && !moveBack)
        {
            speed = 0;
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            spaceship.GetComponent<CharacterController>().Move(-forward * speed);
        }

        /*
        //Exiting out of spaceship
        //While app button is being pressed, check if it has been held for holdTime seconds
        if (GvrController.AppButton && sittingScript.isInsideSpaceship && hasBeenDelayed && hitSafeZone) //add hitsafezone
        {
            counter += Time.deltaTime;
            if (counter > .25f)
            {
                moveBack = false;
                moveForward = false;
            }
            if (counter > holdTime)
            {      
                //unparent player from spaceship
                player.transform.parent = null;
                
                //rotate spaceship to original position
                Vector3 to = new Vector3(0, 0, 0);
                transform.eulerAngles = to; //resets the rotation of spaceship
                player.transform.eulerAngles = to; //resets the rotation of player

                //move the player
                float xpositionPlayer = player.transform.position.x;
                float ypositionPlayer = player.transform.position.y;
                float zpositionPlayer = player.transform.position.z;
                player.transform.position = new Vector3(xpositionPlayer-5, ypositionPlayer, zpositionPlayer-5);
                sittingScript.isInsideSpaceship = false;

                //play sound
                PlaySound(exitSound);

                //enable spaceshipopen gameobject
                spaceshipOpenSound.SetActive(true);

            }
        }
        */

    }
}
