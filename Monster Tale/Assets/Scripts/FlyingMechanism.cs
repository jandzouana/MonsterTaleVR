using UnityEngine;
using System.Collections;

public class FlyingMechanism : MonoBehaviour {
    public Transform spaceship;
    public float speed;
    public float turnSpeed = 20f;
    public bool moveForward;
    public GameObject player; // where script for sitInSpaceship is located
    public bool hasBeenDelayed;

    private float defspeed = 0.50f;
	private sitInSpaceship sittingScript; // function if is sitting
	private CharacterController cc;
    private CharacterController pcc;
    private bool isCoroutineExecuting = false;
    private float time = 3f;

    // Use this for initialization
    void Start()
    {
        sittingScript = player.GetComponent<sitInSpaceship>();
        cc = GetComponent<CharacterController>();
        speed = defspeed;
        moveForward = false;
        pcc = player.GetComponent<CharacterController>();
        hasBeenDelayed = false;
    }

    // Update is called once per frame
    void Update () {
        if (moveForward)
        {
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
            cc.Move(forward * speed);
            pcc.Move(forward * speed);
		}
        if(GvrController.ClickButtonDown && sittingScript.isInsideSpaceship && !moveForward &&hasBeenDelayed) 
        {
            moveForward = true;
        }
        else if (GvrController.ClickButtonDown && sittingScript.isInsideSpaceship && moveForward &&hasBeenDelayed)
        {
            moveForward = false;
        }
        if (GvrController.IsTouching)
        {
            Vector2 touchPos = 2 * GvrController.TouchPos - Vector2.one;
             //Debug.Log(touchPos);

            if(touchPos.x >= touchPos.y && touchPos.y > -touchPos.x)
            {
                Debug.Log("Right");
            }
            else if (touchPos.x <= touchPos.y && touchPos.y < -touchPos.x)
            {
                Debug.Log("Left");
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                player.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }

            else if (touchPos.y >= -touchPos.x && touchPos.y > touchPos.x)
            {
                Debug.Log("Bottom");
            }

            else if (touchPos.y <= -touchPos.x && touchPos.y < touchPos.x)
            {
                Debug.Log("Top");
            }

        }
        if (sittingScript.isInsideSpaceship && !hasBeenDelayed)
        {
            hasBeenDelayed = true;
        }
    }
}
