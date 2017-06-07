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
    public GameObject target;
    public GameObject SittingScriptObject;
    public GameObject exitSound;

    private Vector3 euler; //for rotating spaceship
    public bool moveForward;
    public bool moveBack;
    private bool hitSafeZone;

    private Vector3 to;
    private float counter;
    private sitInSpaceship sittingScript; // function if is sitting

    public void PlaySound(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }



    private IEnumerator RotationMechanisms()
    {
        rotationSpeed = defTurnSpeed;
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        euler.y += secondaryAxis.x;
        euler.x -= secondaryAxis.y;
        spaceship.transform.rotation = Quaternion.Euler(euler*rotationSpeed);
        yield return null;
    }
    private IEnumerator MovingMechanisms()
    {
        //Move Forward
        if (OVRInput.GetUp(OVRInput.Button.Two) && sittingScript.isInsideSpaceship && !moveForward)
        {
            moveForward = true;
            moveBack = false;
        }
        //Move Backward
        else if (OVRInput.GetUp(OVRInput.Button.One) && sittingScript.isInsideSpaceship && !moveBack)
        {
            moveBack = true;
            moveForward = false;
        }

        //Stop (while moving backward)
        else if (OVRInput.GetUp(OVRInput.Button.Two) && sittingScript.isInsideSpaceship && moveForward && !moveBack)
        {
            moveForward = false;
            moveBack = false;
        }
        //Stop (while moving forward)
        else if (OVRInput.GetUp(OVRInput.Button.One) && sittingScript.isInsideSpaceship && moveBack && !moveForward)
        {
            moveForward = false;
            moveBack = false;
        }


        yield return null;

    }

    private IEnumerator ExitSpaceship()
    {
        //While app button is being pressed, check if it has been held for holdTime seconds
        if (OVRInput.GetDown(OVRInput.Button.Four) && sittingScript.isInsideSpaceship && hitSafeZone) //add hitsafezone
        {        
                //unparent player from spaceship
                player.transform.parent = null;
                //move the player back to initial position
                player.transform.position = sittingScript.initialPlayerPosition;
                //enable character controller
                player.GetComponent<OVRPlayerController>().enabled = true;
                player.GetComponent<CharacterController>().enabled = true;

                //move spaceship back
                spaceship.transform.position = sittingScript.initialSpaceshipPosition;
                //rotate spaceship to original position
                to = new Vector3(0, 0, 0);
                spaceship.transform.eulerAngles = to; //resets the rotation of spaceship
                player.transform.eulerAngles = to; //resets the rotation of player

                //no longer in spaceship
                sittingScript.isInsideSpaceship = false;
                //can teleport again
                sittingScript.teleported = false;
                //play sound
                PlaySound(exitSound);
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

        //Exiting out of spaceship
        StartCoroutine(ExitSpaceship());
        hitSafeZone = spaceship.GetComponent<SpaceshipCollide>().hitSafeZone;

    }
}
