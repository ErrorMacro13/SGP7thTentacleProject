﻿using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D ent)
    {
        if (ent.tag == "Player")
        {
            SendMessageUpwards("Activate");
        }
        if (ent.tag == "Acid")
        {
            SendMessageUpwards("ResetOverWorld");
        }
    }
}
