using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Deck : MonoBehaviour {
    //=============================================================
    private Color notMatchColor = new Color(1,1,1,0.2f);
    private Color matchColor = new Color(1,1,1,1);

    private DeckManager.FormState formState = DeckManager.FormState.NotMatch; //型の状態
    private DeckManager.FormType formType = DeckManager.FormType.L; //型タイプ

    private Coroutine coroutine;

    //=============================================================
    private SpriteRenderer spriteRenderer;

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {
        switch(formState) {
            case DeckManager.FormState.NotMatch:
            if(coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            spriteRenderer.color = notMatchColor;
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
            spriteRenderer.color = matchColor;
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
            spriteRenderer.color = new Color(1,1,1,Mathf.Sin(Mathf.Deg2Rad * time));
            yield return null;
        }
    }
}