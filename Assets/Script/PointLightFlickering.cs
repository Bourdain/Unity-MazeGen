using UnityEngine;
using System.Collections;

public class PointLightFlickering : MonoBehaviour {

    Vector3 finalPos;

    [Header("Velocity variables")]
    public float vOffsetMax = .46f;
    public float hOffsetMax = .28f;
    public float velocity =.05f;

    [Header("Intensity")]
    public bool intensityFlickering = true;

    float vOffset, hOffset;
    bool vOffsetNegativeDirection, hOffsetNegativeDirection;

    private Light thisLight;
    // Use this for initialization
    void Start ()
    {
        finalPos = transform.position;
        thisLight = this.GetComponent<Light>();

        vOffset = vOffsetMax * UnityEngine.Random.value;
        hOffset = hOffsetMax * UnityEngine.Random.value;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = finalPos;

        transform.position += new Vector3(hOffset, vOffset);

        if (hOffsetNegativeDirection == false)
        {
            hOffset += velocity;

            if (hOffset >= hOffsetMax) hOffsetNegativeDirection = true;
        }

        else
        {
            hOffset -= velocity;
            if (hOffset <= (hOffsetMax * -1)) hOffsetNegativeDirection = false;
        }

        if (vOffsetNegativeDirection == false)
        {
            if(intensityFlickering)
                thisLight.intensity -= velocity*3;


            vOffset += velocity;

            if (vOffset >= vOffsetMax) vOffsetNegativeDirection = true;
        }

        else
        {
            if (intensityFlickering)
                thisLight.intensity += velocity*3;


            vOffset -= velocity;

            if (vOffset <= (vOffsetMax * -1)) vOffsetNegativeDirection = false;
        }

    }
}
