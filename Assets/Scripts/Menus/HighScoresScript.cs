using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class HighScoresScript : MonoBehaviour{
    private short TOTALLEVELCOUNT = 30;
    public Text level;
    public Text FN1;
    public Text FN2;
    public Text FN3;
    public Text FN4;
    public Text FN5;
    public Text FN6;
    public Text FN7;
    public Text FN8;
    public Text FN9;
    public Text FN10;
    public Text FS1;
    public Text FS2;
    public Text FS3;
    public Text FS4;
    public Text FS5;
    public Text FS6;
    public Text FS7;
    public Text FS8;
    public Text FS9;
    public Text FS10;
    public Text AN1;
    public Text AN2;
    public Text AN3;
    public Text AN4;
    public Text AN5;
    public Text AN6;
    public Text AN7;
    public Text AN8;
    public Text AN9;
    public Text AN10;
    public Text AS1;
    public Text AS2;
    public Text AS3;
    public Text AS4;
    public Text AS5;
    public Text AS6;
    public Text AS7;
    public Text AS8;
    public Text AS9;
    public Text AS10;
    private int levelNum = 0;
    private GameObject xml;
    private LevelData myLevel = new LevelData();
	// Use this for initialization
	void Start () {
        xml = GameObject.Find("SaveDataLoader");
        myLevel = xml.GetComponent<XMLScript>().LoadLevel(levelNum);
        level.text = (levelNum + 1).ToString();
        UpdateScores();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Next()
    {
        levelNum++;
        if (levelNum > TOTALLEVELCOUNT - 1) levelNum = 0;
        level.text = (levelNum + 1).ToString();
        myLevel = xml.GetComponent<XMLScript>().LoadLevel(levelNum);
        UpdateScores();
    }
    void Prev()
    {
        levelNum--;
        if (levelNum < 0) levelNum = TOTALLEVELCOUNT - 1;
        level.text = (levelNum + 1).ToString();
        myLevel = xml.GetComponent<XMLScript>().LoadLevel(levelNum);
        UpdateScores();
    }
    void UpdateScores()
    {
        print("updating list to level " + levelNum);
        FN1.text = myLevel.Free_Play_Times[0].name;
        FN2.text = myLevel.Free_Play_Times[1].name;
        FN3.text = myLevel.Free_Play_Times[2].name;
        FN4.text = myLevel.Free_Play_Times[3].name;
        FN5.text = myLevel.Free_Play_Times[4].name;
        FN6.text = myLevel.Free_Play_Times[5].name;
        FN7.text = myLevel.Free_Play_Times[6].name;
        FN8.text = myLevel.Free_Play_Times[7].name;
        FN9.text = myLevel.Free_Play_Times[8].name;
        FN10.text = myLevel.Free_Play_Times[9].name;
        FS1.text = myLevel.Free_Play_Times[0].time.ToString() + " s";
        FS2.text = myLevel.Free_Play_Times[1].time.ToString() + " s";
        FS3.text = myLevel.Free_Play_Times[2].time.ToString() + " s";
        FS4.text = myLevel.Free_Play_Times[3].time.ToString() + " s";
        FS5.text = myLevel.Free_Play_Times[4].time.ToString() + " s";
        FS6.text = myLevel.Free_Play_Times[5].time.ToString() + " s";
        FS7.text = myLevel.Free_Play_Times[6].time.ToString() + " s";
        FS8.text = myLevel.Free_Play_Times[7].time.ToString() + " s";
        FS9.text = myLevel.Free_Play_Times[8].time.ToString() + " s";
        FS10.text = myLevel.Free_Play_Times[9].time.ToString() + " s";
        AN1.text = myLevel.Arcade_Scores[0].name;
        AN2.text = myLevel.Arcade_Scores[1].name;
        AN3.text = myLevel.Arcade_Scores[2].name;
        AN4.text = myLevel.Arcade_Scores[3].name;
        AN5.text = myLevel.Arcade_Scores[4].name;
        AN6.text = myLevel.Arcade_Scores[5].name;
        AN7.text = myLevel.Arcade_Scores[6].name;
        AN8.text = myLevel.Arcade_Scores[7].name;
        AN9.text = myLevel.Arcade_Scores[8].name;
        AN10.text = myLevel.Arcade_Scores[9].name;
        AS1.text = myLevel.Arcade_Scores[0].score.ToString() + " pts";
        AS2.text = myLevel.Arcade_Scores[1].score.ToString() + " pts";
        AS3.text = myLevel.Arcade_Scores[2].score.ToString() + " pts";
        AS4.text = myLevel.Arcade_Scores[3].score.ToString() + " pts";
        AS5.text = myLevel.Arcade_Scores[4].score.ToString() + " pts";
        AS6.text = myLevel.Arcade_Scores[5].score.ToString() + " pts";
        AS7.text = myLevel.Arcade_Scores[6].score.ToString() + " pts";
        AS8.text = myLevel.Arcade_Scores[7].score.ToString() + " pts";
        AS9.text = myLevel.Arcade_Scores[8].score.ToString() + " pts";
        AS10.text = myLevel.Arcade_Scores[9].score.ToString() + " pts";
    }
}
