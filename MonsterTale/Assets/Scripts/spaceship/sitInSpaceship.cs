using UnityEngine;
using System.Collections;

public class sitInSpaceship : MonoBehaviour {
    public GameObject player;
	public bool isInsideSpaceship;
    public GameObject spaceship;
    public GameObject spaceshipOpenSound;
    public GameObject button;
    private ButtonInteract buttonScript;

    public void TeleportToSpaceship(){
        float xpositionSpaceship = spaceship.transform.position.x;
        float ypositionPlayerSpaceship = spaceship.transform.position.y;
        float zpositionPlayerSpaceship = spaceship.transform.position.z;
        Vector3 to = new Vector3(0, 0, 0);
        player.transform.localPosition = new Vector3(xpositionSpaceship, ypositionPlayerSpaceship, zpositionPlayerSpaceship); //teleports player to spaceship location
		isInsideSpaceship = true;
        player.transform.SetParent(spaceship.transform); //attaches player to spaceship
        player.transform.eulerAngles = to; //rotates the player to face forward
        //ADDED THIS NEED TO RESET CHARACTER POS
        player.GetComponent<CharacterController>().enabled = false;
        //activate character controller for spaceship
        spaceship.GetComponent<CharacterController>().enabled = true;
        //disable spaceship open sound
        //DisableSound(spaceshipOpenSound);
    }

    public void DisableSound(GameObject temp)
    {
        temp.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		isInsideSpaceship = false;
        buttonScript = button.GetComponent<ButtonInteract>();
	}
	
	// Update is called once per frame
	void Update () {
        if (buttonScript.on)
            TeleportToSpaceship();
            buttonScript.on = false;
    	}

}

