using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesss : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * 10 * Time.deltaTime);
    }
}