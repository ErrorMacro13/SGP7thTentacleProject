using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{

    public GameObject Player;
    Vector3 camPosition = new Vector3(3.75f, 3, -20);

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x + camPosition.x, Player.transform.position.y + camPosition.y, camPosition.z);
    }

    void RePosition(Vector3 vec)
    {
        camPosition = vec;
    }
}
