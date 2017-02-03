using UnityEngine;
using System.Collections;

public class PaintingEvents : MonoBehaviour {

    bool hasPlayedSound = false;
    bool hasRigidBody = false;
    public AudioClip FloorHittingNoise;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (!hasRigidBody)
            if (col.gameObject.CompareTag("Player"))
            {
                transform.gameObject.AddComponent<Rigidbody>();
                hasRigidBody = true;
            }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!hasPlayedSound)
        {
            if (col.gameObject.CompareTag("Floor"))
            {
                AudioSource.PlayClipAtPoint(FloorHittingNoise, this.transform.position);
            }
        }
    }
}
