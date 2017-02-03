using UnityEngine;
using System.Collections;

public class FlyingMechanism : MonoBehaviour {
    public Transform spaceship;
    public float speed;
    public float turnSpeed;
    public bool moveForward;
    public bool moveBack;
    public GameObject player; // where script for sitInSpaceship is located
    public bool hasBeenDelayed;
    public float holdTime = 2.0f;
    public GameObject safeZone;

    private float defSpeed = 0.50f;
    private float defTurnSpeed = 30f;
    private float counter;

    private sitInSpaceship sittingScript; // function if is sitting
	private CharacterController cc;
    private CharacterController pcc;
    private bool isCoroutineExecuting = false;
    private float time = 3f;
    public GameObject target;

    void OnCollisionEnter(Collider col)
    {
        if (col.CompareTag("safe"))
        {

        }
    }

    public void DeactivateTarget()
    {
        target.GetComponent<spaceship_open>().enabled = false;
    }

    // Use this for initialization
    void Start()
    {
        sittingScript = player.GetComponent<sitInSpaceship>();
        cc = GetComponent<CharacterController>();
        speed = defSpeed;
        turnSpeed = defTurnSpeed;
        moveForward = false;
        moveBack = false;
        pcc = player.GetComponent<CharacterController>();
        hasBeenDelayed = false;
    }

    // Update is called once per frame
    void Update () {
        Debug.Log("Counter at start:" + counter);
        //Moving forward
        if (moveForward)
        {
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            cc.Move(forward * speed);
		}
        //Moving backward
        if (moveBack)
        {
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            cc.Move(-forward * speed);
        }
        //Forward
        if(GvrController.ClickButtonDown && sittingScript.isInsideSpaceship && !moveForward &&hasBeenDelayed) 
        {
            moveForward = true;
            moveBack = false;
        }
        else if (GvrController.ClickButtonDown && sittingScript.isInsideSpaceship && moveForward &&hasBeenDelayed)
        {
            moveForward = false;
        }
        //While app button is being pressed, check if 
        if (GvrController.AppButton && sittingScript.isInsideSpaceship && hasBeenDelayed)
        {
            Debug.Log(counter);
            counter += Time.deltaTime;
            if (counter > holdTime)
            {
                Debug.Log("Time passed");
                moveBack = false;
                moveForward = false;
                //unparent player from spaceship
                player.transform.parent = null;
                //move the player
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
        if (GvrController.IsTouching && sittingScript.isInsideSpaceship)
        {
            DeactivateTarget();
            Vector2 touchPos = 2 * GvrController.TouchPos - Vector2.one;

            if(touchPos.x >= touchPos.y && touchPos.y > -touchPos.x)
            {
                Debug.Log("Right");
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            }
            else if (touchPos.x <= touchPos.y && touchPos.y < -touchPos.x)
            {
                Debug.Log("Left");
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }

            else if (touchPos.y >= -touchPos.x && touchPos.y > touchPos.x)
            {
                Debug.Log("Bottom");
                transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);
            }

            else if (touchPos.y <= -touchPos.x && touchPos.y < touchPos.x)
            {
                Debug.Log("Top");
                transform.Rotate(Vector3.right, -turnSpeed * Time.deltaTime);
            }

        }
        if (sittingScript.isInsideSpaceship && !hasBeenDelayed)
        {
            hasBeenDelayed = true;
        }
    }
}
