using UnityEngine;
using System.Collections;

public class CapsuleColliderAssign : MonoBehaviour
{

    private CapsuleCollider playerCapsuleCollider;
    
    // Use this for initialization
    void Start()
    {
        playerCapsuleCollider = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CapsuleCollider>();
        this.GetComponent<Cloth>().capsuleColliders[0] = playerCapsuleCollider;


    }

}
