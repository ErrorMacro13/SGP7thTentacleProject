using UnityEngine;
using System.Collections;

public class FloorTile : MonoBehaviour {
    public SpriteRenderer sprite;
    void Awake()
    {
        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;
        childSprite.sortingOrder = 4;
        GameObject child;
        int l = (int)Mathf.Round(transform.localScale.x);
        for (int i = 1; i < l - 1; i++)
        {
            child = Instantiate(childPrefab) as GameObject;
            child.transform.position = transform.position + (new Vector3(spriteSize.x, 0, 0) * i);
            child.transform.parent = transform;
        }
        for (int i = 1; i < l - 1; i++)
        {
            child = Instantiate(childPrefab) as GameObject;
            child.transform.position = transform.position - (new Vector3(spriteSize.x, 0, 0) * i);
            child.transform.parent = transform;
        }
        childPrefab.transform.parent = transform;
        sprite.enabled = false;
    }
}