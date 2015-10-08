using UnityEngine;
using System.Collections;

public class LevelScrollScript : MonoBehaviour
{

    public Transform[] waypoints;
    public int CurrPoint = 0;
    GameObject Cam;
    public static LevelScrollScript Instance { get; private set; }
    public bool locked = false;
    public bool once = false;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        Cam = GameObject.Find("Main Camera");
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            if (waypoints[CurrPoint].transform.position.x != Cam.transform.position.x || waypoints[CurrPoint].transform.position.y != Cam.transform.position.y)
            {
                if (waypoints[CurrPoint].transform.position.x < Cam.transform.position.x)
                {
                    transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
                    if (waypoints[CurrPoint].transform.position.x > Cam.transform.position.x)
                        transform.position -= new Vector3(waypoints[CurrPoint].transform.position.x, 0, 0);
                }
                else if (waypoints[CurrPoint].transform.position.x > Cam.transform.position.x)
                {
                    transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);
                    if (waypoints[CurrPoint].transform.position.x < Cam.transform.position.x)
                        transform.position += new Vector3(waypoints[CurrPoint].transform.position.x, 0, 0);
                }
                if (waypoints[CurrPoint].transform.position.y < Cam.transform.position.y)
                {
                    transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
                    if (waypoints[CurrPoint].transform.position.y > Cam.transform.position.y)
                        transform.position -= new Vector3(0, waypoints[CurrPoint].transform.position.y, 0);
                }
                else if (waypoints[CurrPoint].transform.position.y > Cam.transform.position.y)
                {
                    transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);
                    if (waypoints[CurrPoint].transform.position.y < Cam.transform.position.y)
                        transform.position += new Vector3(0, waypoints[CurrPoint].transform.position.y, 0);
                }
            }
            else
            {
                CurrPoint++;
                if (CurrPoint >= waypoints.Length)
                    CurrPoint = 0;
            }
        }
        else if (!once)
        {
            transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y, 20);
            if (waypoints[CurrPoint].transform.position.x > Cam.transform.position.x)
                transform.position += new Vector3(waypoints[CurrPoint].transform.position.x, 0, 0);
            else if (waypoints[CurrPoint].transform.position.x < Cam.transform.position.x)
                transform.position -= new Vector3(waypoints[CurrPoint].transform.position.x, 0, 0);
            if (waypoints[CurrPoint].transform.position.y > Cam.transform.position.y)
                transform.position += new Vector3(0, waypoints[CurrPoint].transform.position.y, 0);
            else if (waypoints[CurrPoint].transform.position.y < Cam.transform.position.y)
                transform.position -= new Vector3(0, waypoints[CurrPoint].transform.position.y, 0);
            once = true;
        }
    }

    void FixedUpdate()
    {
        Cam = GameObject.Find("Main Camera");
    }

    void LevelHover(int lvlNum)
    {
        locked = true;
        CurrPoint = lvlNum;
        once = false;
    }

    void Unlock()
    {
        locked = false;
    }
}
