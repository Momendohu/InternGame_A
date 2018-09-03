using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DeckManager : MonoBehaviour {
    //=============================================================
    private PanelManager panelManager;

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
        Debug.Log(CheckL(panelManager.ChainNum,panelManager.StartToGoalDistance(),panelManager.DirectionInfo));
        if(CheckL(panelManager.ChainNum,panelManager.StartToGoalDistance(),panelManager.DirectionInfo) == 1) {
            Debug.Log("成立可能");
        }

        if(CheckL(panelManager.ChainNum,panelManager.StartToGoalDistance(),panelManager.DirectionInfo) == 2) {
            Debug.Log("成立!!");
        }
    }

    //=============================================================
    //L型の条件(方向)
    private int[,] termsL_DirectionInfo = { { 0,0,1 },{ 0,1,1 },{ 1,1,0 },{ 1,0,0 } };
    private int termsL_chainLength = 4;
    private float termsL_startToGoalDistance = Mathf.Sqrt(5);

    //L型が成立するかどうか
    //0:未成立
    //1:成立する可能性あり
    //2:成立
    private int CheckL (int chainNum,float startToGoal,int[] directionInfo) {
        bool matchedDirectionInfo = false;
        bool matchedLength = false;
        bool matchedStartToGoalDistance = false;

        //Debug.Log(chainNum + ":" + startToGoal + ":" + directionInfo + ":" + directionInfo.Length);

        //方向での照合
        if((chainNum - 1) <= termsL_DirectionInfo.GetLength(1)) {
            for(int j = 0;j < termsL_DirectionInfo.GetLength(0);j++) {
                for(int i = 0;i < (chainNum - 1);i++) {
                    if(termsL_DirectionInfo[j,i] != directionInfo[i]) {
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
        if(chainNum == termsL_chainLength) {
            matchedLength = true;
        }

        //始点と終点間の長さでの照合
        if(Mathf.Approximately(startToGoal,termsL_startToGoalDistance)) {
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
}