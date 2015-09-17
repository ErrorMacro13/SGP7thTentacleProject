using UnityEngine;
using System.Collections;

public class InstructionArrayScript : MonoBehaviour
{

    public Texture2D movement;
    public Texture2D TimeCtrl;
    public Texture2D ImobSpike;
    public Texture2D GunTurr;
    public Texture2D Bullet;
    public Texture2D RetractingSpike;
    public Texture2D Pendulum;
    public Texture2D MovingPlats;
    public Texture2D RPlate;
    public Texture2D GPlate;
    public Texture2D Telsa;
    public Texture2D CrushingWall;
    public Texture2D FallSpike;
    public Texture2D Sawblade;
    public Texture2D Treadmill;
    public Texture2D FallAcid;
    public Texture2D AcidPool;
    public Texture2D CollapsingFlr;
    public Texture2D Gasses;
    public Texture2D Tar;
    public Texture2D ElecFlr;
    public Texture2D LaserTurr;
    public Texture2D Laser;
    public Texture2D BounceBall;
    public Texture2D IcePlat;
    public Texture2D PhazePlat;
    public Texture2D SpinningBlade;
    public Texture2D Fan;
    private Texture2D[] AllTextures;
    private int size = 28;
    private int CurrLLoc = 0;
    private int CurrRLoc = 1;
    private Texture2D CurrLImg;
    private Texture2D CurrRImg;
    private int pagenum = 1;
    private GUIStyle PageTxt;
    // Use this for initialization
    void Start()
    {
        AllTextures = new Texture2D[size];
        AllTextures[0] = movement;
        AllTextures[1] = TimeCtrl;
        AllTextures[2] = ImobSpike;
        AllTextures[3] = GunTurr;
        AllTextures[4] = Bullet;
        AllTextures[5] = RetractingSpike;
        AllTextures[6] = Pendulum;
        AllTextures[7] = MovingPlats;
        AllTextures[8] = RPlate;
        AllTextures[9] = GPlate;
        AllTextures[10] = Telsa;
        AllTextures[11] = CrushingWall;
        AllTextures[12] = FallSpike;
        AllTextures[13] = Sawblade;
        AllTextures[14] = Treadmill;
        AllTextures[15] = FallAcid;
        AllTextures[16] = AcidPool;
        AllTextures[17] = CollapsingFlr;
        AllTextures[18] = Gasses;
        AllTextures[19] = Tar;
        AllTextures[20] = ElecFlr;
        AllTextures[21] = LaserTurr;
        AllTextures[22] = Laser;
        AllTextures[23] = BounceBall;
        AllTextures[24] = IcePlat;
        AllTextures[25] = PhazePlat;
        AllTextures[26] = SpinningBlade;
        AllTextures[27] = Fan;
        CurrLImg = AllTextures[CurrLLoc];
        CurrRImg = AllTextures[CurrRLoc];
        PageTxt = new GUIStyle();
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    public Quaternion a;
    private GameObject ButtonNext;
    private GameObject ButtonPrev;
    void OnGUI()
    {
        PageTxt.fontSize = 40;
        ButtonNext = GameObject.Find("Next");
        ButtonPrev = GameObject.Find("Prev");
        GUI.DrawTexture(new Rect(ButtonPrev.GetComponent<Transform>().position.x - 550, Screen.height * 0.5f - 275, 550, 275), CurrLImg);
        GUI.DrawTexture(new Rect(ButtonNext.GetComponent<Transform>().position.x - 20, Screen.height * 0.5f - 275, 550, 275), CurrRImg);
        GUI.Label(new Rect(ButtonNext.GetComponent<Transform>().position.x - 170, Screen.height * 0.5f, 50, 50), pagenum + "/14", PageTxt);
    }
    void Next()
    {
        pagenum++;
        if (pagenum > 14)
            pagenum = 1;
        CurrLLoc += 2;
        if (CurrLLoc > 27)
            CurrLLoc = 0;
        CurrLImg = AllTextures[CurrLLoc];
        CurrRLoc += 2;
        if (CurrRLoc > 27)
            CurrRLoc = 1;
        CurrRImg = AllTextures[CurrRLoc];
    }
    void Prev()
    {
        pagenum--;
        if (pagenum < 1)
            pagenum = 14;
        CurrLLoc -= 2;
        if (CurrLLoc < 0)
            CurrLLoc = 26;
        CurrLImg = AllTextures[CurrLLoc];
        CurrRLoc -= 2;
        if (CurrRLoc < 0)
            CurrRLoc = 27;
        CurrRImg = AllTextures[CurrRLoc];
    }
}
