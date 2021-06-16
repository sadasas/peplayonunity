using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : NetworkBehaviour
{
    public static UI instance;
    private Transform IndicatorPointParent, gk;

    private GameObject tt;

    [SerializeField]
    private GameObject dust, dustlari;

    public GameObject[] IndicatorItem;

    public GameObject UIeffect, parent, cutScene;

    public Text texteffect;

    public float TweenTimeItemInd;

    public string[] sceneList;

    private void Awake()
    {
        instance = this;
    }

    [Command]
    public void CMDstartcutscene()
    {
        startCutcsene();
    }

    [ClientRpc]
    public void startCutcsene()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == sceneList[0] || activeScene.name == sceneList[1])
        {
            Debug.Log("CUTSCENE");
            cutScene.SetActive(true);
            CutScene.instance.DestroyObject();

            CutScene.instance.index = 1;
            CutScene.instance.run = true;
        }
        else if (activeScene.name == sceneList[2])
        {
            Debug.Log("CUTSCENE1");
            cutScene.SetActive(true);
            CutScene.instance.DestroyObject();

            CutScene.instance.index = 2;
            CutScene.instance.run = true;
        }

        /*  MovableObs.ready = true;*/
    }

    #region effect

    public void DestroyAllEffect()
    {
        LeanTween.scale(parent, Vector3.zero, TweenTimeItemInd);
    }

    public void effectfaster()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "LEBIH CEPAT";
    }

    public void effectslower()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "MELAMBAT";
    }

    public void effectbigger()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "BIGGER";
    }

    public void effecttransculent()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "TEMBUS BENDA";
    }

    public void effecthighjump()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "LOMPAT TINGGI";
    }

    public void EFFECTSTUNT()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "TERDIAM";
    }

    public void effectrespawn()
    {
        LeanTween.scale(parent, Vector3.one, TweenTimeItemInd);
        texteffect.text = "RESPAWN";
    }

    #endregion effect

    #region Set Playable Character and non

    [Command]
    public void CMDsetPlayable()
    {
        ClientRPCsetPlayable();
    }

    [ClientRpc]
    public void ClientRPCsetPlayable()
    {
        CharacterControls.cutsceneawal = false;
    }

    [Command]
    public void CMDsetnonPlayable()
    {
        ClientRPCsetnonPlayable();
    }

    [ClientRpc]
    public void ClientRPCsetnonPlayable()
    {
        CharacterControls.cutsceneawal = true;
    }

    #endregion Set Playable Character and non

    #region set dust effect end Destroy

    [Client]
    public void ClientSpawnDustRun(Vector3 pos)
    {
        if (!hasAuthority) return;
        CMDSetDustRun(pos);
    }

    [Command]
    public void CMDSetDustRun(Vector3 pos2)
    {
        ClientRPCsetDustRun(pos2);
    }

    [ClientRpc]
    public void ClientRPCsetDustRun(Vector3 pos3)
    {
        Instantiate(dustlari, pos3, dustlari.transform.rotation);
    }

    [Client]
    public void ClientsetDust(Vector3 dddd)
    {
        if (!hasAuthority) return;
        CMDSetDust(dddd);
    }

    [Command]
    public void CMDSetDust(Vector3 vvv)
    {
        ClientRPCsetDust(vvv);
    }

    [ClientRpc]
    public void ClientRPCsetDust(Vector3 cccc)
    {
        Instantiate(dust, cccc, Quaternion.identity, gk);
        Debug.Log("DUST");
    }

    #endregion set dust effect end Destroy

    #region set indicator item and destroy

    [Client]
    public void ClientSetIndItem(int b, GameObject hit)
    {
        if (!hasAuthority) return;
        CMDsetIndItem(b, hit);
        Debug.Log("SET INDICATOR ITEM");
    }

    [Client]
    public void ClientsetDestroyIndItem(Transform v)
    {
        if (!hasAuthority) return;
        CMDsetDestroyIndItem(v);
        Debug.Log("DESTROY INDICATOR ITEM");
    }

    [Command]
    public void CMDsetIndItem(int a, GameObject hit)
    {
        CilentRPCSetIndicatorItem(a, hit);
    }

    [Command]
    public void CMDsetDestroyIndItem(Transform z)
    {
        ClientRPCdestroy(z);
    }

    [ClientRpc]
    public void CilentRPCSetIndicatorItem(int index, GameObject player)
    {
        GameObject indicatorSpawn = player.transform.GetChild(6).gameObject;

        IndicatorPointParent = indicatorSpawn.transform;
        tt = Instantiate(IndicatorItem[index], IndicatorPointParent.position, Quaternion.identity, IndicatorPointParent);

        #region GAGAL

        /* if (!hasAuthority)
         {
             private Transform IndicatorItemPoint = GameObject.FindGameObjectWithTag("MultiplayerItemSpawn").transform;

     IndicatorPointParent = IndicatorItemPoint.transform;

             tt = private Instantiate(IndicatorItem[index], IndicatorPointParent.position, Quaternion.identity, IndicatorPointParent);
 }*/
    }

    /*[ClientRpc]
    public void SetIndicatorItemmm(int index)
    {
        if (isLocalPlayer) return;

        Call = true;
    }*/

    #endregion GAGAL

    [ClientRpc]
    public void ClientRPCdestroy(Transform dd)
    {
        Destroy(tt);
    }

    #endregion set indicator item and destroy
}