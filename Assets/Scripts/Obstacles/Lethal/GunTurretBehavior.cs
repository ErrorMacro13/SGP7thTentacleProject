using UnityEngine;
using System.Collections;

public class GunTurretBehavior : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;
    int SpeedCase = 0;

    GameObject Barrel;
    Vector3 BarrelLocPos;
    public GameObject Bullet;
    GameObject TempBullet;
    public float Percent;

    public float TimeBetweenShots;
    public float InitialDelay;
    float ID;

    public bool enabled;
    // Use this for initialization
    void ResetOverWorld()
    {
        InitialDelay = ID;
    }
    void Start()
    {
        ID = InitialDelay;
        Barrel = transform.Find("Barrel").gameObject;
        BarrelLocPos = Barrel.transform.localPosition;
        Barrel.transform.localPosition -= new Vector3(1.6f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (enabled)
        {
            Percent = InitialDelay / TimeBetweenShots;
            InitialDelay -= Time.deltaTime * CurrGameSpeed;
            if (InitialDelay <= 0.0f)
            {
                Fire();
                InitialDelay = TimeBetweenShots;
            }
            else if (Percent > 0.75f)
                Barrel.transform.localPosition = new Vector3(BarrelLocPos.x - 7f * (1 - Percent), BarrelLocPos.y, 0);
            else
                Barrel.transform.localPosition = new Vector3(BarrelLocPos.x - 2.5f * Percent, BarrelLocPos.y, 0);
        }
        else
            return;
    }

    void Fire()
    {
        TempBullet = Instantiate(Bullet, Barrel.transform.position, transform.rotation) as GameObject;
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

    void ToggleActive(bool isActive)
    {
        if (isActive)
            enabled = true;
        else
            enabled = false;
    }

    void SetTime(int GameSpeed)
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
