using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*発展課題用*/

//レッスンで書いた生成用スクリプトを流用
//生成処理のみを関数化したスクリプト
//Forward.csで呼び出す

public class Generator : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    private float posRange = 3.4f;

    //Z座標を受け取り、その座標にアタッチされたprefabからアイテムをランダム生成
    //X座標はアイテムによる、Y座標は常に0
    public void Generate(float posZ)
    {
        int rand = Random.Range(1, 11);
        if (rand <= 1)  //コーンの場合
        {
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab) as GameObject;
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, posZ);
            }
        }
        else
        {
            for (int j = -1; j <= 1; j++)
            {
                //1~6：コイン、7~9車、10何もなし
                int item = Random.Range(1, 11);
                //ラインから前後±5メートルをランダムに
                int offsetZ = Random.Range(-5, 6);

                if (1 <= item && item <= 6)  //コインの場合
                {
                    GameObject coin = Instantiate(coinPrefab) as GameObject;
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, posZ + offsetZ);
                }
                else if (7 <= item && item <= 9)  //車の場合
                {
                    GameObject car = Instantiate(carPrefab) as GameObject;
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, posZ + offsetZ);
                }
            }
        }
    }
}