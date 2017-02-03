using UnityEngine;
using System.Collections;

public class Chalice : MonoBehaviour {
    void Start()
    {

    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("Player"))
        {
            Application.LoadLevel(2);
        }
    }
}
