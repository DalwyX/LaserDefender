using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    [SerializeField] GameObject bonusVFXPrefab;
    [SerializeField] AudioClip pickupClip;
    SpriteRenderer sprite;
    GameObject vfx;
    int mult = 1;


    void Start()
    {
        vfx = Instantiate(bonusVFXPrefab, transform.position, Quaternion.identity) as GameObject;
        vfx.transform.Rotate(new Vector3(90f, 0f));
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Fading());
    }

    void Update()
    {
        vfx.transform.position = transform.position;
    }

    IEnumerator Fading()
    {
        while (true)
        {
            float alpha = sprite.color.a;
            if (alpha <= 0.2f || alpha >= 1f)
            {
                mult *= -1;
            }
            alpha = alpha + mult * 0.05f;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            yield return new WaitForSeconds(0.033f);
        }
    }

    public void PickUp()
    {
        AudioSource.PlayClipAtPoint(pickupClip, Camera.main.transform.position);
        Destroy(vfx);
        Destroy(gameObject);
    }
}
