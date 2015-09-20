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
    [XmlAttribute("Name")]
    public string name;
    [XmlAttribute("Score")]
    public float score;
}
public class FreePlayTimes
{
    public FreePlayTimes() { }
    public FreePlayTimes(string n, float t) { name = n; time = t; }
    [XmlAttribute("Name")]
    public string name;
    [XmlAttribute("Time")]
    public float time;
}
[XmlRoot("Level")]
public class LevelData
{
    public LevelData() { }
    [XmlArray("ArcadeScores")]
    [XmlArrayItem("ArcadeScore")]
    public ArcadeScores[] ArcadeScores = new ArcadeScores[10];
    public FreePlayTimes[] FreePlayTimes = new FreePlayTimes[10];
}
public class XMLScript : MonoBehaviour
{
    public Slider BGS = null;
    public Slider AFXS = null;
    public Slider MS = null;
    public Toggle FS = null;
    public AudioSource BGMusicSource = null;
    private Settings sets = new Settings();
    private int TOTALLEVELS = 30;
    // Use this for initialization
    void Start()
    {
        //InitalizeAllLevelFilesToDefaults();
        //LoadAndOrganizeLevel();
        ApplySettingsData("XML\\Settings\\SettingsData");
    }
    // Update is called once per frame
    void Update()
    {

    }
    //save out the data to a specified path
    public void SaveSettingsData(string path)
    {
        sets.AFXvol = AFXS.value;
        sets.BGvol = BGS.value;
        sets.Mastervol = MS.value;
        sets.FullScreen = FS.isOn;
        var serializer = new XmlSerializer(typeof(Settings));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, sets);
            stream.Close();
        }
    }
    //load data from a specified path
    public static Settings LoadSettingsData(string path)
    {
        var serializer = new XmlSerializer(typeof(Settings));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as Settings;
        }
    }
    public void SaveLevelData(string path, LevelData level)
    {
        var serializer = new XmlSerializer(typeof(LevelData));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, level);
            stream.Close();
        }
    }
    public LevelData LoadLevelData(string path)
    {
        var serializer = new XmlSerializer(typeof(LevelData));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as LevelData;
        }
    }
    //apply data from a specific path
    //using this code inside settings will set the graphics for the sliders + toggle
    //using this code outside settings will alter the AFX/BG sounds + FullScreen
    public void ApplySettingsData(string path)
    {
        Settings set = new Settings();
        set = LoadSettingsData(path);
        if (Application.loadedLevelName == "Options" && BGS != null && AFXS != null && FS != null && MS != null)
        {
            print("Setting Slider Data...");
            MS.value = set.Mastervol;
            AFXS.value = set.AFXvol;
            BGS.value = set.BGvol;
            FS.isOn = set.FullScreen;
        }
        else if (Application.loadedLevelName == "MainMenu")
            ToggleFullScreen(set.FullScreen);
        BGMusicSource.ignoreListenerVolume = true;
        AudioListener.volume = (set.AFXvol / 100) * (set.Mastervol / 100);
        BGMusicSource.volume = (set.BGvol / 100) * (set.Mastervol / 100);
    }
    public void ToggleFullScreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
    public void InitalizeAllLevelFilesToDefaults()
    {
        print("beginning initalization");
        for (int j = 0; j < TOTALLEVELS; j++)
        {
            LevelData MyLevel = new LevelData();
            for (int i = 0; i < 10; i++)
            {
                MyLevel.ArcadeScores[i] = new ArcadeScores("BoB", (int)Random.Range(0,99999));
                MyLevel.FreePlayTimes[i] = new FreePlayTimes("Jill", Random.Range(0,200));
                print("populated level");
            }
            string pathname = "XML\\Levels\\LevelData" + j;
            SaveLevelData(pathname, MyLevel);
            print("saved new file");
        }
    }
    public void AddAdditionalDefaultLevelFile(int levelNumber)
    {
        LevelData MyLevel = new LevelData();
        for (int i = 0; i < 10; i++)
        {
            MyLevel.ArcadeScores[i] = new ArcadeScores("BoB", 99999);
            MyLevel.FreePlayTimes[i] = new FreePlayTimes("Jill", 60.3f);
        }
        string pathname = "XML\\Levels\\LevelData" + (levelNumber - 1);
        SaveLevelData(pathname, MyLevel);
    }
    public LevelData LoadAndOrganizeLevel(bool all = true, int LevelToOrg = 0)
    {
        if (all)
        {
            for (int i = 0; i < TOTALLEVELS; i++)
            {
                string pathname = "XML\\Levels\\LevelData" + i;
                LevelData level = new LevelData();
                level = LoadLevelData(pathname);
                List<ArcadeScores> scores = new List<ArcadeScores>();
                List<FreePlayTimes> times = new List<FreePlayTimes>();
                for (int j = 0; j < 10; j++)
                {
                    times.Add(level.FreePlayTimes[j]);
                    scores.Add(level.ArcadeScores[j]);
                }
                scores.Sort((l1, l2) => (int)(l2.score - l1.score));
                times.Sort((l1, l2) => (int)(l1.time - l2.time));
                level.FreePlayTimes = times.ToArray();
                level.ArcadeScores = scores.ToArray();
                    SaveLevelData(pathname, level);
            }
            return new LevelData();
        }
        else
        {
            string pathname = "XML\\Levels\\LevelData" + LevelToOrg;
            LevelData level = new LevelData();
            level = LoadLevelData(pathname);
            List<ArcadeScores> scores = new List<ArcadeScores>();
            List<FreePlayTimes> times = new List<FreePlayTimes>();
            for (int j = 0; j < 10; j++)
            {
                times.Add(level.FreePlayTimes[j]);
                scores.Add(level.ArcadeScores[j]);
            }
            scores.Sort((l1, l2) => (int)(l1.score - l2.score));
            times.Sort((l1, l2) => (int)(l1.time - l2.time));
            SaveLevelData(pathname, level);
            return new LevelData();
        }
    }
}
