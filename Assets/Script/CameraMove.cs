using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue=0;
        float zAxisValue = Input.GetAxis("Vertical");
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            yAxisValue = 1;
        }


        if (Camera.current != null)
        {
            Camera.current.transform.Translate(new Vector3(xAxisValue, zAxisValue, yAxisValue));
        }
    }
}
