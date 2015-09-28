using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayersData
{
    public PlayersData() { }
    public int levelNumber;
    public int mode;
    public GameObject bounceBack;
    public string name;
    public float time;
    public float score;
}
public class CheckPointScript : MonoBehaviour
{

    public GameObject Door;
    public float YDown;
    public int CheckpointNumber = 0;
    public bool EndOfLevelCheckPoint = true;
    bool hit = false;
    private GameObject saver;
    private GameObject World;
    private GameObject SM;
    private GameObject Player;
    private PlayersData data = new PlayersData();
    public GameObject[] levelObjects;
    public bool levelActive = false;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        World = GameObject.Find("GameOverWorld");
        saver = GameObject.Find("SaveDataLoader");
        SM = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hit)
        {
            hit = true;
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y - YDown, Door.transform.position.z);
            //print(Player.GetComponent<PlayerController>().GetCurrentLevel());
            if (Player.GetComponent<PlayerController>().GetCurrentLevel() != CheckpointNumber)
            {
                if (EndOfLevelCheckPoint && CheckpointNumber - 1 != -1)
                {
                    World.SendMessage("SavePlayersCurrentLevelAndScore", CheckpointNumber);
                    data.levelNumber = CheckpointNumber - 1;
                    data.bounceBack = this.gameObject;
                    SM.SendMessage("SavePlayersData", data);
                }
            }

            if (levelActive)
            {
                print("active");
                for (int i = 0; i < levelObjects.Length; i++)
                {
                    levelObjects[i].SendMessage("ToggleActive", true);
                }
            }
            else
            {
                print("deactive");
                for (int i = 0; i < levelObjects.Length; i++)
                {
                    levelObjects[i].SendMessage("ToggleActive", false);
                }
            }
        }
    }
    public bool GetEndFlag()
    {
        return EndOfLevelCheckPoint;
    }
    void SavePlayersData(PlayersData data)
    {
        print("bouncing to world");
        World.SendMessage("SavePlayersData", data);
    }
}