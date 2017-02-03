using UnityEngine;
using System.Collections;

public class PaintingImageRandom : MonoBehaviour {

    public Texture[] ImageList;

	// Use this for initialization
	void Start () {
        Renderer r = GetComponent<Renderer>();
        r.material.mainTexture = ImageList[(int)Random.Range(0, ImageList.Length)];
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
