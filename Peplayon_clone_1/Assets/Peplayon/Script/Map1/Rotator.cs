using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 3f, speedMove = 100f;
    public bool obstacle1;
    public bool obstacle2;
    public bool obstacle3;
    public bool clearobstacle;

    private Rigidbody rb;

    private Rigidbody plyr;

    private void Start()

    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (MovableObs.ready)
        {
            if (obstacle1)
            {
                transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f, Space.Self);
            }
            else if (obstacle2)
            {
                transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("frezzerotation");
            plyr = other.GetComponent<Rigidbody>();

            Invoke("addforcw", 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /* CharacterControls.canMove = false;*/
        }
    }

    private void addforcw()
    {
        Vector3 movee = new Vector3(1, 0, 0);
        plyr.AddForce(-movee * speedMove);
    }
}