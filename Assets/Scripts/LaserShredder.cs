using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShredder : MonoBehaviour {

    float yMax;
    float yMin;

    void Start()
    {
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 5f;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 5f;
    }

    void Update ()
    {
        if (transform.position.y > yMax || transform.position.y < yMin)
        {
            Destroy(gameObject);
        }
	}
}
