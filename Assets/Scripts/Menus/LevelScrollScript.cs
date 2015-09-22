using UnityEngine;
using System.Collections;

public class LevelScrollScript : MonoBehaviour
{

    public Transform[] waypoints;
    public int CurrPoint = 0;
    GameObject Cam;

    // Use this for initialization
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints[CurrPoint].transform.position.x != Cam.transform.position.x || waypoints[CurrPoint].transform.position.y != Cam.transform.position.y)
        {
            print("Off");
            if (waypoints[CurrPoint].transform.position.x < Cam.transform.position.x)
            {
                print("Left");

                transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
                if (waypoints[CurrPoint].transform.position.x > Cam.transform.position.x)
                    transform.position -= new Vector3(waypoints[CurrPoint].transform.position.x, 0, 0);
            }
            else if (waypoints[CurrPoint].transform.position.x > Cam.transform.position.x)
            {
                print("Right");

                transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);
                if (waypoints[CurrPoint].transform.position.x < Cam.transform.position.x)
                    transform.position += new Vector3(waypoints[CurrPoint].transform.position.x, 0, 0);
            }
            if (waypoints[CurrPoint].transform.position.y < Cam.transform.position.y)
            {
                print("Up");

                transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
                if (waypoints[CurrPoint].transform.position.y > Cam.transform.position.y)
                    transform.position -= new Vector3(0, waypoints[CurrPoint].transform.position.y, 0);
            }
            else if (waypoints[CurrPoint].transform.position.y > Cam.transform.position.y)
            {
                print("Down");

                transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);
                if (waypoints[CurrPoint].transform.position.y < Cam.transform.position.y)
                    transform.position += new Vector3(0, waypoints[CurrPoint].transform.position.y, 0);
            }
        }
        else
        {
            print("Count UP");
            CurrPoint++;
            if (CurrPoint >= waypoints.Length)
                CurrPoint = 0;
        }
    }
}
