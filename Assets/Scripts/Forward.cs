using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*発展課題用*/

//スタート・ゴール間に15メートル間隔で生成ラインを仮定し
//自身にアタッチされたGenerate.csからGenerate()を呼び出してアイテムを生成
//プレイヤーがゴールを越えたら自身をDestroyする
public class Forward : MonoBehaviour {
    
    Generator generator;
    private GameObject unitychan;
    //スタート・ゴール間の距離を計算する
    private int startPos = -160;
    private int goalPos = 120;
    //前方何メートルまで事前に生成するか
    private float offset = 45f;
    //生成ラインのZ座標を格納するリストと添字用カウンター
    private List<float> genLine;
    private int counter;

    // Use this for initialization
    void Start () {
        //プレイヤー情報を取得しておく
        this.unitychan = GameObject.Find("unitychan");
        //アタッチされているスクリプトを取得しておく、Update()で関数を呼び出す
        generator = GetComponent<Generator>();

        //初期化
        genLine = new List<float>();
        counter = 0;

        //スタート・ゴール間に含まれる15の倍数をListに格納
        //それらを生成するラインのZ座標としてUpdate()でGenerate()に渡す
        for (int i = startPos; i < goalPos; i += 15)
        {
            genLine.Add(i);
        }

        Debug.Log("前方にアイテムを生成します");
    }
	
	void Update () {        
        //プレイヤーのZ座標＋オフセットが、生成するラインを上回った(追い越した)時
        if (unitychan.transform.position.z + offset >= genLine[counter])
        {
            //Z座標にアイテムを生成し、カウンターを１つ増やす
            generator.Generate(genLine[counter]);
            Debug.Log(counter + "回目 Z座標＝" + genLine[counter] + "に生成");
            counter += 1;
        }

        //カウンターが最後まで回ったら自身をDestroy
        if (counter > genLine.Count-1)
        {
            Destroy(gameObject);
        }
    }
}