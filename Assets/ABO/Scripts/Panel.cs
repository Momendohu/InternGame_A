using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Panel : MonoBehaviour {
    //=============================================================
    private Color pianoColor = new Color(97f / 255f,195f / 255f,89f / 255f);
    private Color drumColor = new Color(255f / 255f,236f / 255f,185f / 255f);

    private readonly float activeAlpha = 1; //アクティブな状態(On)の時の透明度
    private readonly float inactiveAlpha = 0.2f; //アクティブじゃない状態(Off)の時の透明度

    //=============================================================
    public float Px; //座標(形成立判定に使用)
    public float Py; //座標(形成立判定に使用)

    //=============================================================
    private PanelManager.EState state = PanelManager.EState.Off; //状態
    private PanelManager.InstrumentType instrumentType = PanelManager.InstrumentType.None; //楽器タイプ

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
        //楽器タイプに応じて色を変える
        switch(instrumentType) {
            case PanelManager.InstrumentType.Piano:
            spriteRenderer.color = pianoColor;
            break;

            case PanelManager.InstrumentType.Drum:
            spriteRenderer.color = drumColor;
            break;

            default:
            break;
        }

        //ステートに応じて半透明に(フォーカスされていない表現)
        Color nowColor = spriteRenderer.color; //現在の色を取得(最終的には画像でやるから使わないかも)
        switch(state) {
            case PanelManager.EState.Off:
            spriteRenderer.color = new Color(nowColor.r,nowColor.g,nowColor.b,inactiveAlpha);
            break;

            case PanelManager.EState.On:
            spriteRenderer.color = new Color(nowColor.r,nowColor.g,nowColor.b,activeAlpha);
            break;

            default:
            break;
        }
    }

    //=============================================================
    //タッチされた時の処理
    public void Touched (PanelManager.EState _state) {
        PanelManager p = transform.parent.GetComponent<PanelManager>();
        p.TouchedPanelCoordinate[p.TouchedPanelCoordinatePoint] = new Vector2(this.Px,this.Py); //座標を保存
        p.TouchedPanelCoordinatePoint++; //保存を次に進める

        Debug.Log("px:" + this.Px + " py:" + this.Py + " next is " + p.TouchedPanelCoordinatePoint);
        this.ChangeState(_state);
    }

    //=============================================================
    //stateを変える
    public void ChangeState (PanelManager.EState _state) {
        state = _state;
    }

    //=============================================================
    //stateを取得する
    public PanelManager.EState GetState () {
        return state;
    }

    //=============================================================
    //instrumentTypeを変える
    public void ChangeInstrumentType (PanelManager.InstrumentType _instrumentType) {
        instrumentType = _instrumentType;
    }

    //=============================================================
    //=============================================================
    /*internal void ChangeState () {
        throw new NotImplementedException();
    }*/
}