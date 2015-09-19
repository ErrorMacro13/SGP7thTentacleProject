using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

/*
 * All MOBILE objects will need the following code!
 * 
    float CurrGameSpeed = 1.0f;
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
    //ALL RESETABLE OBJECTS will need this code
    //This should take care of returning the object to its orignal status
    void ResetOverWorld(){ }

   to use the prior code: All actions that affect SPEED will need to be multiplied by the CurrGameSpeed variable
 * 
 */



public class GameWorldScript : MonoBehaviour {
	public GameObject Player;
	public GameObject CameraOne;
	public Texture2D HalfSpeedTexture;
	public Texture2D QuarterSpeedTexture;
	public Texture2D StopSpeedTexture;
	public Texture2D NormalSpeedTexture;
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
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		//slow speed to 1/2
		if (Input.GetKeyDown(KeyCode.Keypad1) && SlowSpeed == 0 && TimeGauge > 0) {
			SlowSpeed++;
			GameTime = 1;
			BroadcastMessage ("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = .75f;
            TimeSlowAfx.Play();
		}
		//slow speed to 1/4
		else if (Input.GetKeyDown(KeyCode.Keypad1) && SlowSpeed == 1 && TimeGauge > 0) {
			SlowSpeed++;
			GameTime = 2;
			BroadcastMessage ("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = .5f;
            TimeSlowAfx.Play();
		}
		//stop speed
		else if (Input.GetKeyDown(KeyCode.Keypad2) && TimeGauge > 0) {
			SlowSpeed = 0;
			GameTime = 3;
			BroadcastMessage ("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = .1f;
            TimeSlowAfx.Play();
		}
		//resume speed
		else if (Input.GetKeyDown(KeyCode.Keypad3) || TimeGauge <= 0) {
            if (GameTime != 0)
            {
                TimeSpeedAfx.Play();
            }
            SlowSpeed = 0;
			GameTime = 0;
			BroadcastMessage ("SetTime", GameTime);
            CameraOne.GetComponent<AudioSource>().pitch = 1.0f;
		}
		if (GameTime != 0)
			Drain (Time.deltaTime);
		if (TimeGauge < 0)
			TimeGauge = 0;
	}
	void Drain(float dt){
		switch (GameTime) {
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
	void Refill(float amt){
        print("refilling");
		TimeGauge += amt;
        if (TimeGauge > MAXMANA)
            TimeGauge = MAXMANA;
	}
	void SetEnergy(float amt){
		TimeGauge = amt;
	}
	float GetEnergy(){
		return TimeGauge;
	}
    float GetTime()
    {
        return ElapsedTime;
    }
	void OnGUI(){
		GUIStyle ManaBarStyle = new GUIStyle ();
		ManaBarStyle.fontSize = 40;
        Rect PercentBar = new Rect(50, 50, TimeGauge * 2 + 60, 45);
        Rect TimeSymbol = new Rect(10, 50, 40, 40);
        //Rect AboveHeadBar = new Rect(420, 180, TimeGauge + 5, 5);
		float mana = Mathf.Round (TimeGauge);
		GUI.skin = MeterSkin;
		GUI.Box (PercentBar, "");
		//if(TimeGauge > 0.0f)
			//GUI.Box (AboveHeadBar, "");
		GUI.skin = null;
		GUI.Box (PercentBar, mana.ToString() + "%", ManaBarStyle);
		switch (GameTime) {
		case 1:
			GUI.DrawTexture (TimeSymbol, HalfSpeedTexture);
			break;
		case 2:
			GUI.DrawTexture (TimeSymbol, QuarterSpeedTexture);
			break;
		case 3:
			GUI.DrawTexture (TimeSymbol, StopSpeedTexture);
			break;
		default:
			GUI.DrawTexture (TimeSymbol, NormalSpeedTexture);
			break;
		}
		float time = Time.time;
		time = Mathf.Round (time * 10) / 10;
        Rect Timer = new Rect(Screen.width - 105, 50, 40, 20);
        Rect TimerLabel = new Rect(Screen.width - 365, 50, 100, 20);
        ElapsedTime = time;
        if(ActiveTimer)
            TimeOnTimer = time;
		GUI.Label (Timer, (TimeOnTimer-TimeBeforeDeath).ToString(), ManaBarStyle);
		GUI.Label (TimerLabel, "Elapsed Time: ", ManaBarStyle);
	}
	void ResetWorld(){
		BroadcastMessage ("ResetOverWorld");
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
}