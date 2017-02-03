using UnityEngine;
using System.Collections;

public class destroyCanvasTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevel == 0)
        {
            Destroy(this.gameObject);
        }
	}
}
