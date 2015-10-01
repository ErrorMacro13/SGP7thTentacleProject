using UnityEngine;
using System.Collections;

public class LaserTurretBehavior : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;
    public float InitialDelay = 1.0f;
    public float ChargeUpTime = 1.0f;
    public float FireTime = 1.0f;
    public float BeamWidth = 1.0f;
    public bool On = false;
    GameObject PEStart;
    GameObject PEEnd;

    float ID;
    bool IS;

    public GameObject Beam;
    public GameObject LaserCatcher;

    // Use this for initialization
    void ResetOverWorld()
    {
        On = IS;
        InitialDelay = ID;
    }

    void Start()
    {
        IS = On;
        ID = InitialDelay;
        Beam.transform.localPosition = LaserCatcher.transform.localPosition / 2;
        if (On)
            Beam.transform.localScale = new Vector3(Beam.transform.localPosition.x * 2 - 1, BeamWidth, 1);
        else
            Beam.transform.localScale = new Vector3(0,0,0);

        PEEnd = GameObject.Find("PECatcher");
        PEStart = GameObject.Find("PEThrower");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (InitialDelay <= 0.0f)
        {
            if (On)
            {
                PEStart.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                PEStart.GetComponent<ParticleSystem>().Play();
                On = false;
                InitialDelay = ChargeUpTime;
                Beam.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                PEEnd.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                PEEnd.GetComponent<ParticleSystem>().Play();
                On = true;
                InitialDelay = FireTime;
                Beam.transform.localScale = new Vector3(Beam.transform.localPosition.x * 2 - 1, BeamWidth, 1);
            }
        }
        else
            InitialDelay -= (Time.deltaTime * CurrGameSpeed);
    }

    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                if(On)
                    PEStart.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                else
                    PEEnd.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                break;
            case 2:
                if (On)
                    PEStart.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                else
                    PEEnd.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                CurrGameSpeed = 0.25f;
                break;
            case 3:
                if (On)
                    PEStart.GetComponent<ParticleSystem>().Pause();
                else
                    PEEnd.GetComponent<ParticleSystem>().Pause();
                CurrGameSpeed = 0.0f;
                break;
            default:
                if (On)
                    PEStart.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                else
                    PEEnd.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                CurrGameSpeed = 1.0f;
                break;
        }
    }
}
