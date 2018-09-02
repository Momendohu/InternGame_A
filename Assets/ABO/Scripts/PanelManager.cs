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
    public enum EState { off = 0, on = 1 } //状態

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
                obj.transform.position = new Vector2((panelSize + panelGap) * (i - numX / 2) + panelX,(panelSize + panelGap) * (j - numY / 2) + panelY);
                if(i == 2) {
                    obj.GetComponent<Panel>().State = EState.on;
                }

                Debug.Log(i+":"+j);
            }
        }
    }
}