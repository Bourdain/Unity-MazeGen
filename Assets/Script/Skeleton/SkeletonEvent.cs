using UnityEngine;
using System.Collections;

public class SkeletonEvent : MonoBehaviour
{

    public GameObject Parent;
    float time = 0;
    bool startTime = false;
    public AudioClip SkeletonLaugh;
    // Use this for initialization
    void Start()
    {
        Parent.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (startTime)
        {
            time += Time.deltaTime;
        }

        if (time >= 5)
            Parent.SetActive(false);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (!startTime)
            {
                Parent.SetActive(true);
                startTime = true;
                AudioSource.PlayClipAtPoint(SkeletonLaugh, transform.position);
            }
        }
    }

    /* void OnTriggerExit(Collider col)
     {
         if (col.CompareTag("Player"))
             Parent.SetActive(false);
     }*/
}
