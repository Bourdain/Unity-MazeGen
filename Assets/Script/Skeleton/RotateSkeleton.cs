using UnityEngine;
using System.Collections;

public class RotateSkeleton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(Random.value * 6, Random.value * 4, Random.value * 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
