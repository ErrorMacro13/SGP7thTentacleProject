using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public CursorMode cursorMode = CursorMode.Auto;
    public Texture2D cursor;
    public Texture2D hoverCursor;
    public Texture2D hoverCursor1;
    public Texture2D hoverCursor2;
    public Texture2D hoverCursor3;
    public Texture2D hoverCursor4;


    Vector2 cursorSize = new Vector2(32, 32);

    bool hovered = false;
    float animTime = 0.5f;
    float timer;
    int cursorState;
    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;

        cursor = Resources.Load("Images/Cursors_Timelock_Cursor") as Texture2D;
        hoverCursor = Resources.Load("Images/Cursors_Timelock_clock") as Texture2D;
        hoverCursor1 = Resources.Load("Images/Cursors_Timelock_clock1") as Texture2D;
        hoverCursor2 = Resources.Load("Images/Cursors_Timelock_clock2") as Texture2D;
        hoverCursor3 = Resources.Load("Images/Cursors_Timelock_clock3") as Texture2D;
        hoverCursor4 = Resources.Load("Images/Cursors_Timelock_clock4") as Texture2D;

        Cursor.SetCursor(cursor, new Vector2(16, 0), cursorMode);
        cursorState = 0;
        timer = animTime;
    }

    void Update()
    {
        if (hovered)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                cursorState++;
                if (cursorState > 4)
                    cursorState = 0;
                switch (cursorState)
                {
                    case 0:
                        Cursor.SetCursor(hoverCursor, new Vector2(16, 0), cursorMode);
                        break;
                    case 1:
                        Cursor.SetCursor(hoverCursor1, new Vector2(16, 0), cursorMode);
                        break;
                    case 2:
                        Cursor.SetCursor(hoverCursor2, new Vector2(16, 0), cursorMode);
                        break;
                    case 3:
                        Cursor.SetCursor(hoverCursor3, new Vector2(16, 0), cursorMode);
                        break;
                    case 4:
                        Cursor.SetCursor(hoverCursor4, new Vector2(16, 0), cursorMode);
                        break;
                    default:
                        break;
                }
                
                timer = animTime;
            }
               
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSize.x / 2, Event.current.mousePosition.y, cursorSize.x, cursorSize.y), cursor);
    }

    void Hovered(bool hov)
    {
        if (hov)
        {
            hovered = true;
            Cursor.SetCursor(hoverCursor, new Vector2(16, 0), cursorMode);
        }
        else if (!hov)
        {
            hovered = false;
            cursorState = 0;
            Cursor.SetCursor(cursor, new Vector2(16, 0), cursorMode);
        }
    }
}
