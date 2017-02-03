using UnityEngine;
using System.Collections;

public class RotateSun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(.05f, 0, 0);
	}
}
