using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    [SerializeField] Sprite sprite3;
    [SerializeField] Sprite sprite4;

    int activeSprite;
    SpriteRenderer sprite;

    void Start ()
    {
        activeSprite = 1;
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(SpriteSwitching());
        StartCoroutine(Rotation());
    }

    IEnumerator SpriteSwitching()
    {
        while (true)
        {
            sprite.sprite = GetSprite();
            yield return new WaitForSeconds(1f);
            activeSprite++;
        }
    }

    IEnumerator Rotation()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, 90f) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    Sprite GetSprite()
    {
        switch (activeSprite)
        {
            case 1:
                return sprite1;
            case 2:
                return sprite2;
            case 3:
                return sprite3;
            case 4:
                return sprite4;
            default:
                activeSprite = 1;
                return sprite1;
        }
    }
}
