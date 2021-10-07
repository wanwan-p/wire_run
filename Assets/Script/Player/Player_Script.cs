using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Script : MonoBehaviour
{
    //今のシーン
    public static string now_scene;

    //プレイヤーのリジットボディ
    public Rigidbody2D Player;
    //空中ジャンプ可能フラグ(スキルを取得済か関係なし)
    public bool Doublejump_able = true;
    //地面のレイヤー(Unityのインスペクターでstageを設定している)
    public LayerMask CollisionLayer;

    //スキル使用可能状況
    //空中ジャンプ
    public static bool aj_able;
    //地上斬り
    public static bool gd_able;
    //空中回転斬り
    public static bool ad_able;
    //ワイヤー距離増加
    public static bool wg_able;
    //ホバリング1
    public static bool h1_able;
    //ホバリング2
    public static bool h2_able;
    //ホバリング3
    public static bool h3_able;

    //コライダーのフィルターを公開し着地判定(Unityのインスペクターで判定の角度を50〜130に設定)
    [SerializeField] ContactFilter2D filter2D_Dawn;
    //コライダーのフィルターを公開し衝突判定(Unityのインスペクターで判定の角度を110~250に設定)
    [SerializeField] ContactFilter2D filter2D_front;
    //コライダーのフィルターを公開し衝突判定(Unityのインスペクターで判定の角度を全角度に設定)
    [SerializeField] ContactFilter2D filter2D_All;

    //プレイヤーの速度
    private float Speed = 4.0f;
    //private float Speed = 0.0f;

    // 一番最初に実行
    void Start()
    {
        //プレイヤーのリジットボディを設定
        Player = this.gameObject.GetComponent<Rigidbody2D>();
        //今のシーン
        now_scene = SceneManager.GetActiveScene().name;
        //スキルセット
        skill_set();
    }

    // 常に実行
    void Update()
    {
        //キャラクターを右に移動
        //ワイヤー状態でない場合
        if(Wire_Script.wire_Grab == false)
        {
            //y方向への速さは保持
            Vector2 move = new Vector2(Speed, Player.velocity.y);
            Player.velocity = move;

            //positionでのキャラ移動を行ったがガクガクするためボツ
            #region
            //前方の壁に当たった場合、挙動がおかしくなるため後ろに戻す
            //if (GetComponent<Rigidbody2D>().IsTouching(filter2D_front))
            //{
            //    //止まる
            //    this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);

            //    ////現在のキャラクターの位置
            //    //Transform playerTransform = Player.transform;
            //    //Vector2 playerPosition = playerTransform.position;

            //    ////少し後ろの座標を設定
            //    //playerPosition.x -= 0.1f;

            //    ////座標を設定
            //    //playerTransform.position = playerPosition;

            //}
            //else
            //{
            //    //右へ等速で進ませる
            //    this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
            //}
            #endregion
        }
        //空中ジャンプ後に地面についた場合、もしくはワイヤー状態になった場合
        if(Doublejump_able == false)
        {
            if(getGrand() == true || Wire_Script.wire_Grab == true)
            {
                //空中ジャンプフラグをtrueにする
                Doublejump_able = true;
            }
        }
    }
    private void skill_set()
    {
        //空中ジャンプ
        aj_able = skill_able(PlayerPrefsKey.air_jump_able);
        //地上斬り
        gd_able = skill_able(PlayerPrefsKey.grand_decapitate_able);
        //空中回転斬り
        ad_able = skill_able(PlayerPrefsKey.air_decapitate_able);
        //ワイヤー距離増加
        wg_able = skill_able(PlayerPrefsKey.wire_gain_able);
        //ホバリング1
        h1_able = skill_able(PlayerPrefsKey.hovering1_able);
        //ホバリング2
        h2_able = skill_able(PlayerPrefsKey.hovering2_able);
        //ホバリング3
        h3_able = skill_able(PlayerPrefsKey.hovering3_able);
}

    //スキルが使用可能かboolで返す
    public  bool skill_able(string PPK)
    {
        int i = PlayerPrefs.GetInt(PPK, 0);
        if(i == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //地面についているか判定
    public bool getGrand()
    {
        return GetComponent<Rigidbody2D>().IsTouching(filter2D_Dawn);
    }

    //ステージに衝突したか判定
    public bool getCollision()
    {
        return GetComponent<Rigidbody2D>().IsTouching(filter2D_All);
    }

    //ワイヤー状態の時z軸の回転を可能とする
    public void z_Freeze()
    {
        if(Wire_Script.wire_Grab == true)
        {
            Player.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            //プレイヤーの回転を0に戻して、ｚ軸の回転を不可
            Player.rotation = default;
            Player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}