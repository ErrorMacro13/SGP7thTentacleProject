using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{

    GameObject Cam;
    public float newZoom;
    public float YOffset;
    public float XOffset;
    public bool PlayerLocked;
    public bool VertLocked;
    bool zooming = false;
    public Vector3 Offset;
    bool update = false;

    // Use this for initialization
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (zooming)
        {
            if (PlayerLocked)
            {
                Cam.SendMessage("RePosition", new Vector3(0, 0, -20));
            }
            else
            {

                if (newZoom > Cam.gameObject.GetComponent<Camera>().orthographicSize)
                {
                    Cam.gameObject.GetComponent<Camera>().orthographicSize += .1f;
                    if (Cam.gameObject.GetComponent<Camera>().orthographicSize > newZoom)
                        Cam.gameObject.GetComponent<Camera>().orthographicSize = newZoom;
                }
                else if (newZoom < Cam.gameObject.GetComponent<Camera>().orthographicSize)
                {
                    Cam.gameObject.GetComponent<Camera>().orthographicSize -= .1f;
                    if (Cam.gameObject.GetComponent<Camera>().orthographicSize < newZoom)
                        Cam.gameObject.GetComponent<Camera>().orthographicSize = newZoom;
                }

                if (Offset.x > XOffset)
                {
                    update = true;
                    Offset.x -= .04f;
                    if (Offset.x < XOffset)
                        Offset.x = XOffset;
                }
                else if (Offset.x < XOffset)
                {
                    update = true;
                    Offset.x += .04f;
                    if (Offset.x > XOffset)
                        Offset.x = XOffset;
                }

                if (Offset.y > YOffset)
                {
                    update = true;
                    Offset.y -= .04f;
                    if (Offset.y < YOffset)
                        Offset.y = YOffset;
                }
                else if (Offset.y < YOffset)
                {
                    update = true;
                    Offset.y += .04f;
                    if (Offset.y > YOffset)
                        Offset.y = YOffset;
                }
            }
            if (update)
            {
                update = false;
                Cam.SendMessage("RePosition", Offset);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Cam.SendMessage("VertLock", VertLocked);
            Offset.x = Cam.transform.position.x - GameObject.Find("Player").transform.position.x;
            Offset.z = -20;
            if (VertLocked)
                Offset.y = Cam.transform.position.y;// GameObject.Find("Player").transform.position.y;
            else
                Offset.y = Cam.transform.position.y - GameObject.Find("Player").transform.position.y;
            Cam.SendMessage("RePosition", Offset);
            zooming = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            zooming = false;
        }
    }
}
