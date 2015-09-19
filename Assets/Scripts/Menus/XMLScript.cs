using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Settings
{
    public Settings() { }
    public float BGvol;
    public float AFXvol;
    public float Mastervol;
    public bool FullScreen;
}
public class XMLScript : MonoBehaviour
{
    public Slider BGS = null;
    public Slider AFXS = null;
    public Slider MS = null;
    public Toggle FS = null;
    public AudioSource BGMusicSource = null;
    private Settings sets = new Settings();
    // Use this for initialization
    void Start()
    {
        ApplyData("SettingsData");
        print("Start Ran");
    }
    // Update is called once per frame
    void Update()
    {

    }
    //save out the data to a specified path
    public void SaveData(string path)
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
    public static Settings LoadData(string path)
    {
        var serializer = new XmlSerializer(typeof(Settings));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as Settings;
        }
    }
    //apply data from a specific path
    //using this code inside settings will set the graphics for the sliders + toggle
    //using this code outside settings will alter the AFX/BG sounds + FullScreen
    public void ApplyData(string path)
    {
        switch (path)
        {
            case "SettingsData":
                {
                    Settings set = new Settings();
                    set = LoadData(path);
                    if (BGS == null || AFXS == null || FS == null)
                    {
                        print("ERROR UNABLE TO READ SLIDERS");
                    }
                    if (Application.loadedLevelName == "Options" && BGS != null && AFXS != null && FS != null)
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
                    AudioListener.volume = set.AFXvol/100;
                    BGMusicSource.volume = set.BGvol/100;
                    break;
                }
            case "LevelData":
                {
                    break;
                }
        }
    }
    public void ToggleFullScreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
}
