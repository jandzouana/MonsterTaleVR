using UnityEngine;
using System.Collections;

public class FlyingMechanism : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
