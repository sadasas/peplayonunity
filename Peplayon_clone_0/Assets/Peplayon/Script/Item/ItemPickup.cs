using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ItemPickup : NetworkBehaviour
{
    public int indexItem;
    public float jumpHeightplus, speedplus, speedandjumpStun, speedmin, Countdown;

    public GameObject EffectPrefab;
    public MeshRenderer mes;
    public Collider coll;

    private CharacterControls characterControls;

    private bool iya = false;
    private GameObject ohteer;
    private UI ui;
    private DetectChild detect;
    private GameObject Effect;

    #region networkbehaviour

    private void Update()
    {
        if (iya)
        {
            if (hasAuthority)
            {
                iya = false;
                ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
                detect = GameObject.FindGameObjectWithTag("IndicatorItemSpawn").GetComponent<DetectChild>();
                Debug.Log("PICKUP");
                if (indexItem == 1)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("JUMP");
                    Jump();
                    CMDsetTransparentBox(this.gameObject);
                }
                else if (indexItem == 2)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("RUN");
                    Run();
                    CMDsetTransparentBox(this.gameObject);
                }
                else if (indexItem == 3)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("SLOW");
                    Slow();
                    CMDsetTransparentBox(this.gameObject);
                }
                else if (indexItem == 4)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("TRANSCULENT");
                    Transculent();
                    CMDsetTransparentBox(this.gameObject);
                }
                else if (indexItem == 5)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("FLASHBACK");
                    Flashback(ohteer.gameObject);
                    CMDsetTransparentBox(this.gameObject);
                }
                else if (indexItem == 6)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("STUNT");
                    Stunt();
                    CMDsetTransparentBox(this.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterControls = other.gameObject.GetComponent<CharacterControls>();
            iya = true;
            ohteer = other.gameObject;
            NetworkIdentity player = GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkIdentity>();
            NetworkIdentity item = GetComponent<NetworkIdentity>();
            AuthoryManager aM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AuthoryManager>();
            aM.getauthority(item, player);
        }
    }

    #endregion networkbehaviour

    #region settransparentbox

    [Command]
    private void CMDsetTransparentBox(GameObject ff)
    {
        ClientRPCsetTransparentBox(ff);
    }

    [ClientRpc]
    public void ClientRPCsetTransparentBox(GameObject dd)
    {
        Debug.Log("SENT ALL TO RPC");
        mes.enabled = false;
        coll.enabled = false;
        for (int i = 0; i <= 6; i++)
        {
            GameObject cc = dd.transform.GetChild(i).gameObject;
            cc.SetActive(false);
            BoxCollider ff = dd.GetComponent<BoxCollider>();
            Destroy(ff);
        }
    }

    #endregion settransparentbox

    #region Coroutine item

    public void Jump()
    {
        StartCoroutine(setJump());
    }

    public void Run()
    {
        StartCoroutine(setRun());
    }

    public void Slow()
    {
        StartCoroutine(setSlow());
    }

    public void Transculent()
    {
        StartCoroutine(setTransculent());
    }

    public void Flashback(GameObject Player)
    {
        StartCoroutine(setFlashback(Player));
    }

    public void Stunt()
    {
        StartCoroutine(setStun());
    }

    public void DestroyTransculent()
    {
        Collider[] AllObject = FindObjectsOfType<Collider>();

        foreach (Collider co in AllObject)
        {
            if (co.CompareTag("Object"))
            {
                co.isTrigger = false;
            }
        }
    }

    #region Set effect and  Destory

    [Client]
    public void setEffect(GameObject pp)
    {
        if (!hasAuthority) return;

        CMDseteffect(pp);
    }

    [Client]
    public void destroyEffect()
    {
        if (!hasAuthority) return;

        CMDdestroyEffect();
    }

    [Command]
    public void CMDseteffect(GameObject jj)
    {
        ClientRPCsetEffect(jj);
    }

    [Command]
    public void CMDdestroyEffect()
    {
        ClientRPCsetDestroyEffect();
    }

    [ClientRpc]
    public void ClientRPCsetEffect(GameObject aa)
    {
        Effect = Instantiate(EffectPrefab, aa.transform.position, aa.transform.rotation, aa.transform) as GameObject;
    }

    [ClientRpc]
    public void ClientRPCsetDestroyEffect()
    {
        Debug.Log("Destroy effect");
        Destroy(Effect);
    }

    #endregion Set effect and  Destory

    private IEnumerator setJump()
    {
        ui.UIeffect.SetActive(true);
        ui.effecthighjump();
        characterControls.jumpHeight = jumpHeightplus;

        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.jumpHeight = 6f;

        detect.Child();
        ui.DestroyAllEffect();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setRun()
    {
        ui.UIeffect.SetActive(true);
        ui.effectfaster();
        characterControls.speed = speedplus;
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.speed = 10f;
        detect.Child();
        ui.DestroyAllEffect();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setSlow()
    {
        ui.UIeffect.SetActive(true);
        ui.effectslower();
        characterControls.speed = speedmin;
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.speed = 10f;
        detect.Child();
        ui.DestroyAllEffect();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setTransculent()
    {
        ui.UIeffect.SetActive(true);
        ui.effecttransculent();
        Collider[] AllObject = FindObjectsOfType<Collider>();

        foreach (Collider co in AllObject)
        {
            if (co.CompareTag("Object"))
            {
                co.isTrigger = true;
            }
        }
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        foreach (Collider co in AllObject)
        {
            if (co.CompareTag("Object"))
            {
                co.isTrigger = false;
            }
        }

        ui.DestroyAllEffect();
        ui.UIeffect.SetActive(false);
        detect.Child();
    }

    private IEnumerator setFlashback(GameObject player)
    {
        ui.UIeffect.SetActive(true);
        ui.effectrespawn();
        DeadZone dd = GameObject.FindGameObjectWithTag("Deadzone").GetComponent<DeadZone>();
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        ui.DestroyAllEffect();
        ui.UIeffect.SetActive(false);
        player.transform.position = dd.currenctCheckPoint;
        detect.Child();
    }

    private IEnumerator setStun()
    {
        ui.UIeffect.SetActive(true);
        ui.EFFECTSTUNT();
        characterControls.speed = speedandjumpStun;
        characterControls.jumpHeight = speedandjumpStun;
        characterControls.rb.isKinematic = true;
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.jumpHeight = 6f;
        characterControls.speed = 10f;
        characterControls.rb.isKinematic = false;

        ui.DestroyAllEffect();
        ui.UIeffect.SetActive(false);
        detect.Child();
    }

    #endregion Coroutine item
}