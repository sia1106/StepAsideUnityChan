using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*レッスンで書いたもの*/

public class ItemGenerator : MonoBehaviour
{
    //参照
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    //スタートとゴールのZ座標
    private int startPos = -160;
    private int goalPos = 120;
    //コインと車のX座標の位置調整に使う
    private float posRange = 3.4f;
    
    void Start()
    {
        //開始時、スタートとゴール間に15メートル間隔で以下の処理を行う
        for (int i = startPos; i < goalPos; i += 15)
        {
            //numは乱数用で1～10の数字が入る(int型乱数はmin以上maxより小さい整数)
            //2／10でコーン生成
            //8／10でそれ以外になる
            int num = Random.Range(1, 11);
            if (num <= 2)  //コーンの場合
            {
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    //instantiateでコーンを原点に生成
                    //jの取る値は(-1 -0.6 -0.2 0.2 0.6 1.0)で6で、各値に4をかけてX座標とする
                    //Yはそのまま(原点なので0)、Zは15の倍数
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else  //コイン・車・何もなしの場合
            {
                for(int j = -1; j <= 1; j++)
                {
                    //jの取る値は-1,0,1で3つ          
                    //以下の処理を3回行う
                    int item = Random.Range(1, 11);  //itemは乱数用で1～10の整数
                    int offsetZ = Random.Range(-5, 6); //オフセットも乱数でｰ5～5の整数

                    if (1 <= item && item <= 6)  //コインの場合
                    {
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        //Xは(-3.4,0,-3.4)の3つの値
                        //zはiの±5メートル範囲
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)  //車の場合(item=10ならはずれ)
                    {
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);

                    }
                }

            }
        }
   
    }
}
                       
//処理のイメージとしては、スタート・ゴール間に15メートル間隔でラインを引いて
//20％の確率でコーンを一直線に並べる
//コーンでない場合、前後10メートル範囲内に「コイン・車・何もなし」がランダムに3点並ぶ
//割合としては60%でコイン、30％で車、10％で何もなし