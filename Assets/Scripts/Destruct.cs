using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*課題：ユニティちゃんが通り過ぎて画面外に出たアイテムを直ちに破棄してください
アイテムの破棄にはDestroy関数を使いましょう*/

//Prefabにアタッチし、オブジェクトが画面外に出た時
//自身をDestroyするスクリプト
public class Destruct : MonoBehaviour {

    private GameObject unitychan;
    private float PlayerZ;
    private float ObjectZ;
    //画面外に出るまでの距離
    private float visibleZ = -5;
    
    void Start () {
        //開始時にオブジェクトのZ座標を取得
        this.ObjectZ = this.transform.position.z;
        //プレイヤーの情報を取得しておく
        this.unitychan = GameObject.Find("unitychan");
    }
	
	void Update () {
        //プレイヤーのZ座標を取得
        this.PlayerZ = unitychan.transform.position.z;
        //追い越す(オブジェクトのZ座標を上回った)場合Destroy
        if (PlayerZ + visibleZ > ObjectZ)
        {
            Destroy(gameObject);
        }
    }
}
