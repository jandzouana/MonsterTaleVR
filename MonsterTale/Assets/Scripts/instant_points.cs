using UnityEngine;
using UnityEngine.UI; //needed to use Text
using System.Collections;

public class instant_points : MonoBehaviour {
    private sitInSpaceship sittingScript; // function if is sitting
    public bool interacted; //can only play game once
    private GameObject point;
    public GameObject origPoint;
    public GameObject pointContainer;
    public GameObject player;
    public GameObject pingAudio;
    public GameObject landing;
    public GameObject panel;
    public GameObject textPanel; //instructions to spaceship
    public string initialText;
    public string text2, text3, text4;
    public GameObject newSafeZone;

    private Vector3 pos;
    private string tempString;
    private Text txtRef;
    private int count;

    // Use this for initialization
    void Start()
    {
        sittingScript = player.GetComponent<sitInSpaceship>();
        interacted = false;
        txtRef = textPanel.GetComponent<Text>();
        count = 0;
        tempString = initialText;
        txtRef.text = initialText;
    }
    public void PlaySound(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }
    public void ChangeText(Text panel, string text)
    {
        panel.text = text;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Point"))
        {
            Destroy(point.gameObject);
            PlaySound(pingAudio);
            //creates points(one at a time) at these locations
            //text changes depending on which point is captured
            switch (count)
            {
                case 0:
                    pos = new Vector3(40, 5, 85);
                    break;
                case 1:
                    pos = new Vector3(0, 30, 0);
                    tempString = text2;
                    break;
                case 2:
                    pos = new Vector3(206, 28, 4);
                    tempString = text3;
                    break;
                case 3:
                    //activating landing spot
                    landing.SetActive(true);
                    tempString = text4;
                    break;
                default:
                    break;
            }
            if(count!=3)InstantiatePoint(pos);
            ChangeText(txtRef, tempString);
            count++;
        }
    }

    //creates a point at a set location
    void InstantiatePoint(Vector3 position)
    {
        interacted = true;
        point = Instantiate(origPoint); //holds clone of capture point in point gameobject
        point.transform.SetParent(pointContainer.transform); //puts point in point container
        //moves point to first location
        point.transform.localPosition = position;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("interacted:"+interacted);
        if (sittingScript.isInsideSpaceship && !interacted)
        {
            //initial starting point
            pos = new Vector3(67.4f, 3, 29.3f);
            InstantiatePoint(pos);
        }
        //count should compare to the last point.
        if (!sittingScript.isInsideSpaceship && interacted)
        {
            Debug.Log("Yes");
            panel.SetActive(false); //disable text panel after all the points have been captured
            landing.SetActive(false); //disables beginner landing marker
            newSafeZone.SetActive(true); //enables bigger landing space

        }
    }
}
