using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputDataHolderSO inputDataHolderSO;
    [SerializeField]
    private float moveForceMultiplier = 5f;
    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float rotationCap = 5f;
        rigidbody2D.SetRotation(rigidbody2D.rotation - inputDataHolderSO.rotation * rotationCap);

        if (inputDataHolderSO.isImpulsing)
        {
            rigidbody2D.AddForce(transform.up * moveForceMultiplier);
        }
    }
}
