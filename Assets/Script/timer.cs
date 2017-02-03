using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    float t = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevel == 1)
        {

        t += Time.deltaTime;
        Debug.Log(t);
        Text txt = gameObject.GetComponent<Text>();

        txt.text = "Segundos passados: " + ((int)t).ToString();
        }
    }
}
