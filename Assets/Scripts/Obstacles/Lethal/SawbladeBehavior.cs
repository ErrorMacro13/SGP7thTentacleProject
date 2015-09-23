﻿using UnityEngine;
using System.Collections;

public class SawbladeBehavior : MonoBehaviour
{

    private float CurrGameSpeed = 1.0f;
    public float Speed = 1;
    public Transform[] Waypoints;
    int currWaypoint;
    bool XPass = false;
    bool YPass = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > Waypoints[currWaypoint].position.x && !XPass)
        {
            transform.position -= new Vector3(Speed * CurrGameSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x <= Waypoints[currWaypoint].position.x)
                XPass = true;
        }
        else if (transform.position.x < Waypoints[currWaypoint].position.x && !XPass)
        {
            transform.position += new Vector3(Speed * CurrGameSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x >= Waypoints[currWaypoint].position.x)
                XPass = true;
        }
        else
            XPass = true;

        if (transform.position.y > Waypoints[currWaypoint].position.y && !YPass)
        {
            transform.position -= new Vector3(0, Speed * CurrGameSpeed * Time.deltaTime, 0);
            if (transform.position.y <= Waypoints[currWaypoint].position.y)
                YPass = true;
        }
        else if (transform.position.y < Waypoints[currWaypoint].position.y && !YPass)
        {
            transform.position += new Vector3(0, Speed * CurrGameSpeed * Time.deltaTime, 0);
            if (transform.position.y >= Waypoints[currWaypoint].position.y)
                YPass = true;
        }
        else
            YPass = true;

        if (YPass && XPass)
        {
            currWaypoint++;
            XPass = false;
            YPass = false;
            if (currWaypoint >= Waypoints.Length)
                currWaypoint = 0;
        }
    }

    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                break;
            default:
                CurrGameSpeed = 1.0f;
                break;
        }
    }
}
