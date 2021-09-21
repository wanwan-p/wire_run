using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    //プレイヤーのリジットボディ
    public Rigidbody2D Player;
    //ジャンプ可能フラグ
    public bool jump_Flg = true;
    //地面のレイヤー(Unityのインスペクターでstageを設定している)
    public LayerMask CollisionLayer;

    //コライダーのフィルターを公開し着地判定(Unityのインスペクターで判定の角度を70〜110に設定)
    [SerializeField] ContactFilter2D filter2D_Dawn;
    //コライダーのフィルターを公開し衝突判定(Unityのインスペクターで判定の角度を0に設定)
    [SerializeField] ContactFilter2D filter2D＿front;

    //プレイヤーの速度
    private float Speed = 3.0f;
    //private float Speed = 0.0f;
    // 一番最初に実行
    void Start()
    {
        //プレイヤーのリジットボディを設定
        Player = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // 常に実行
    void Update()
    {
        //右へ等速で進ませる
        this.transform.position += new Vector3(Speed * Time.deltaTime, 0 , 0);

        //ジャンプフラグの設定
        if(jump_Flg == false)
        {
            //地面についた場合true
            jump_Flg = IsCollision();
        }

        //前方の壁に当たった場合、挙動がおかしくなるため後ろに戻す
        //if(GetComponent<Rigidbody2D>().IsTouching(filter2D＿front))
        //{
        //    //現在のキャラクターの位置
        //    Transform myTransform = this.transform;
        //    Vector2 charactorPosition = myTransform.position;

        //    //少し後ろの座標を設定
        //    charactorPosition.x -= 1.0f;

        //    //座標を設定
        //    myTransform.position = charactorPosition;

        //}

        DEBUG_text Dt = GameObject.Find("Debug_Text").GetComponent<DEBUG_text>();
        Dt.textupdate(GetComponent<Rigidbody2D>().IsTouching(filter2D_Dawn));
    }

    //ジャンプフラグ設定処理
    bool IsCollision()
    {
        return GetComponent<Rigidbody2D>().IsTouching(filter2D_Dawn);
    }

}