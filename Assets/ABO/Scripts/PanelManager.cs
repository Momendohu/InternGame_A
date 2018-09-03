using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PanelManager : MonoBehaviour {
    //=============================================================
    private int panelNumX = 7; //パネルの数
    private int panelNumY = 7; //パネルの数
    private float panelX = 0; //パネルの位置
    private float panelY = 0; //パネルの位置
    private float panelSize = 0.75f; //パネルのサイズ
    private float panelGap = 0.015f; //パネル間のすきま

    private int maxChainNum; //チェインの上限数

    //=============================================================
    public Vector2Int[] ChainInfo; //チェインの座標
    public int ChainNum; //チェイン数

    public int[] DirectionInfo; //方向情報

    //=============================================================
    public enum EState { None = -1, Off = 0, On = 1 } //状態
    public enum Form { None = -1, L = 0, O = 1 } //形
    public enum InstrumentType { None = -1, Piano = 0, Drum = 1 } //楽器タイプ
    public enum DirectionType { Vertically = 0, Horizontally = 1 } //方向タイプ

    //=============================================================
    private GameObject panel; //パネル

    //=============================================================
    private void Init () {
        CRef();

        maxChainNum = panelNumX * panelNumY;
        ChainInfo = new Vector2Int[maxChainNum];
        DirectionInfo = new int[maxChainNum - 1];
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
    //スタートからゴールの距離
    public float StartToGoalDistance () {
        if(ChainNum >= 1) {
            return Vector2Int.Distance(ChainInfo[0],ChainInfo[ChainNum - 1]);
        } else {
            return 0;
        }
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

                //座標を設定
                obj.GetComponent<Panel>().Px = i;
                obj.GetComponent<Panel>().Py = j;

                //ランダムで楽器タイプを決定
                if((float)Random.Range(0,2) >= 1) {
                    obj.GetComponent<Panel>().ChangeInstrumentType(InstrumentType.Piano);
                } else {
                    obj.GetComponent<Panel>().ChangeInstrumentType(InstrumentType.Drum);
                }

                //親を設定
                obj.transform.SetParent(this.transform,false);

                //Debug.Log(i + ":" + j);
            }
        }
    }
}