using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

	[SerializeField] float rotationSpeed = 1f;
	
	void Update ()
    {
        Vector3 rotation = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        transform.Rotate(rotation);
	}
}
