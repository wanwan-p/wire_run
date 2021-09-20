using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    //プレイヤーのリジットボディ
    public Rigidbody2D Player;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのリジットボディを設定
        Player = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
