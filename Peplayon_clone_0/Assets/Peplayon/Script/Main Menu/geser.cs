using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class geser : MonoBehaviour
{
    public GameObject ChooseCharacter;
    public GameObject MainMenu;
    public GameObject camera;
    public Image tabMenu1, tabMenu2, tabMenu3;
    public Text text1, text2, text3;

    public GameObject selectmenu;
    public GameObject option;
    public GameObject credit;
    public GameObject play;
    public Canvas main;
    public Canvas choose;
    public float tweentime;
    private bool mainmenuv;

    public void Slide()
    {
        StartCoroutine(slidemenu());
    }

    public void BckSlide()
    {
        StartCoroutine(slidemenuBack());
    }

    private IEnumerator slidemenu()
    {
        LeanTween.scale(selectmenu, Vector3.zero, tweentime)
            .setEaseInOutSine();
        LeanTween.scale(option, Vector3.zero, tweentime)
           .setEaseInOutSine();
        LeanTween.scale(credit, Vector3.zero, tweentime)
           .setEaseInOutSine();
        LeanTween.scale(play, Vector3.zero, tweentime)
           .setEaseInOutSine();
        yield return new WaitForSeconds(tweentime);
        LeanTween.move(camera, new Vector3(26.2999992f, 1.5f, -9.6f), 1f);
        main.enabled = false;
        yield return new WaitForSeconds(1);
        choose.enabled = true;
        MainMenu.SetActive(false);

        ChooseCharacter.SetActive(true);
    }

    private IEnumerator slidemenuBack()
    {
        tabMenu1.fillAmount = 0f;
        tabMenu2.fillAmount = 0f;
        tabMenu3.fillAmount = 0f;
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        mainmenuv = true;
        LeanTween.move(camera, new Vector3(0f, 1.5f, -9.6f), 1f);

        choose.enabled = false;

        yield return new WaitForSeconds(1);
        main.enabled = true;

        ChooseCharacter.SetActive(false);

        MainMenu.SetActive(true);
        LeanTween.scale(selectmenu, Vector3.one, tweentime)
           .setEaseInSine();
        LeanTween.scale(option, Vector3.one, tweentime)
           .setEaseInSine();
        LeanTween.scale(credit, Vector3.one, tweentime);
        LeanTween.scale(play, Vector3.one, tweentime)
           .setEaseInSine();
    }

    private void Update()
    {
    }
}