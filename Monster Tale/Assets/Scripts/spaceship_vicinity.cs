using UnityEngine;
using System.Collections;

public class spaceship_vicinity : MonoBehaviour {
	public bool entered;
    public GameObject sound;


    // Use this for initialization
    void Start () {
		entered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Plays sound when in the vicinity of the spaceship
    public void PlaySound()
    {
        sound.GetComponent<AudioSource>().Play();
    }

    // Function to see if player is in the collider spaceship area
    private void OnTriggerEnter (Collider col) {
		if (col.CompareTag("Player")) {
			entered = true;
            PlaySound();
		}	
	}

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            entered = true;
        }
    }

    // Function to see if player is in the collider spaceship area and leaves
    private void OnTriggerExit (Collider col) {
			entered = false;
	}
}
