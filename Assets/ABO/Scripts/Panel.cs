using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Panel : MonoBehaviour {
    //=============================================================
    private Color onColor = new Color(97f / 255f,195f / 255f,89f / 255f);
    private Color offColor = new Color(255f / 255f,236f / 255f,185f / 255f);

    //=============================================================
    private PanelManager.EState state = PanelManager.EState.off; //状態

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
        switch(state) {
            case PanelManager.EState.off:
            spriteRenderer.color = offColor;
            break;

            case PanelManager.EState.on:
            spriteRenderer.color = onColor;
            break;

            default:
            break;
        }
    }

    //=============================================================
    //stateを変える
    public void ChangeState (PanelManager.EState _state) {
        state = _state;
    }
}