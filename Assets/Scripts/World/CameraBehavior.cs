using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{

    public GameObject Player;
    Vector3 camPosition = new Vector3(3.75f, 3, -20);
    bool VertLocked = true;
    // Use this for initialization
    void Start()
    {
        VertLocked = true;
        RePosition(new Vector3(3.75f, Player.transform.position.y + 3, -20));
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        
    }

    void FollowPlayer()
    {
        if (!VertLocked)
            transform.position = new Vector3(Player.transform.position.x + camPosition.x, Player.transform.position.y + camPosition.y, camPosition.z);
        else
            transform.position = new Vector3(Player.transform.position.x + camPosition.x, camPosition.y, camPosition.z);
    }

    void RePosition(Vector3 vec)
    {
        camPosition = vec;
    }

    void VertLock(bool var)
    {
        VertLocked = var;
    }

    void ResetOverWorld()
    {
        VertLocked = true;
        RePosition(new Vector3(3.75f, Player.transform.position.y + 3, -20));
    }
}
