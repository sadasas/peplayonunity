using Mirror;
using Peplayon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public static CutScene instance;
    public GameObject dd;

    private Camera Camera2, Camera1;

    public int index;
    public GameObject CountStart;

    public Text txt;
    public AudioSource swepp, horn, countdown;

    public CharacterControls cr;
    private UI ui;
    public bool run = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (run)
        {
            if (index == 1)
            {
                //Debug.Log("Find Cam");
                //dd = GameObject.FindGameObjectWithTag("PlayerCamera").gameObject;
                dd = FindObjectOfType<CameraManager>().gameObject;
                Camera1 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<Camera>();
                Camera2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<Camera>();
                cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
                run = false;
                StartCoroutine(startCutScene());
                CharacterControls.cutsceneawal = false;
            }
            else if (index == 2)
            {
                dd = GameObject.FindGameObjectWithTag("PlayerCamera").gameObject;
                Camera1 = GameObject.FindGameObjectWithTag("Camera1").GetComponent<Camera>();
                Camera2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<Camera>();
                cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
                run = false;
                StartCoroutine(startCutscene3());
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(Camera2);
        Destroy(Camera1);
        Destroy(dd);
        Destroy(cr);
    }

    private IEnumerator startCutscene3()
    {
        Debug.Log("Ssssssssssssssssssssssssssdfffffffffffffffffffffffffffffffffffffffffffffffffffssssss");
        Camera1.enabled = true;
        dd.SetActive(false);

        LeanTween.move(Camera1.gameObject, new Vector3(58.2910538f, 85.07f, -28.120369f), 2f);

        LeanTween.rotate(Camera1.gameObject, new Vector3(18.8000011f, 282.5f, 0), 2f);
        yield return new WaitForSeconds(2f);
        LeanTween.move(Camera1.gameObject, new Vector3(17.7510529f, 77.18f, 50.2196274f), 2f);
        LeanTween.rotate(Camera1.gameObject, new Vector3(18.8000011f, 185.800018f, 0), 2f);

        yield return new WaitForSeconds(2f);
        LeanTween.move(Camera1.gameObject, new Vector3(-64f, 68f, 15.1000004f), 2);
        LeanTween.rotate(Camera1.gameObject, new Vector3(18.8000031f, 99.1000137f, -1.80378026e-06f), 2f);
        yield return new WaitForSeconds(2f);
        LeanTween.move(Camera1.gameObject, new Vector3(-4.57999992f, 43.0999985f, -71.3000031f), 2);
        LeanTween.rotate(Camera1.gameObject, new Vector3(18.8000031f, 0, -1.80378026e-06f), 2f);
        yield return new WaitForSeconds(2f);
        LeanTween.move(Camera1.gameObject, new Vector3(-4.57999992f, 107.129997f, -71.3000031f), 2f);
        yield return new WaitForSeconds(3f);
        Camera2.enabled = true;
        Camera1.enabled = false;
        CountStart.SetActive(true);
        countdown.Play();

        LeanTween.scale(CountStart, Vector3.one, 0.3f)
            .setEaseInCirc();
        yield return new WaitForSeconds(0.3f);
        txt.text = "1";
        yield return new WaitForSeconds(0.7f);
        CountStart.transform.localScale = Vector3.zero;
        LeanTween.scale(CountStart, Vector3.one, 0.3f)
             .setEaseInSine();
        yield return new WaitForSeconds(0.3f);
        txt.text = "2";
        countdown.Play();
        yield return new WaitForSeconds(0.7f);
        CountStart.transform.localScale = Vector3.zero;
        LeanTween.scale(CountStart, Vector3.one, 0.3f)
             .setEaseInQuad();
        yield return new WaitForSeconds(0.3f);

        txt.text = "3";
        countdown.Play();
        yield return new WaitForSeconds(0.7f);
        CountStart.transform.localScale = Vector3.zero;
        CountStart.SetActive(false);
        horn.Play();

        yield return new WaitForSeconds(1);
        CharacterControls.cutsceneawal = false;
        dd.SetActive(true);
        Camera2.enabled = false;
        Obstacle1Map3.active = true;
    }

    private IEnumerator startCutScene()
    {
        Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
        dd.SetActive(false);

        LeanTween.move(Camera1.gameObject, new Vector3(16.2000008f, 19.8999996f, 188f), 9f);
        swepp.Play();
        yield return new WaitForSeconds(10);

        Camera1.enabled = false;
        Camera2.enabled = true;
        swepp.Stop();
        LeanTween.move(Camera2.gameObject, new Vector3(16.2000008f, 18, -179.800003f), 3f);
        swepp.Play();
        yield return new WaitForSeconds(2);

        LeanTween.move(Camera2.gameObject, new Vector3(35.7f, 23.8999996f, -200f), 1f);

        LeanTween.rotate(Camera2.gameObject, new Vector3(48.2000122f, 180f, 0), 1f);
        yield return new WaitForSeconds(1);
        swepp.Stop();
        LeanTween.move(Camera2.gameObject, new Vector3(11f, 23.8999996f, -200f), 4f);

        yield return new WaitForSeconds(3);

        yield return new WaitForSeconds(1);

        dd.SetActive(true);
        Camera2.enabled = false;
        countdown.Play();
        CountStart.SetActive(true);
        LeanTween.scale(CountStart, Vector3.one, 0.3f)
            .setEaseInCirc();
        yield return new WaitForSeconds(0.3f);
        txt.text = "1";
        yield return new WaitForSeconds(0.7f);
        CountStart.transform.localScale = Vector3.zero;
        LeanTween.scale(CountStart, Vector3.one, 0.3f)
             .setEaseInSine();
        yield return new WaitForSeconds(0.3f);
        txt.text = "2";
        countdown.Play();
        yield return new WaitForSeconds(0.7f);
        CountStart.transform.localScale = Vector3.zero;
        LeanTween.scale(CountStart, Vector3.one, 0.3f)
             .setEaseInQuad();
        yield return new WaitForSeconds(0.3f);

        txt.text = "3";
        countdown.Play();
        yield return new WaitForSeconds(0.7f);
        CountStart.transform.localScale = Vector3.zero;
        CountStart.SetActive(false);
        horn.Play();
        yield return new WaitForSeconds(1);
        Debug.Log("HARUSNYA NYALA");
    }
}