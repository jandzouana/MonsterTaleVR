using UnityEngine;
using System.Collections;

public class instant_points : MonoBehaviour {
    private sitInSpaceship sittingScript; // function if is sitting
    public bool interacted;
    private GameObject point;
    public GameObject origPoint;
    public GameObject pointContainer;
    public GameObject player;
    private int count;
    public GameObject pingAudio;
    public GameObject landing;
    Vector3 pos;
    // Use this for initialization
    void Start()
    {
        sittingScript = player.GetComponent<sitInSpaceship>();
        interacted = false;
        count = 0;
    }
    public void PlaySound(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Point"))
        {
            Destroy(point.gameObject);
            PlaySound(pingAudio);
            //creates points(one at a time) at these locations
            switch (count)
            {
                case 0:
                    pos = new Vector3(40, 5, 85);
                    break;
                case 1:
                    pos = new Vector3(0, 30, 0);
                    break;
                case 2:
                    pos = new Vector3(0, 30, 120);
                    break;
                case 3:
                    pos = new Vector3(206, 28, 4);
                    break;
                case 4:
                    //activating landing spot
                    landing.SetActive(true);
                    break;
                default:
                    break;
            }
            if(count!=4)InstantiatePoint(pos);
            Debug.Log("collided");
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
        if (sittingScript.isInsideSpaceship && !interacted)
        {
            pos = new Vector3(67.4f, 0, 29.3f);
            InstantiatePoint(pos);
        }
    }
}
