using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeY;
    public SpriteRenderer[] spriteArr;

    private SpriteRenderer topSprite;

    private float centerCameraY;

    void Start()
    {

        topSprite = spriteArr[spriteArr.Length - 1];
        centerCameraY = Camera.main.transform.position.y;
        Vector2 position = Camera.main.transform.position;
        foreach (var i in spriteArr)
        {
            i.transform.position = position;
            position.y += i.size.y * i.transform.localScale.y;
        }

    }

    void Update()
    {
        foreach (var i in spriteArr)
        {
            i.transform.position = new Vector2(i.transform.position.x, i.transform.position.y + scrollSpeed * Time.deltaTime);
        }
        foreach (var i in spriteArr)
        {

            if (i.transform.position.y < centerCameraY - i.size.y * i.transform.localScale.y)
            {
                print(i.transform.position.y - i.size.y * i.transform.localScale.y / 2);
                i.transform.position = new Vector2(i.transform.position.x, topSprite.transform.position.y + i.size.y * i.transform.localScale.y);
                topSprite = i;
            }

        }
    }
}

