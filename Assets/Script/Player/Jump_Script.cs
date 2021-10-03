using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Script : MonoBehaviour
{
    //　プレイヤー
    private Player_Script PS;
    // ワイヤー
    private Wire_Script WS;
    //　ジャンプ力
    private float speedY = 20;
    

    // Start is called before the first frame update
    void Start()
    {
        //  プレイヤーのコンポーネントを設定
        PS = GameObject.Find("Player").GetComponent<Player_Script>();
        //  ワイヤーのコンポーネントを設定
        WS = GameObject.Find("Button_Wire").GetComponent<Wire_Script>();
    }

    //　ジャンプボタンが押された場合
    public void Click()
    {
        //地面にいるフラグ
        bool jumpable_grand = PS.getGrand();
        //ワイヤー状態フラウ
        bool jumoable_wire = WS.wire_Grab;

        //通常ジャンプ
        if (jumpable_grand == true || jumoable_wire == true)
        {
            //　ワイヤー状態の場合削除
            WS.Wirelift();
            //　ジャンプ力設定
            Vector2 JunpingPower = new Vector2(PS.Player.velocity.x, speedY);
            PS.Player.velocity = JunpingPower;
        }
        //空中ジャンプ
        else if(PS.Doublejump_able == true && Player_Script.aj_able == true)
        {
            //　ジャンプ力設定
            Vector2 JunpingPower = new Vector2(PS.Player.velocity.x, speedY);
            PS.Player.velocity = JunpingPower;

            //空中ジャンプを不可能とする
            PS.Doublejump_able = false;
        }
    }

    private void Update()
    {
        
    }
}
