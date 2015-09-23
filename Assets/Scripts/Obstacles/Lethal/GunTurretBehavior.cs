using UnityEngine;
using System.Collections;

public class GunTurretBehavior : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;
    short SpeedCase = 0;

    public GameObject Bullet;
    GameObject TempBullet;

    public float TimeBetweenShots;
    public float InitialDelay;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        InitialDelay -= Time.deltaTime * CurrGameSpeed;
        if (InitialDelay <= 0.0f)
        {
            Fire();
            InitialDelay = TimeBetweenShots;
        }
    }

    void Fire()
    {
        TempBullet = Instantiate(Bullet, transform.Find("Barrel").transform.position, transform.rotation) as GameObject;
        TempBullet.transform.parent = transform;
        switch (SpeedCase)
        {
            case 0:
                {
                    BroadcastMessage("SetTime", 4);
                    break;
                }
            case 1:
                {
                    BroadcastMessage("SetTime", 1);
                    break;
                }
            case 2:
                {
                    BroadcastMessage("SetTime", 2);
                    break;
                }
            case 3:
                {
                    BroadcastMessage("SetTime", 3);
                    break;
                }
        }
    }
    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                SpeedCase = 1;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                SpeedCase = 2;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                SpeedCase = 3;
                break;
            default:
                CurrGameSpeed = 1.0f;
                SpeedCase = 0;
                break;
        }
    }
}
