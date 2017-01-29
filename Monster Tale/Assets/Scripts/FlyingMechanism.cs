using UnityEngine;
using System.Collections;

public class FlyingMechanism : MonoBehaviour {
  public Transform spaceship;
  public float speed;
  public bool moveForward;
	public GameObject player; // where script for sitInSpaceship is located

	private float defspeed = 3.0f;
	private sitInSpaceship sittingScript; // function if is sitting
	private CharacterController cc;

    // Use this for initialization
    void Start()
    {
        sittingScript = player.GetComponent<sitInSpaceship>();
        cc = GetComponent<CharacterController>();
        speed = defspeed;
        moveForward = false;

    }

	// Update is called once per frame
	void Update () {
		if (GvrController.AppButtonDown && sittingScript.isInsideSpaceship && !moveForward)
    {
            Vector3 forward = spaceship.TransformDirection(Vector3.forward); // getting forward direction
			cc.SimpleMove(forward * speed);
			Debug.Log ("should move");
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
  }
}
