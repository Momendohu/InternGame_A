using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Deck : MonoBehaviour {
    //=============================================================
    private Color notMatchColor = new Color(1,1,1,0.2f);
    private Color matchColor = new Color(1,1,1,1);

    private Coroutine coroutine;

    //=============================================================
    [System.NonSerialized]
    public DeckManager.FormState FormState = DeckManager.FormState.NotMatch; //型の状態

    [System.NonSerialized]
    public DeckManager.FormType FormType = DeckManager.FormType.L; //型タイプ

    //=============================================================
    private SpriteRenderer image1;
    private SpriteRenderer image2;

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        image1 = transform.Find("Image1").GetComponent<SpriteRenderer>();
        image2 = transform.Find("Image2").GetComponent<SpriteRenderer>();
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {
        switch(FormState) {
            case DeckManager.FormState.NotMatch:
            if(coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            image1.color = notMatchColor;
            image2.color = notMatchColor;
            break;

            case DeckManager.FormState.Matchable:
            if(coroutine == null) {
                coroutine = StartCoroutine(Flush(3));
            }
            break;

            case DeckManager.FormState.Match:
            if(coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            image1.color = matchColor;
            image2.color = matchColor;
            break;

            default:
            break;
        }
    }

    //=============================================================
    //点滅
    private IEnumerator Flush (float speed) {
        float time = 0;
        while(true) {
            time += speed;
            image1.color = new Color(1,1,1,Mathf.Sin(Mathf.Deg2Rad * time));
            image2.color = new Color(1,1,1,Mathf.Sin(Mathf.Deg2Rad * time));
            yield return null;
        }
    }
}