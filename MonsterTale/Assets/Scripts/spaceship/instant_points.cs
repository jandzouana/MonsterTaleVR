using UnityEngine;
using UnityEngine.UI; //needed to use Text
using System.Collections;

public class instant_points : MonoBehaviour {
    private sitInSpaceship sittingScript; // function if is sitting
    private bool finishedTutorial;
    public GameObject sittingScriptObject;
    public GameObject spaceship;
    public bool interacted; //can only play game once
    private GameObject point;
    public GameObject origPoint;
    public GameObject pointContainer;
    public GameObject player;
    public GameObject pingAudio;
    public GameObject landing;
    public GameObject panel;
    public GameObject textPanel; //instructions to spaceship
    public GameObject outsidePanel;
    public string initialText;
    public string text2, text3, text4, text5;
    public GameObject newSafeZone;

    private Vector3 pos;
    private string tempString;
    private Text txtRef;
    public int count;

    // Use this for initialization
    void Start()
    {
        sittingScript = sittingScriptObject.GetComponent<sitInSpaceship>();
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
    public void HitPoints()
    {
        if (spaceship.GetComponent<SpaceshipCollide>().hit == true)
        {
            Destroy(point.gameObject);
            PlaySound(pingAudio);
            //creates points(one at a time) at these locations
            //text changes depending on which point is captured
            switch (count)
            {
                case 0:
                    pos = new Vector3(40, 5, 85);
                    tempString = text2;
                    break;
                case 1:
                    tempString = text3;
                    pos = new Vector3(0, 30, 0);
                    break;
                case 2:
                    tempString = text4;
                    pos = new Vector3(206, 28, 4);
                    break;
                case 3:
                    tempString = text5;
                    //activating landing spot
                    landing.SetActive(true);
                    break;
                default:
                    break;
            }
            ChangeText(txtRef, tempString);

            if (count != 3)
            {
                spaceship.GetComponent<SpaceshipCollide>().hit = false;
                InstantiatePoint(pos);
                count++;
            }
        }
    }

    //creates a point at a set location
    void InstantiatePoint(Vector3 position)
    {
        point = Instantiate(origPoint); //holds clone of capture point in point gameobject
        point.transform.SetParent(pointContainer.transform); //puts point in point container
        //moves point to first location
        point.transform.localPosition = position;
    }
    // Update is called once per frame
    void Update()
    {
        if (sittingScript.isInsideSpaceship && !interacted && !finishedTutorial)
        {
            panel.SetActive(true);
			outsidePanel.SetActive(false); //hides panel on outside of spaceship by gate
            //initial starting point
            pos = new Vector3(67.4f, 3, 29.3f);
            InstantiatePoint(pos);
            interacted = true;
        }
        if (interacted && !finishedTutorial) HitPoints();
        //count should compare to the last point.
        if (!sittingScript.isInsideSpaceship && interacted && !finishedTutorial)
        {
            panel.SetActive(false); //disable text panel after all the points have been captured
            landing.SetActive(false); //disables beginner landing marker
            newSafeZone.SetActive(true); //enables bigger landing space
            finishedTutorial = true;

        }
    }
}
