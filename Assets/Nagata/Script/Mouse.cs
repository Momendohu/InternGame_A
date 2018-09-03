using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    [SerializeField] private float Cursol_X;
    [SerializeField] private float Cursol_Y;
    [SerializeField] private bool TapFlag;

    // Use this for initialization
    void Start () {
        MouseInfo();
        TapFlag = false;
    }

    // Update is called once per frame
    void Update () {
        MouseInfo();
    }

    //---------------------------------------------------------
    // マウスの座標やクリックの管理
    //---------------------------------------------------------
    private void MouseInfo () {
        Vector3 vPos = Input.mousePosition;
        vPos.z = 10.0f;

        // マウス座標をワールド座標に変換
        Vector3 vWorldPos = Camera.main.ScreenToWorldPoint(vPos);
        Cursol_X = vWorldPos.x;
        Cursol_Y = vWorldPos.y;
        TapFlag = Input.GetMouseButton(0);  // 左ボタンクリック
        this.transform.position = new Vector3(Cursol_X,Cursol_Y,vWorldPos.z);
    }

    //--------------------------------------------------------
    // 侵入検知(領域に入っているとき)
    //--------------------------------------------------------
    void OnTriggerStay2D (Collider2D other) {
        // 左クリックonの時
        if(TapFlag)
            //配置ブロックの色を変える
            if(other.tag == "Panel") {
                other.gameObject.GetComponent<Panel>().ChangeState(PanelManager.EState.On);
            }
    }
}

