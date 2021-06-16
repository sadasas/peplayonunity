using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustTrigger : NetworkBehaviour
{
    public bool Grounded, CoroutineAllowed;

    public AudioSource jump, walk;

    private CharacterControls cr;

    private void Start()
    {
        cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("GROUNDED");
            jump.Play();

            UI ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
            ui.ClientsetDust(transform.position);
            Grounded = true;
            CoroutineAllowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Grounded = false;
            CoroutineAllowed = false;
        }
    }

    private void Update()
    {
        if (Grounded && cr.blend > 0 && CoroutineAllowed)
        {
            walk.Play();
            Debug.Log("run");
            StartCoroutine("spawndust");
            CoroutineAllowed = false;
        }
        if (cr.blend <= 0 || !Grounded)
        {
            walk.Stop();

            StopCoroutine("spawndust");
            CoroutineAllowed = true;
        }
        UpdateVolume();
    }

    private void UpdateVolume()
    {
    }

    private IEnumerator spawndust()
    {
        while (Grounded)
        {
            Debug.Log("RUN");
            UI ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
            ui.ClientSpawnDustRun(transform.position);
            yield return new WaitForSeconds(0.25f);
        }
    }
}