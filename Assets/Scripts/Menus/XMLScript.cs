using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class Settings
{
    public Settings() { }
    public float BGvol;
    public float AFXvol;
    public float Mastervol;
    public bool FullScreen;
}
public class ArcadeScores
{
    public ArcadeScores() { }
    public ArcadeScores(string n, float s) { name = n; score = s; }
    public string name;
    public float score;
}
public class FreePlayTimes
{
    public FreePlayTimes() { }
    public FreePlayTimes(string n, float t) { name = n; time = t; }
    public string name;
    public float time;
}
public class LevelData
{
    public LevelData() {
        for (int i = 0; i < 10; i++)
        {
            Arcade_Scores[i] = new ArcadeScores();
            Free_Play_Times[i] = new FreePlayTimes();
        }
    }
    public ArcadeScores[] Arcade_Scores = new ArcadeScores[10];
    public FreePlayTimes[] Free_Play_Times = new FreePlayTimes[10];
}
public class CurrentPlayerStats
{
    public CurrentPlayerStats() { }
    public int level;
    public float score;
    public int life;
}
public class XMLScript : MonoBehaviour
{
    public Slider BGS = null;
    public Slider AFXS = null;
    public Slider MS = null;
    public Toggle FS = null;
    public Text LevelText = null;
    public AudioSource BGMusicSource = null;
    private Settings sets = new Settings();
    private int TOTALLEVELS = 30;
    private float DefaultedGame = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("DefaultGame")) DefaultedGame = PlayerPrefs.GetFloat("DefaultGame");
        if (Application.loadedLevelName == "MainMenu" && DefaultedGame == 0) StartGame();
        CurrentPlayerStats CPS = new CurrentPlayerStats();
        CPS = LoadPlayersStats();
        if (LevelText != null) LevelText.text = "Level "+(CPS.level+1);
        ApplySettings();
    }
    void StartGame()
    {
        PlayerPrefs.SetFloat("DefaultGame", 1);
        DefaultSettings();
        DefaultHighScoresFiles();
        OrganizeLevelFiles();
        ChangePlayersStats(0);
    }
    void Update()
    {

    }

    public void DefaultSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", 75);
        PlayerPrefs.SetFloat("BackgroundVolume", 0);
        PlayerPrefs.SetFloat("AFXVolume", 100);
        PlayerPrefs.SetFloat("FullScreen", 0);
        PlayerPrefs.Save();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", MS.value);
        PlayerPrefs.SetFloat("BackgroundVolume", BGS.value);
        PlayerPrefs.SetFloat("AFXVolume", AFXS.value);
        if (FS.isOn) PlayerPrefs.SetFloat("FullScreen", 1);
        else PlayerPrefs.SetFloat("FullScreen", 0);
        PlayerPrefs.Save();
    }

    public Settings LoadSettings()
    {
        Settings set = new Settings();
        set.Mastervol = PlayerPrefs.GetFloat("MasterVolume");
        set.BGvol = PlayerPrefs.GetFloat("BackgroundVolume");
        set.AFXvol = PlayerPrefs.GetFloat("AFXVolume");
        float temp = PlayerPrefs.GetFloat("FullScreen");
        //if (temp == 1) set.FullScreen = true;
        //else set.FullScreen = false;
        set.FullScreen = false;
        return set;
    }

    public void ApplySettings()
    {
        Settings set = new Settings();
        set = LoadSettings();
        if (BGS != null && AFXS != null && FS != null && MS != null)
        {
            MS.value = set.Mastervol;
            AFXS.value = set.AFXvol;
            BGS.value = set.BGvol;
            FS.isOn = set.FullScreen;
        }
        else if (Application.loadedLevelName == "MainMenu")
            Screen.fullScreen = set.FullScreen;
        BGMusicSource.ignoreListenerVolume = true;
        AudioListener.volume = (set.AFXvol / 100) * (set.Mastervol / 100);
        BGMusicSource.volume = (set.BGvol / 100) * (set.Mastervol / 100);
    }

    public void SaveLevel(LevelData data, float levelNum)
    {
        for (int i = 0; i < 10; i++)
        {
            string varScore = "Level" + levelNum + "ArcadeScore" + i;
            string varScoreName = "Level" + levelNum + "ArcadeName" + i;
            string varTime = "Level" + levelNum + "FreePTime" + i;
            string varTimeName = "Level" + levelNum + "FreePName" + i;
            PlayerPrefs.SetFloat(varScore, data.Arcade_Scores[i].score);
            PlayerPrefs.SetString(varScoreName, data.Arcade_Scores[i].name);
            PlayerPrefs.SetFloat(varTime, data.Free_Play_Times[i].time);
            PlayerPrefs.SetString(varTimeName, data.Free_Play_Times[i].name);
        }
        PlayerPrefs.Save();
    }

    public LevelData LoadLevel(float levelNum)
    {
        LevelData leveldata = new LevelData();
        //actually allocate the arrays
        for (int i = 0; i < 10; i++)
        {
            string varScore = "Level" + levelNum + "ArcadeScore" + i;
            string varScoreName = "Level" + levelNum + "ArcadeName" + i;
            string varTime = "Level" + levelNum + "FreePTime" + i;
            string varTimeName = "Level" + levelNum + "FreePName" + i;
            leveldata.Arcade_Scores[i].score = PlayerPrefs.GetFloat(varScore);
            leveldata.Arcade_Scores[i].name = PlayerPrefs.GetString(varScoreName);
            leveldata.Free_Play_Times[i].time = PlayerPrefs.GetFloat(varTime);
            leveldata.Free_Play_Times[i].name = PlayerPrefs.GetString(varTimeName);
        }
        return leveldata;
    }

    public void DefaultHighScoresFiles()
    {
        //initalize all levels to a random default set of values and names
        List<string> nameArray = new List<string>();
        nameArray.Add("bob");
        nameArray.Add("Susan");
        nameArray.Add("Joe");
        nameArray.Add("Alice");
        nameArray.Add("Mark");
        nameArray.Add("Miranda");
        nameArray.Add("Cody");
        nameArray.Add("Bryce");
        nameArray.Add("Xephos");
        nameArray.Add("Kim");
        for (int j = 0; j < TOTALLEVELS; j++)
        {
            LevelData MyLevel = new LevelData();
            for (int i = 0; i < 10; i++)
            {
                MyLevel.Arcade_Scores[i] = new ArcadeScores(nameArray[(int)Random.Range(0, 9)], (int)Random.Range(0, 99999));
                MyLevel.Free_Play_Times[i] = new FreePlayTimes(nameArray[(int)Random.Range(0, 9)], Random.Range(0, 200));
            }
            SaveLevel(MyLevel, j);
        }
    }

    public void AddAdditionalDefaultLevelFile(int levelNumber)
    {
        //add an additional level set to random default scores and names
        List<string> nameArray = new List<string>();
        nameArray.Add("bob");
        nameArray.Add("Susan");
        nameArray.Add("Joe");
        nameArray.Add("Alice");
        nameArray.Add("Mark");
        nameArray.Add("Miranda");
        nameArray.Add("Cody");
        nameArray.Add("Bryce");
        nameArray.Add("Xephos");
        nameArray.Add("Kim");
        LevelData MyLevel = new LevelData();
        for (int i = 0; i < 10; i++)
        {
            MyLevel.Arcade_Scores[i] = new ArcadeScores(nameArray[(int)Random.Range(0, 9)], (int)Random.Range(0, 99999));
            MyLevel.Free_Play_Times[i] = new FreePlayTimes(nameArray[(int)Random.Range(0, 9)], Random.Range(0, 99999));
        }
        SaveLevel(MyLevel, levelNumber - 1);
    }

    public void OrganizeLevelFiles(bool all = true, int LevelNum = 0)
    {
        //load in, sort and resave all level files
        if (all)
        {
            for (int i = 0; i < TOTALLEVELS; i++)
            {
                LevelData level = new LevelData();
                level = LoadLevel(i);
                List<ArcadeScores> scores = new List<ArcadeScores>();
                List<FreePlayTimes> times = new List<FreePlayTimes>();
                for (int j = 0; j < 10; j++)
                {
                    times.Add(level.Free_Play_Times[j]);
                    scores.Add(level.Arcade_Scores[j]);
                }
                scores.Sort((l1, l2) => (int)(l2.score - l1.score));
                times.Sort((l1, l2) => (int)(l1.time - l2.time));
                level.Free_Play_Times = times.ToArray();
                level.Arcade_Scores = scores.ToArray();
                SaveLevel(level, i);
            }
        }
        else
        {
            LevelData level = new LevelData();
            level = LoadLevel(LevelNum);
            List<ArcadeScores> scores = new List<ArcadeScores>();
            List<FreePlayTimes> times = new List<FreePlayTimes>();
            for (int j = 0; j < 10; j++)
            {
                times.Add(level.Free_Play_Times[j]);
                scores.Add(level.Arcade_Scores[j]);
            }
            scores.Sort((l1, l2) => (int)(l2.score - l1.score));
            times.Sort((l1, l2) => (int)(l1.time - l2.time));
            SaveLevel(level, LevelNum);
        }
    }

    public void SavePlayersStats(CurrentPlayerStats CPS)
    {
        //save players level and score
        PlayerPrefs.SetInt("PlayersLevel", CPS.level);
        PlayerPrefs.SetFloat("PlayersScore", CPS.score);
        PlayerPrefs.SetInt("PlayersLife", CPS.life);
        PlayerPrefs.Save();
    }

    public CurrentPlayerStats LoadPlayersStats()
    {
        //load players level and score
        CurrentPlayerStats CPS = new CurrentPlayerStats();
        CPS.level = PlayerPrefs.GetInt("PlayersLevel");
        CPS.score = PlayerPrefs.GetFloat("PlayersScore");
        CPS.life = PlayerPrefs.GetInt("PlayersLife");
        return CPS;
    }

    public void SavePlayersScores(PlayersData data)
    {
        //update high scores
        switch (data.mode)
        {
            case 1: //Arcade Score
                {
                    LevelData tempData = LoadLevel(data.levelNumber);
                    List<ArcadeScores> scores = new List<ArcadeScores>();
                    scores.Add(new ArcadeScores(data.name, data.score));
                    for (int i = 0; i < 10; i++)
                    {
                        scores.Add(tempData.Arcade_Scores[i]);
                    }
                    scores.Sort((l1, l2) => (int)(l2.score - l1.score));
                    scores.RemoveAt(scores.Count - 1);
                    tempData.Arcade_Scores = scores.ToArray();
                    SaveLevel(tempData, data.levelNumber);
                    break;
                }
            case 2: //Free Play Time
                {
                    LevelData tempData = LoadLevel(data.levelNumber);
                    List<FreePlayTimes> times = new List<FreePlayTimes>();
                    times.Add(new FreePlayTimes(data.name, data.time));
                    for (int i = 0; i < 10; i++)
                    {
                        times.Add(tempData.Free_Play_Times[i]);
                    }
                    times.Sort((l1, l2) => (int)(l1.time - l2.time));
                    times.RemoveAt(times.Count - 1);
                    tempData.Free_Play_Times = times.ToArray();
                    SaveLevel(tempData, data.levelNumber);
                    break;
                }
            default:
                break;
        }
    }

    public void ChangePlayersStats(int level)
    {
        PlayerPrefs.SetInt("PlayersLevel", level);
        PlayerPrefs.SetFloat("PlayersScore", 0);
        PlayerPrefs.Save();
    }
}
