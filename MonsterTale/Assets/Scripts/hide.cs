using UnityEngine;
using System.Collections;

public class hide : MonoBehaviour {
    public GameObject target;
	// Use this for initialization

    public void Hide()
    {
        target.SetActive(false);
    }
    public void Show()
    {
        target.SetActive(true);
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
