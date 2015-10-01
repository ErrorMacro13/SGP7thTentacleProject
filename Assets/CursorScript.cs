using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public CursorMode cursorMode = CursorMode.Auto;
    public Texture2D cursor;
    public Texture2D hoverCursor;

    Vector2 cursorSize = new Vector2(32, 32);

    bool hovered = false;

    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;

        cursor = Resources.Load("Images/Cursors_Timelock_Cursor") as Texture2D;
        hoverCursor = Resources.Load("Images/Cursors_Timelock_clock") as Texture2D;
        Cursor.SetCursor(cursor, new Vector2(16, 0), cursorMode);
    }

    void Update()
    {
        
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
            Cursor.SetCursor(hoverCursor, new Vector2(16, 0), cursorMode);
        }
        else if (!hov)
        {
            Cursor.SetCursor(cursor, new Vector2(16, 0), cursorMode);
        }
    }
}
