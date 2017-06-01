using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_vicinity : MonoBehaviour {
    public bool entered;
    // Use this for initialization
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            entered = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        entered = false;
    }
    void Start()
    {

        entered = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
