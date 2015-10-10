using UnityEngine;
using System.Collections;

public class PushScript : MonoBehaviour
{

    private float CurrGameSpeed = 1.0f;
    public float Speed = 1f; // Hidden in waypoints Scale X
    public float RotSpeed = 0f; // hidden in waypoints Scale Y
    public float newRot;  // Hidden in waypoints Scale Z
    public float currRot;
    public float spin = 1;

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
        if (name == "LevelPush2")
        transform.Rotate(Vector3.back * Time.deltaTime * CurrGameSpeed * spin);

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
            print("laserupdate");
            Speed = Waypoints[currWaypoint].localScale.x;
            RotSpeed = Waypoints[currWaypoint].localScale.y;
            newRot = Waypoints[currWaypoint].localScale.z;
            currWaypoint++;
            XPass = false;
            YPass = false;
            if (currWaypoint >= Waypoints.Length)
                currWaypoint = 0;
        }

        if (currRot > newRot)
        {
            currRot -= Time.deltaTime * CurrGameSpeed * RotSpeed;
            transform.rotation = Quaternion.AngleAxis(270 + currRot, Vector3.forward);
            if (currRot < newRot)
            {
                currRot = newRot;
                transform.rotation = Quaternion.AngleAxis(270 + currRot, Vector3.forward);
            }
        }
        else if (currRot < newRot)
        {
            currRot += Time.deltaTime * CurrGameSpeed * RotSpeed;
            transform.rotation = Quaternion.AngleAxis(270 + currRot, Vector3.forward);
            if (currRot > newRot)
            {
                currRot = newRot;
                transform.rotation = Quaternion.AngleAxis(270 + currRot, Vector3.forward);
            }
        }
    }

    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            default:
                CurrGameSpeed = 1.0f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
        }
    }
}
