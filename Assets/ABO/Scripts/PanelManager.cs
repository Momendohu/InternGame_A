using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PanelManager : MonoBehaviour {
    //=============================================================
    private int panelNumX = 11; //パネルの数
    private int panelNumY = 11; //パネルの数
    private float panelX = 0; //パネルの位置
    private float panelY = 0; //パネルの位置
    private float panelSize = 0.48f; //パネルのサイズ
    private float panelGap = 0.015f; //パネル間のすきま

    //=============================================================
    public enum EState { None = -1, Off = 0, On = 1 } //状態
    public enum Form { None = -1, L = 0, O = 1 } //形
    public enum InstrumentType { None = -1, Piano = 0, Drum = 1 }//楽器タイプ

    //=============================================================
    private GameObject panel; //パネル

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        panel = Resources.Load("Panel") as GameObject;
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {
        CreatePanels(panelNumX,panelNumY);
    }

    private void Update () {

    }

    //=============================================================
    //パネルの生成
    //numX:横の数
    //numY:縦の数
    private void CreatePanels (int numX,int numY) {
        GameObject obj = null;

        for(int i = 0;i < numX;i++) {
            for(int j = 0;j < numY;j++) {
                obj = Instantiate(panel) as GameObject;
                obj.transform.position = new Vector2((panelSize + panelGap) * (i - (int)(numX / 2)) + panelX,(panelSize + panelGap) * (j - (int)(numY / 2)) + panelY);

                //ランダムで楽器タイプを決定
                if((float)Random.Range(0,2) >= 1) {
                    obj.GetComponent<Panel>().ChangeInstrumentType(InstrumentType.Piano);
                } else {
                    obj.GetComponent<Panel>().ChangeInstrumentType(InstrumentType.Drum);
                }

                /*switch(form) {
                    case Form.L:
                    if(TermsL(i - (int)(numX / 2),j - (int)(numY / 2))) {
                        obj.GetComponent<Panel>().ChangeState(EState.on);
                    }
                    break;

                    case Form.O:
                    if(TermsO(i - (int)(numX / 2),j - (int)(numY / 2))) {
                        obj.GetComponent<Panel>().ChangeState(EState.on);
                    }
                    break;
                }*/

                //Debug.Log(i + ":" + j);
            }
        }
    }

    //=============================================================
    //L型の条件
    private bool TermsL (int numX,int numY) {
        if(numX == -1 && numY == 0) {
            return true;
        }

        if(numX == -1 && numY == 1) {
            return true;
        }

        if(numX == 0 && numY == 0) {
            return true;
        }

        if(numX == 1 && numY == 0) {
            return true;
        }

        return false;
    }

    //=============================================================
    //O型の条件
    private bool TermsO (int numX,int numY) {
        if(numX == 0 && numY == 0) {
            return true;
        }

        if(numX == 0 && numY == 1) {
            return true;
        }

        if(numX == 1 && numY == 0) {
            return true;
        }

        if(numX == 1 && numY == 1) {
            return true;
        }

        return false;
    }
}