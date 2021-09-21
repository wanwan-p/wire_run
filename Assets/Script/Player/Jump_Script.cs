using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Script : MonoBehaviour
{
    //　プレイヤー
    private Player_Script PS;
    //　ジャンプ力
    private float speedX = 0;
    private float speedY = 800;
    

    // Start is called before the first frame update
    void Start()
    {
        //  プレイヤーのコンポーネントを設定
        PS = GameObject.Find("Player").GetComponent<Player_Script>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //　ジャンプボタンが押された場合
    public void Click()
    {        
        if(PS.jump_Flg == true)
        {
            //　ジャンプ力設定
            Vector2 JunpingPower = new Vector2(speedX, speedY);
            PS.Player.AddForce(JunpingPower);

            //　ジャンプフラグをfalseに変更
            PS.jump_Flg = false;
        }
    }
}
