using UnityEngine;
using System.Collections;

public class sitInSpaceship : MonoBehaviour {
    public GameObject player;
    public GameObject SoundOpen;
	public bool isInsideSpaceship;
    public GameObject spaceship;
    public GameObject button;
    private ButtonInteract buttonScript;

    public void TeleportToSpaceship(){
        float xpositionSpaceship = spaceship.transform.position.x;
        float ypositionPlayerSpaceship = spaceship.transform.position.y;
        float zpositionPlayerSpaceship = spaceship.transform.position.z;
        Vector3 to = new Vector3(0, 0, 0);
        //player.transform.localPosition = new Vector3(xpositionSpaceship, ypositionPlayerSpaceship, zpositionPlayerSpaceship); //teleports player to spaceship location
		isInsideSpaceship = true;
        player.transform.SetParent(spaceship.transform); //attaches player to spaceship
        player.transform.localPosition = Vector3.zero;
        player.transform.eulerAngles = to; //rotates the player to face forward

    }
    //other things that need to be done as player teleports to spaceship
    private void Utilities()
    {
        //disables player character controller to stop spaceship from rotating
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<OVRPlayerController>().enabled = false;

        //activate character controller for spaceship
        spaceship.GetComponent<CharacterController>().enabled = true;
        //disable spaceship open sound
        //DisableSound(spaceshipOpenSound);
    }
    private void PlaySound(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }

    public void DisableSound(GameObject soundObject)
    {
        soundObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		isInsideSpaceship = false;
        buttonScript = button.GetComponent<ButtonInteract>();
	}

    // Update is called once per frame
    void Update()
    {
        
        if (buttonScript.on)
        {
            TeleportToSpaceship();
            Utilities(); //other things that need to be done as player teleports to spaceship
            PlaySound(SoundOpen);
            buttonScript.on = false;
        }
        if (OVRInput.Get(OVRInput.Button.One))
        {
            //Debug.Log("Pressed");
        }


    }

}

