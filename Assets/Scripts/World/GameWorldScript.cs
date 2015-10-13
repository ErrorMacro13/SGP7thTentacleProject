using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

/*
 * All MOBILE objects will need the following code!
 * 

    private Vector3 StartLoc;

    private float CurrGameSpeed = 1.0f;
	void SetTime(short GameSpeed){
		switch (GameSpeed) {
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
    
    void ResetOverWorld(){

    }

   to use the prior code: All actions that affect SPEED will need to be multiplied by the CurrGameSpeed variable
 * 
 */



public class GameWorldScript : MonoBehaviour
{
    public bool DisableDrain = true;
    public GameObject Player;
    public GameObject CameraOne;
    public Texture2D HalfSpeedTexture;
    public Texture2D QuarterSpeedTexture;
    public Texture2D StopSpeedTexture;
    public Texture2D NormalSpeedTexture;
    public Texture2D Gauntlet;
    public Texture2D GauntletBG;
    public Texture2D TimerBG;
    public Texture2D Health;
    public GUISkin MeterSkin;
    public AudioSource TimeSlowAfx;
    public AudioSource TimeSpeedAfx;
    public float MAXMANA = 100;
    private float TimeGauge = 100;
    private short GameTime = 0;
    private short SlowSpeed = 0;
    private float ElapsedTime = 0;
    private bool ActiveTimer = true;
    private float TimeOnTimer;
    private float TimeBeforeDeath;
    private GameObject saver;
    private GameObject soundManager;
    private int tempX;
    private int tempY = 0;
    private float LastCheckPointTime;
    // Use this for initialization
    void Start()
    {
        saver = GameObject.Find("SaveDataLoader");
        soundManager = GameObject.Find("SoundManager");
    }
    // Update is called once per frame
    void Update()
    {
        //slow speed to 1/2
        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.LeftArrow)) && SlowSpeed == 0 && TimeGauge > 0 && Time.timeScale > 0.0f)
        {
            SlowSpeed++;
            GameTime = 1;
            BroadcastMessage("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = .75f;
            TimeSlowAfx.Play();
        }
        //slow speed to 1/4
        else if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.LeftArrow)) && SlowSpeed == 1 && TimeGauge > 0 && Time.timeScale > 0.0f)
        {
            SlowSpeed++;
            GameTime = 2;
            BroadcastMessage("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = .5f;
            TimeSlowAfx.Play();
        }
        //stop speed
        else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.DownArrow)) && TimeGauge > 0 && Time.timeScale > 0.0f)
        {
            SlowSpeed = 0;
            GameTime = 3;
            BroadcastMessage("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = .1f;
            TimeSlowAfx.Play();
        }
        //resume speed
        else if (((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.RightArrow)) || TimeGauge <= 0) && Time.timeScale > 0.0f)
        {
                if (GameTime != 0)
                {
                    TimeSpeedAfx.Play();
                }
                SlowSpeed = 0;
                GameTime = 0;
                BroadcastMessage("SetTime", GameTime);
                CameraOne.GetComponent<AudioSource>().pitch = 1.0f;
            
        }
        if (GameTime != 0 && !DisableDrain)
            Drain(Time.deltaTime);
        if (TimeGauge < 0)
            TimeGauge = 0;
    }
    void Drain(float dt)
    {
        switch (GameTime)
        {
            case 1:
                TimeGauge -= 10 * dt;
                break;
            case 2:
                TimeGauge -= 20 * dt;
                break;
            case 3:
                TimeGauge -= 30 * dt;
                break;
            default:
                TimeGauge -= 0;
                break;
        }
    }
    void Refill(float amt)
    {
        print("refilling");
        TimeGauge += amt;
        if (TimeGauge > MAXMANA)
            TimeGauge = MAXMANA;
    }
    public void SetEnergy(float amt)
    {
        TimeGauge = amt;
    }
    float GetEnergy()
    {
        return TimeGauge;
    }
    public float GetTime()
    {
        return TimeOnTimer - TimeBeforeDeath;
    }
    public Quaternion a;
    void OnGUI()
    {
        GUIStyle ManaBarStyle = new GUIStyle();
        ManaBarStyle.fontSize = 40;
        Rect PercentBar = new Rect(90, 50, TimeGauge + (TimeGauge/15), 45);
        Rect TimeSymbol = new Rect(268, 32, 40, 40);
        //Rect AboveHeadBar = new Rect(420, 180, TimeGauge + 5, 5);
        float mana = Mathf.Round(TimeGauge);
        GUI.DrawTexture(new Rect(Screen.width - 410, 30, 500, 85), TimerBG);
        GUI.DrawTexture(new Rect(10, -21, 360, 200), GauntletBG);
        GUI.skin = MeterSkin;
        float green = TimeGauge/100;
        float red = 1-green;
        GUI.color = new Color(red,green,0);
        GUI.Box(PercentBar, "");
        GUI.color = new Color(1, 1, 1);
        //if(TimeGauge > 0.0f)
        //GUI.Box (AboveHeadBar, "");
        GUI.skin = null;
        GUI.DrawTexture(new Rect(10, -21, 360, 200), Gauntlet);
        switch (GameTime)
        {
            case 1:
                GUI.DrawTexture(TimeSymbol, HalfSpeedTexture);
                break;
            case 2:
                GUI.DrawTexture(TimeSymbol, QuarterSpeedTexture);
                break;
            case 3:
                GUI.DrawTexture(TimeSymbol, StopSpeedTexture);
                break;
            default:
                GUI.DrawTexture(TimeSymbol, NormalSpeedTexture);
                break;
        }
        float time = Time.time;
        time = Mathf.Round(time * 10) / 10;
        Rect Timer = new Rect(Screen.width - 105, 50, 40, 20);
        Rect TimerLabel = new Rect(Screen.width - 365, 50, 100, 20);
        ElapsedTime = time;
        if (ActiveTimer)
            TimeOnTimer = time;
        GUI.Label(Timer, (TimeOnTimer - TimeBeforeDeath).ToString(), ManaBarStyle);
        GUI.Label(TimerLabel, "Elapsed Time: ", ManaBarStyle);
        if (soundManager.GetComponent<SoundManager>().GameState == 1)
        {
            for (int i = 1; i < Player.GetComponent<PlayerController>().GetLives() + 1; i++)
            {
                if (i > 10)
                {
                    tempX = i - 10;
                    tempY = 1;
                }
                else
                {
                    tempX = i;
                    tempY = 0;
                }
                GUI.DrawTexture(new Rect(Screen.width - (400 - (35 * tempX)), 107 + (35 * tempY), 32, 32), Health);
            }
        }
    }
    public void IsLifeAdded(float time)
    {
        LastCheckPointTime = time;
        if (GetTime() < time) Player.GetComponent<PlayerController>().AddLife();
        else return;
    }
    void ResetWorld()
    {
        BroadcastMessage("ResetOverWorld");
    }
    void ResetTimer()
    {
        ActiveTimer = false;
    }
    void StartTimer()
    {
        TimeBeforeDeath = Time.time;
        ActiveTimer = true;
    }
    void ZeroTimer()
    {
        float time = Time.time;
        time = Mathf.Round(time * 10) / 10;
        TimeBeforeDeath = time;
    }
    public int CalcScore()
    {
        int TimeComponent = (int)GetTime() - (int)LastCheckPointTime;
        int ScoreComponent = (int)Player.GetComponent<PlayerController>().GetScore();
        int LifeComponent = (int)Player.GetComponent<PlayerController>().GetLives();
        return ScoreComponent + 64 * TimeComponent + LifeComponent * 200;
    }
    void SavePlayersData(PlayersData data)
    {
        data.time = GetTime();
        data.score = CalcScore();
        saver.SendMessage("SavePlayersScores", data);
    }
    void SavePlayersCurrentLevelAndScore(int num)
    {
        CurrentPlayerStats CPL = new CurrentPlayerStats();
        CPL.level = num;
        CPL.score = CalcScore();
        saver.SendMessage("SavePlayersStats", CPL);
    }
}