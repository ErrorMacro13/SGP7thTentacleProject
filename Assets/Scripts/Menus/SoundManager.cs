using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource hover;
    public AudioSource click;
    public AudioSource back;
    public int GameState = 0;
    public static SoundManager ths;
    public string PlayerName = "Cody";
    public static SoundManager Instance { get; private set; }

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
  
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
	}

    void ArcadeState()
    {
        GameState = 1;
    }

    void FreePlayState()
    {
        GameState = 2;
    }

    int GetState()
    {
        return GameState;
    }

    void In()
    {
        click.Play();
    }
    void Out()
    {
        back.Play();
    }
    void Hovered()
    {
        hover.Play();
    }
    void SavePlayersData(PlayersData data)
    {
        data.name = PlayerName;
        data.mode = GameState;
        data.bounceBack.SendMessage("SavePlayersData", data);
    }
}
