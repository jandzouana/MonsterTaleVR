using UnityEngine;
using System.Collections;

public class show_instructions : MonoBehaviour {
    private sitInSpaceship sittingScript; // function if is sitting
    public bool interacted;
    public GameObject panel;
    public GameObject player;
    // Use this for initialization
    void Start () {
        sittingScript = player.GetComponent<sitInSpaceship>();
        interacted = false;
    }
    void ShowPanel()
    {
        panel.SetActive(true);
        interacted = true;
    }
    // Update is called once per frame
    void Update () {
        if (sittingScript.isInsideSpaceship && !interacted)
        {
            ShowPanel();
        }
    }
}
