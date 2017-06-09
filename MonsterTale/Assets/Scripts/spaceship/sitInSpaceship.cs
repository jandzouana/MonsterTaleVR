using UnityEngine;
using System.Collections;

public class sitInSpaceship : MonoBehaviour {
    public bool isInsideSpaceship;
    public bool teleported;
    public GameObject player;
    public GameObject spaceship;
    public GameObject button;
    public GameObject SoundOpen;
    public Vector3 initialPlayerPosition;
    public Vector3 initialSpaceshipPosition;
	public GameObject CenterAnchorCamera;
	public GameObject spaceshipReflectionProbe;

    private ButtonInteract buttonScript;
	private Fade scriptFade;

    public void TeleportToSpaceship(){
        float xpositionSpaceship = spaceship.transform.position.x;
        float ypositionPlayerSpaceship = spaceship.transform.position.y;
        float zpositionPlayerSpaceship = spaceship.transform.position.z;
        Vector3 to = new Vector3(0, 0, 0);
		isInsideSpaceship = true;
        player.transform.SetParent(spaceship.transform); //attaches player to spaceship
        player.transform.localPosition = Vector3.zero;
        player.transform.eulerAngles = to; //rotates the player to face forward

    }
    //initial position of object
    Vector3 GetInitialPosition(GameObject target)
    {
        float xposition = target.transform.position.x;
        float yposition = target.transform.position.y;
        float zposition = target.transform.position.z;
        return new Vector3(xposition, yposition, zposition);
    }
    //other things that need to be done as player teleports to spaceship
    private void Utilities()
    {
        //disables player character controller to stop spaceship from rotating
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<OVRPlayerController>().enabled = false;
        //activate character controller for spaceship
        spaceship.GetComponent<CharacterController>().enabled = true;
		//disables reflection probe;
		spaceshipReflectionProbe.SetActive(false); //hides panel on outside of spaceship by gate

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
		isInsideSpaceship = teleported =  false;
        buttonScript = button.GetComponent<ButtonInteract>();
		scriptFade = CenterAnchorCamera.GetComponent<Fade>();
	}

    // Update is called once per frame
    void Update()
    {
        if (buttonScript.on) isInsideSpaceship = true;
        if (isInsideSpaceship && !teleported)
        {
			//flashes white when inside spaceship
			StartCoroutine(scriptFade.FadeIn());
            initialPlayerPosition = GetInitialPosition(player);
            initialSpaceshipPosition = GetInitialPosition(spaceship);
            teleported = true;
            TeleportToSpaceship();
            Utilities(); //other things that need to be done as player teleports to spaceship
            PlaySound(SoundOpen);
            buttonScript.on = false;
        }

    }

}

