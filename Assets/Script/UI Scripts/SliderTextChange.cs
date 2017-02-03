using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SliderTextChange : MonoBehaviour {

    Slider Slider;
    Text txt;
	// Use this for initialization
	void Start () {
        Slider = transform.parent.gameObject.GetComponent<Slider>();
        txt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = Slider.value.ToString();
	}
}
