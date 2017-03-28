using UnityEngine;
using System.Collections;

public class play_sound : MonoBehaviour {
    public GameObject sound;


    public void PlaySound()
    {
        sound.GetComponent<AudioSource>().Play();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
