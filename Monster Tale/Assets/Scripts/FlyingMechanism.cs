using UnityEngine;
using System.Collections;

public class FlyingMechanism : MonoBehaviour {
    public Transform spaceship;
    public float speed = 0.25f;
    public float reverseSpeed = 0.25f;
    public float turnSpeed;
    public GameObject player; // where script for sitInSpaceship is located
    public float holdTime = 1.0f;
    public GameObject safeZone;
    public AutoWalk autoWalkScript;
    public spaceship_open spaceShipScript;
    public GameObject spaceshipScriptPart;
    public GameObject controller;
    public GameObject spaceshipMB;
    public bool hitSafeZone;
    public GameObject target;
    public float rotationSpeed; //gyro turn
    public GameObject exitSound;

    private bool moveForward;
    private bool moveBack;
    private bool hasBeenDelayed;
    private float defSpeed = 0.50f;
    private float defTurnSpeed = 30f;
    private float counter;
    private sitInSpaceship sittingScript; // function if is sitting
	private CharacterController cc;
    private CharacterController pcc;

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

    public void DeactivateTarget()
    {
        target.GetComponent<spaceship_open>().enabled = false;
    }
    public void ActivateTarget()
    {
        target.GetComponent<spaceship_open>().enabled = true;
    }
    // Use this for initialization
    void Start()
    {
        sittingScript = player.GetComponent<sitInSpaceship>();
        cc = GetComponent<CharacterController>();
        //speed = defSpeed;
        turnSpeed = defTurnSpeed;
        rotationSpeed = defTurnSpeed; 
        moveForward = false;
        moveBack = false;
        pcc = player.GetComponent<CharacterController>();
        hasBeenDelayed = false;
        autoWalkScript = player.GetComponent<AutoWalk>();
        spaceShipScript = spaceshipScriptPart.GetComponent<spaceship_open>();
        GetComponent<CharacterController>().enabled = false;//disables character controller on start (roatational problems) 
        Input.gyro.enabled = true; //enabling gyro to rotate spaceship with cardboard
    }

    // Update is called once per frame
    void Update () {
        //if (sittingScript.isInsideSpaceship)
      //  {
            //Gyro();
       // }
        //Moving forward
        if (moveForward)
        {
            speed = defSpeed;
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            cc.Move(forward * speed);
		}
        //Moving backward
        if (moveBack)
        {
            speed = reverseSpeed;
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            cc.Move(-forward * speed);
        }
        //Forward
        if((GvrController.ClickButtonDown || Input.GetButtonDown("Fire1")) && sittingScript.isInsideSpaceship && !moveForward &&hasBeenDelayed) 
        {
            moveForward = true;
            moveBack = false;
        }
        else if ((GvrController.ClickButtonDown || Input.GetButtonDown("Fire1")) && sittingScript.isInsideSpaceship && moveForward &&hasBeenDelayed)
        {
            moveForward = false;
        }

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

                //enable player walking
                autoWalkScript.canMove = true;
                autoWalkScript.moveForward = false;

                //play sound
                PlaySound(exitSound);

                //disables character control
                GetComponent<CharacterController>().enabled = false;
                //Activating spaceship open script
                ActivateTarget();
                spaceShipScript.spaceshipClick = false;
                hasBeenDelayed = false;
                controller.SetActive(true); //shows controller;
                spaceshipMB.GetComponent<CapsuleCollider>().enabled = true;//enables collider on spaceship
            }
        }
        //Reverse
        if (GvrController.AppButtonDown && sittingScript.isInsideSpaceship && !moveBack && hasBeenDelayed)
        {
            moveForward = false;
            moveBack = true;

        }
        else if (GvrController.AppButtonDown && sittingScript.isInsideSpaceship && moveBack && hasBeenDelayed)
        {
            counter += Time.deltaTime;
            moveBack = false;

        }
        if (GvrController.AppButtonUp)
        {
            counter = 0;
        }
        if (GvrController.IsTouching && sittingScript.isInsideSpaceship && hasBeenDelayed)
        {
            //disables the spaceship open script
            DeactivateTarget();
            Vector2 touchPos = 2 * GvrController.TouchPos - Vector2.one;

            if(touchPos.x >= touchPos.y && touchPos.y > -touchPos.x)
            {
                //Debug.Log("Right");
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            }
            else if (touchPos.x <= touchPos.y && touchPos.y < -touchPos.x)
            {
                //Debug.Log("Left");
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }

            else if (touchPos.y >= -touchPos.x && touchPos.y > touchPos.x)
            {
                //Debug.Log("Bottom");
                transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);
            }

            else if (touchPos.y <= -touchPos.x && touchPos.y < touchPos.x)
            {
                //Debug.Log("Top");
                transform.Rotate(Vector3.right, -turnSpeed * Time.deltaTime);
            }

        }
        if (sittingScript.isInsideSpaceship && !hasBeenDelayed)
        {
            hasBeenDelayed = true;
        }
    }
}

/*
 *     private void Gyro()
    {
        Quaternion rotation = Quaternion.Euler(180, 0, 0);
        transform.rotation = Input.gyro.attitude * rotation;
        //transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
    }
*/