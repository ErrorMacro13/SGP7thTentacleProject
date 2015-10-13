using UnityEngine;
using System.Collections;

public class BackgroundTile : MonoBehaviour {
    public SpriteRenderer sprite;
    public float gridX = 0.0f;
    void Awake()
    {
        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;
        childSprite.sortingOrder = -20;
        GameObject child;
        for (int i = 1; i < gridX - 1; i++)
        {
            child = Instantiate(childPrefab) as GameObject;
            child.transform.position = transform.position + (new Vector3(spriteSize.x, 0, 0) * i - new Vector3(.1f * i, 0, 0));
            child.transform.parent = transform;
        }
        childPrefab.transform.parent = transform;
        sprite.enabled = false;
    }	
}
