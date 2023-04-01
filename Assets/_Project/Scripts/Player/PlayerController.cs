using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveForceMultiplier = 5f;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float rotationCap = 5f;
        rigidbody2D.SetRotation(rigidbody2D.rotation - horizontalInput * rotationCap);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody2D.AddForce(transform.up * moveForceMultiplier);
        }
    }
}
