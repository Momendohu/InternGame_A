using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DeckManager : MonoBehaviour {
    //=============================================================
    private PanelManager panelManager;

    //=============================================================
    public enum FormState { NotMatch = 0, Matchable = 1, Match = 2 } //型の成立状態
    public enum FormType { None = -1, L = 0, O = 1 } //型タイプ

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        panelManager = GameObject.Find("PanelManager").GetComponent<PanelManager>();
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {
    }

    private void Update () {
        //L型での照合
        switch(Check(panelManager.ChainNum,panelManager.StartToGoalDistance(),panelManager.DirectionInfo,termsL_directionInfo,termsL_chainLength,termsL_startToGoalDistance)) {
            case (int)FormState.NotMatch:
            Debug.Log("未成立");
            break;

            case (int)FormState.Matchable:
            Debug.Log("成立可能");
            break;

            case (int)FormState.Match:
            Debug.Log("成立!!");
            break;

            default:
            break;
        }
    }

    //=============================================================
    //L型の条件(方向)
    private int[,] termsL_directionInfo = { { 0,0,1 },{ 0,1,1 },{ 1,1,0 },{ 1,0,0 } };
    private int termsL_chainLength = 4;
    private float termsL_startToGoalDistance = Mathf.Sqrt(5);

    //=============================================================
    //L型が成立するかどうか
    //0:未成立
    //1:成立する可能性あり
    //2:成立
    //chainNum,startToGoal,directionInfo -> 照合対象
    //terms_DirectionInfo,terms_chainLength,terms_startToDistance -> 照合条件
    private int Check (int chainNum,float startToGoal,int[] directionInfo,int[,] terms_directionInfo,int terms_chainLength,float terms_startToGoalDistance) {
        bool matchedDirectionInfo = false;
        bool matchedLength = false;
        bool matchedStartToGoalDistance = false;

        //Debug.Log(chainNum + ":" + startToGoal + ":" + directionInfo + ":" + directionInfo.Length);

        //方向での照合
        if((chainNum - 1) <= terms_directionInfo.GetLength(1)) {
            for(int j = 0;j < terms_directionInfo.GetLength(0);j++) {
                for(int i = 0;i < (chainNum - 1);i++) {
                    if(terms_directionInfo[j,i] != directionInfo[i]) {
                        break;
                    }
                    matchedDirectionInfo = true;
                }

                if(matchedDirectionInfo) {
                    break;
                }
            }
        } else {
            return 0;
        }

        //チェインの長さでの照合
        if(chainNum == terms_chainLength) {
            matchedLength = true;
        }

        //始点と終点間の長さでの照合
        if(Mathf.Approximately(startToGoal,terms_startToGoalDistance)) {
            matchedStartToGoalDistance = true;
        }

        if(matchedDirectionInfo) {
            if(matchedLength && matchedStartToGoalDistance) {
                return 2;
            } else {
                return 1;
            }
        } else {
            return 0;
        }
    }

    //=============================================================
}