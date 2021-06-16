using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimCharacterSelect : MonoBehaviour
{
    public float TweenTime, TweenTime1, TweenTime2, TweenTime3;
    public Image menuTab1, menuTab2, menuTab3;
    public Text text1, text2, text3;

    private void OnEnable()
    {
        StartCoroutine(Menu1());
        StartCoroutine(Menu2());
        StartCoroutine(Menu3());
    }

    private IEnumerator Menu1()
    {
        yield return new WaitForSeconds(TweenTime1);
        menuTab1.enabled = true;
        LeanTween.value(menuTab1.gameObject, 0.1f, 1, TweenTime)
            .setOnUpdate((value) =>
            {
                menuTab1.fillAmount = value;
            });
        yield return new WaitForSeconds(TweenTime);
        text1.enabled = true;
    }

    private IEnumerator Menu2()
    {
        yield return new WaitForSeconds(TweenTime2);
        menuTab2.enabled = true;
        LeanTween.value(menuTab2.gameObject, 0.1f, 1, TweenTime)
            .setOnUpdate((value) =>
            {
                menuTab2.fillAmount = value;
            });

        yield return new WaitForSeconds(TweenTime);
        text2.enabled = true;
    }

    private IEnumerator Menu3()
    {
        yield return new WaitForSeconds(TweenTime3);
        menuTab1.enabled = true;
        LeanTween.value(menuTab3.gameObject, 0.1f, 1, TweenTime)
            .setOnUpdate((value) =>
            {
                menuTab3.fillAmount = value;
            });
        yield return new WaitForSeconds(TweenTime);
        text3.enabled = true;
    }
}